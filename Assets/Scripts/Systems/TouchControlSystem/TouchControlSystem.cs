using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class TouchControlSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<InputComponent>> _filter = default;
        readonly EcsPoolInject<InputComponent> _inputComponentPool = default;
        readonly EcsPoolInject<PlayerComponent> _playerComponentPool = default;

        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value) {
                ref InputComponent inputComponent = ref _inputComponentPool.Value.Get(entity);
                ref PlayerComponent playerComponent = ref _playerComponentPool.Value.Get(entity);
                var swipeRange = inputComponent.SwipeRange;
                inputComponent.StopTouching = false;

                if((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began)) {
                    inputComponent.StartTouchingPosition = Input.GetTouch(0).position;
                }
                
                if((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Moved)) {
                    inputComponent.CurrentTouchingPosition = Input.GetTouch(0).position;
                    Vector2 distance = inputComponent.CurrentTouchingPosition - inputComponent.StartTouchingPosition;

                    if(!inputComponent.StopTouching) {
                        if (distance.x < -swipeRange) {
                            var playerPosition = playerComponent.Transform.position;
                            playerPosition.x = -9;
                            playerComponent.Transform.position = playerPosition;
                            inputComponent.StopTouching = true;
                        }
                        else if (distance.x > swipeRange) {
                            var playerPosition = playerComponent.Transform.position;
                            playerPosition.x = -4;
                            playerComponent.Transform.position = playerPosition;   
                            inputComponent.StopTouching = true;
                        }
                    }
                }
            }
        }
    }
}