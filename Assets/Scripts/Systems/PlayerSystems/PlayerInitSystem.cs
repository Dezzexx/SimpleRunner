using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class PlayerInitSystem : IEcsPreInitSystem {
        readonly EcsPoolInject<PlayerComponent> _playerComponentPool = default;
        readonly EcsWorldInject _world = default;
        
        public void PreInit (IEcsSystems systems) {
            var entity = _world.Value.NewEntity();
            var playerView = GameObject.FindGameObjectWithTag("Player");

            ref var playerComponent = ref _playerComponentPool.Value.Add(entity);
            playerComponent.Transform = playerView.transform;
            
            var playerMonoBehavior = playerView.GetComponent<Player>();
            playerMonoBehavior.World = _world.Value;
        }
    }
}