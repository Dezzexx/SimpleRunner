using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class WInInitSystem : IEcsInitSystem {
        readonly EcsPoolInject<WinComponent> _winComponent = default;
        readonly EcsCustomInject<SceneData> _sceneData = default;
        readonly EcsWorldInject _world = default;

        private int _gameTimeScale = 0;
        public void Init (IEcsSystems systems) {
            var entity = _world.Value.NewEntity();

            ref var winComponent = ref _winComponent.Value.Add(entity);
            winComponent.isGameActive = _sceneData.Value.isGameEnable = false;
            winComponent.GameTimeScale = _gameTimeScale;
        }
    }
}