using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class ParticleRunSystem : IEcsRunSystem {   
        readonly EcsFilterInject<Inc<ParticleComponent>> _filter = default; 
        readonly EcsPoolInject<ParticleComponent> _particlePool = default;
        readonly EcsPoolInject<PlayerComponent> _playerPool = default;    
        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value) {
                var brianView = GameObject.FindGameObjectWithTag("Brain");
                ref ParticleComponent particleComponent = ref _particlePool.Value.Get(entity);  

                var brainPosition = brianView.transform.position;
                var brainExplosionPrefab = particleComponent.BrainExplosion;
                if (!brianView.gameObject.activeInHierarchy) {
                    var brainExplosionRef = GameObject.Instantiate(brainExplosionPrefab, brainPosition, Quaternion.identity);
                }
            }
        }
    }
}