using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class TimerSystem : IEcsRunSystem {   
        readonly EcsFilterInject<Inc<TimeComponent>> _filter;
        readonly EcsPoolInject<TimeComponent> _timerPool = default;     
        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value) {
                ref TimeComponent timerComponent = ref _timerPool.Value.Get(entity);

                timerComponent.TimeBetweenFrames += Time.deltaTime;
                
                if (timerComponent.TimeBetweenFrames > 2.1f) {
                    timerComponent.TimeBetweenFrames = 0f;
                }
            }
        }
    }
}