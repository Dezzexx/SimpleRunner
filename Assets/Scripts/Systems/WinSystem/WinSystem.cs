using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class WinSystem : IEcsRunSystem {    
        readonly EcsFilterInject<Inc<WinComponent>> _filter = default;
        readonly EcsPoolInject<WinComponent> _winPool = default;
        readonly EcsCustomInject<SceneData> _sceneData = default;
        readonly EcsSharedInject<HUDDisplayService> _sharedHUDDisplay = default;   

        //ебануть пул на игрока, добавить к нему анимацию idle, когда !isGameActive.
        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value) {
                ref WinComponent winComponent = ref _winPool.Value.Get(entity);
                if (_sharedHUDDisplay.Value.GoalBrainEating <= winComponent.ValueForVictory) {
                    _sharedHUDDisplay.Value.WinPanel.SetActive(true);
                    _sharedHUDDisplay.Value.Text.gameObject.SetActive(false);
                    _sceneData.Value.isGameActive = false;
                }
            }
        }
    }
}