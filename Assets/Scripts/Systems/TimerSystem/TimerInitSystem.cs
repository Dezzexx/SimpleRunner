using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class TimerInitSystem : IEcsInitSystem {
        readonly EcsPoolInject<TimeComponent> _timerPool = default;
        readonly EcsWorldInject _world = default;

        private float _timeBetweenFrames = 0;
        public void Init (IEcsSystems systems) {
            var entity = _world.Value.NewEntity();

            ref var timerComponent = ref _timerPool.Value.Add(entity);
            timerComponent.TimeBetweenFrames = _timeBetweenFrames;
        }
    }
}