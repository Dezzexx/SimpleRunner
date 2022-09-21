using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class TouchInitSystem : IEcsInitSystem {
        readonly EcsPoolInject<InputComponent> _inputComponentPool = default;
        readonly EcsPoolInject<PlayerComponent> _playerComponentPool = default;
        readonly EcsCustomInject<StaticData> _staticData = default;
        readonly EcsWorldInject _world = default;
        public void Init (IEcsSystems systems) {
            var entity = _world.Value.NewEntity();
            var playerView = GameObject.FindGameObjectWithTag("Player");

            ref var inputComponent = ref _inputComponentPool.Value.Add(entity);
            ref var playerComponent = ref _playerComponentPool.Value.Add(entity);
            inputComponent.SwipeRange = _staticData.Value.SwipeRange;
            inputComponent.StartTouchingPosition = Vector2.zero;
            inputComponent.CurrentTouchingPosition = Vector2.zero;
            inputComponent.StopTouching = false;
            playerComponent.Transform = playerView.transform;
        }
    }
}