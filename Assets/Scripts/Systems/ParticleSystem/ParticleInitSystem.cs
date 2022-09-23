using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client {
    sealed class ParticleInitSystem : IEcsInitSystem {
        readonly EcsWorldInject _world = default;
        readonly EcsPoolInject<ParticleComponent> _particlePool = default;
        readonly EcsPoolInject<PlayerComponent> _playerComponentPool = default;
        public void Init (IEcsSystems systems) {
            var entity = _world.Value.NewEntity();

            ref var brainExplosion = ref _particlePool.Value.Add(entity);
            brainExplosion.BrainExplosion = Resources.Load<ParticleSystem>("Particle/BrainExplosion");
        }
    }
}