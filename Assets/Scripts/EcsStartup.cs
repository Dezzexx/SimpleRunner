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
        private IEcsSystems _systems;

        private void Start () {
            Time.timeScale = 1;
            var hudDisplayService = new HUDDisplayService();
            hudDisplayService.Text = _text;
            hudDisplayService.WinPanel = _winPanel;

            _world = new EcsWorld ();
            _systems = new EcsSystems (_world, hudDisplayService);


            _systems
                .Add (new TileInitSystem())
                .Add (new TileMoveSystem())
                .Add (new TileSpawnSystem())
                .Add (new PlayerInitSystem())
                .Add (new CollectibleRunSystem())
                .Add (new TouchInitSystem())
                .Add (new TouchControlSystem())
                .Add (new WInInitSystem())
                .Add (new WinSystem())
                
                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                .DelHere<CollectEvent>()
                .Inject(_staticData, _sceneData, hudDisplayService)
                .Init();
        }

        private void Update () {
            // process systems here.
            _systems?.Run ();
        }

        private void OnDestroy () {
            // list of custom worlds will be cleared
            // during IEcsSystems.Destroy(). so, you
            // need to save it here if you need.
            _systems?.Destroy ();
            _systems?.GetWorld ()?.Destroy ();
            _systems = null;

            // cleanup custom worlds here.
            
            // cleanup default world.
            // if (_world != null) {
            //     _world.Destroy ();
            //     _world = null;
            // }
        }
    }
}