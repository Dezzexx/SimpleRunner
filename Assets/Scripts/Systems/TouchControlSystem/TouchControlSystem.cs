using System.Diagnostics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class TouchControlSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<PlayerComponent>> _filter = default;
        readonly EcsPoolInject<PlayerComponent> _playerComponentPool = default;

        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value) {
                ref PlayerComponent playerComponent = ref _playerComponentPool.Value.Get(entity);
                var playerTransform = playerComponent.Transform;
                var playerSpeed = playerComponent.PlayerSpeed;

                if (Input.touchCount > 0) {
                    Touch touch = Input.GetTouch(0);
                    Vector2 touchDeltaPosition = touch.deltaPosition;
                    var moveDirection= new Vector3(touchDeltaPosition.x * playerSpeed, 0, 0); 

                    switch (touch.phase) {
                        case(TouchPhase.Moved):
                            playerTransform.Translate(moveDirection * Time.fixedDeltaTime);
                            break;
                        case(TouchPhase.Stationary):
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

                // if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
                //     Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                //     var moveDirection= new Vector3(touchDeltaPosition.x * playerSpeed, 0, 0); 
                //     playerTransform.Translate(moveDirection * Time.fixedDeltaTime);

                //     if (playerTransform.position.x < playerComponent.MaxLeftBound) {
                //         var currentPosition = new Vector3(playerComponent.MaxLeftBound, playerTransform.position.y, playerTransform.position.z);
                //         playerTransform.position = currentPosition;
                //     }
                        
                //     if (playerTransform.position.x > playerComponent.MaxRightBound) {
                //         var currentPosition = new Vector3(playerComponent.MaxRightBound, playerTransform.position.y, playerTransform.position.z);
                //         playerTransform.position = currentPosition;
                //     }

                // }
            }
        }
    }
}
