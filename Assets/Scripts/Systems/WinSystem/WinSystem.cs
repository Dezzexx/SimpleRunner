using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class WinSystem : IEcsRunSystem {    
        readonly EcsFilterInject<Inc<WinComponent>> _filter = default;
        readonly EcsPoolInject<WinComponent> _winComponent = default;
        readonly EcsSharedInject<HUDDisplayService> _sharedHUDDisplay = default;   

        private int winCounter;  
        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value) {
                ref WinComponent winComponent = ref _winComponent.Value.Get(entity);
                if (_sharedHUDDisplay.Value.GoalBrainEating <= 0) {
                    Time.timeScale = winComponent.GameTimeScale;
                    _sharedHUDDisplay.Value.WinPanel.SetActive(true);
                    _sharedHUDDisplay.Value.Text.gameObject.SetActive(false);
                }
            }
        }
    }
}