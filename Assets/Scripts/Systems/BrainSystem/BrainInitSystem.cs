using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class BrainInitSystem : IEcsInitSystem {
        readonly EcsCustomInject<StaticData> _staticData = default;
        readonly EcsPoolInject<BrainComponent> _brainPool = default;
        readonly EcsWorldInject _world = default;

        private float _rotationAngle = 1f;
        private float _rotationSpeed = .1f;
        private float _speed = 5f;
        public void Init (IEcsSystems systems) {
            var entity = _world.Value.NewEntity();
            var spawnPos = new Vector3(-4f, 2.5f, 263f);

            ref var brainComponent = ref _brainPool.Value.Add(entity);
            brainComponent.BrainPrefab = _staticData.Value.BrainPrefab;
            brainComponent.RotationSpeed = _rotationSpeed;
            brainComponent.RotationAngle = _rotationAngle;
            brainComponent.Speed = _speed;

            var brain = GameObject.Instantiate(brainComponent.BrainPrefab, spawnPos, Quaternion.identity);
            brainComponent.Transform = brain.transform;
        }
    }
}