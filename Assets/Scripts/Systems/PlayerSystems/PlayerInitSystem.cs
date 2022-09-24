using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class PlayerInitSystem : IEcsInitSystem {
        readonly EcsPoolInject<PlayerComponent> _playerComponentPool = default;
        readonly EcsWorldInject _world = default;
        
        private float _playerSpeed = 0.25f;
        private float _maxLeftBound = -10f;
        private float _maxRightBound = -3f;
        
        public void Init (IEcsSystems systems) {
            var entity = _world.Value.NewEntity();
            var playerView = GameObject.FindGameObjectWithTag("Player");

            ref var playerComponent = ref _playerComponentPool.Value.Add(entity);
            playerComponent.Transform = playerView.transform;
            playerComponent.PlayerSpeed = _playerSpeed;
            playerComponent.MaxLeftBound = _maxLeftBound;
            playerComponent.MaxRightBound = _maxRightBound;
            
            var playerMonoBehavior = playerView.GetComponent<Player>();
            playerMonoBehavior.World = _world.Value;
        }
    }
}