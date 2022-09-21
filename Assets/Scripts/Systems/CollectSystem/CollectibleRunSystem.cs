using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class CollectibleRunSystem : IEcsRunSystem {     
        readonly EcsFilterInject<Inc<CollectEvent>> _filter = default;
        readonly EcsPoolInject<CollectEvent> _collectEvent = default;  
        readonly EcsSharedInject<HUDDisplayService> _sharedHUDDisplay = default; 
        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value) {
                ref CollectEvent collectEvent = ref _collectEvent.Value.Get(entity);

                _sharedHUDDisplay.Value.GoalBrainEating -= collectEvent.CollectibleValue;
                _sharedHUDDisplay.Value.Text.text = $"Brain eating goal:{_sharedHUDDisplay.Value.GoalBrainEating.ToString()}";
            }
        }
    }
}