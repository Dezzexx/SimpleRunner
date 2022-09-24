using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class WinInitSystem : IEcsInitSystem {
        readonly EcsPoolInject<WinComponent> _winComponent = default;
        readonly EcsWorldInject _world = default;

        private int _valueForVictory = 0;  

        public void Init (IEcsSystems systems) {
            var entity = _world.Value.NewEntity();

            ref var winComponent = ref _winComponent.Value.Add(entity);
            winComponent.ValueForVictory = _valueForVictory;
        }
    }
}