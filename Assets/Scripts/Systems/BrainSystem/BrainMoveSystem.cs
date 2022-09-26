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
                for (int i = 0; i < brainComponent.brainTransform.Count; i++) {
                    brainComponent.brainTransform[i].position -= Vector3.forward * brainComponent.Speed * Time.deltaTime;
                    brainComponent.brainTransform[i].rotation *= rotationY;
                    if (brainComponent.brainTransform[i].position.z < -120) {
                        brainComponent.brainTransform[i].gameObject.SetActive(false);
                        brainComponent.brainTransform.RemoveAt(i);
                    }
                }
            }
        }
    }
}