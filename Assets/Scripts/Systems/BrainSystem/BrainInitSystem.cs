using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class BrainInitSystem : IEcsInitSystem {
        readonly EcsCustomInject<SceneData> _sceneData = default;
        readonly EcsCustomInject<StaticData> _staticData = default;
        readonly EcsPoolInject<BrainComponent> _brainPool = default;
        readonly EcsPoolInject<TimerComponent> _timerPool = default;
        readonly EcsWorldInject _world = default;

        private float _rotationAngle = 1f;
        private float _rotationSpeed = .3f;
        public void Init (IEcsSystems systems) {
            var entity = _world.Value.NewEntity();

            ref var brainComponent = ref _brainPool.Value.Add(entity);
            ref var timerComponent = ref _timerPool.Value.Add(entity);
            brainComponent.BrainPrefab = _sceneData.Value.BrainPrefab;
            brainComponent.RotationSpeed = _rotationSpeed;
            brainComponent.RotationAngle = _rotationAngle;
            brainComponent.Speed = _staticData.Value.MoveSpeed;
            brainComponent.brainTransform = new List<Transform>();
        }
    }
}