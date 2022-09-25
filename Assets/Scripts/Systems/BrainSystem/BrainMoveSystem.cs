using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class BrainMoveSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<BrainComponent>> _filter = default;
        readonly EcsPoolInject<BrainComponent> _brainPool = default;

        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value) {
                ref BrainComponent brainComponent = ref _brainPool.Value.Get(entity);
                
                Quaternion rotationY = Quaternion.AngleAxis(brainComponent.RotationAngle * brainComponent.RotationSpeed, Vector3.up);
                brainComponent.Transform.position -= Vector3.forward * brainComponent.Speed * Time.deltaTime;
                brainComponent.Transform.rotation *= rotationY;
            }
        }
    }
}