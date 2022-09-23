using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;
using TMPro;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {
        [SerializeField] private StaticData _staticData;
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _winPanel;

        private EcsWorld _world;        
        private IEcsSystems _initSystems;
        private IEcsSystems _fixedUpdateSystems;
        private IEcsSystems _gameActiveSystems;
        private IEcsSystems _gameNotActiveSystems;

        private void Start () {
            _sceneData.isGameActive = true;

            var hudDisplayService = new HUDDisplayService();
            hudDisplayService.Text = _text;
            hudDisplayService.WinPanel = _winPanel;
            
            _world = new EcsWorld ();
            _initSystems = new EcsSystems (_world, hudDisplayService);
            _fixedUpdateSystems = new EcsSystems (_world);
            _gameActiveSystems = new EcsSystems(_world, hudDisplayService);
            _gameNotActiveSystems = new EcsSystems(_world, hudDisplayService);
            
            _gameActiveSystems
                .Add (new TileMoveSystem())
                .Add (new TileSpawnSystem())
                .Add (new CollectibleRunSystem())
                // .Add (new ParticleRunSystem())

                .DelHere<CollectEvent>()
                .Inject(_staticData, _sceneData, hudDisplayService)
                .Init();

            _gameNotActiveSystems
                .Add (new WinSystem())

                .Inject(_sceneData, hudDisplayService)
                .Init();

            _fixedUpdateSystems
                .Add (new TouchControlSystem())

                .Inject(_staticData, _sceneData, hudDisplayService)
                .Init();

            _initSystems
                .Add (new TileInitSystem())
                .Add (new PlayerInitSystem())
                .Add (new WinInitSystem())
                // .Add (new ParticleInitSystem())
                
                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                // .DelHere<CollectEvent>()
                // .DelHere<ParticleComponent>()
                .Inject(_staticData, _sceneData, hudDisplayService)
                .Init();
        }

        private void Update () {
            _initSystems?.Run();

            // if (!_sceneData.isGameActive == false) {
            //     _gameNotActiveSystems?.Run();
            // }
            // if (_sceneData.isGameActive == true) {
            //     _gameActiveSystems?.Run();
            // }
            if (!_sceneData.isGameActive == false) {
                _gameNotActiveSystems?.Run();
            }
            if (_sceneData.isGameActive) {
                _gameActiveSystems?.Run();
            }
        }

        private void FixedUpdate() {
            if(_sceneData.isGameActive) {
                _fixedUpdateSystems?.Run();
            }
        }

        private void OnDestroy () {
            // list of custom worlds will be cleared
            // during IEcsSystems.Destroy(). so, you
            // need to save it here if you need.
            _initSystems?.Destroy();
            _initSystems?.GetWorld()?.Destroy();
            _initSystems = null;

            _gameActiveSystems?.Destroy();
            _gameActiveSystems?.GetWorld()?.Destroy();
            _gameActiveSystems = null;

            _gameNotActiveSystems?.Destroy();
            _gameNotActiveSystems?.GetWorld()?.Destroy();
            _gameNotActiveSystems = null;           

            _fixedUpdateSystems?.Destroy();
            _fixedUpdateSystems?.GetWorld()?.Destroy();
            _fixedUpdateSystems = null;

            // cleanup custom worlds here.
            
            // cleanup default world.
            // if (_world != null) {
            //     _world.Destroy ();
            //     _world = null;
            // }
        }
    }
}