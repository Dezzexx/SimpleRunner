using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class TouchControlSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<PlayerComponent>> _filter = default;
        readonly EcsPoolInject<PlayerComponent> _playerComponentPool = default;

        private Vector2 _startPos;
        private Vector2 _direction;

        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value) {
                ref PlayerComponent playerComponent = ref _playerComponentPool.Value.Get(entity);
                var playerTransform = playerComponent.Transform;
                var playerSpeed = playerComponent.PlayerSpeed;

                if (Input.touchCount > 0) {
                    Touch touch = Input.GetTouch(0);
                    
                    switch (touch.phase) {
                        case(TouchPhase.Began):
                            _startPos = touch.position;
                            break;
                        case(TouchPhase.Moved):
                            Vector2 touchDeltaPosition = touch.deltaPosition;
                            var moveDirection= new Vector3(touchDeltaPosition.x * playerSpeed, 0, 0); 
                            playerTransform.Translate(moveDirection * Time.fixedDeltaTime);
                            break;
                        case(TouchPhase.Stationary):
                            _direction = touch.position - _startPos;
                            var moveDirectionStationary = new Vector3(_direction.x * 0.05f, 0, 0);
                            playerTransform.Translate(moveDirectionStationary * Time.fixedDeltaTime);
                            break;
                        default:
                            break;
                    }

                    if (playerTransform.position.x < playerComponent.MaxLeftBound) {
                        var currentPosition = new Vector3(playerComponent.MaxLeftBound, playerTransform.position.y, playerTransform.position.z);
                        playerTransform.position = currentPosition;
                    }
                        
                    if (playerTransform.position.x > playerComponent.MaxRightBound) {
                        var currentPosition = new Vector3(playerComponent.MaxRightBound, playerTransform.position.y, playerTransform.position.z);
                        playerTransform.position = currentPosition;
                    }
                }
            }
        }
    }
}
