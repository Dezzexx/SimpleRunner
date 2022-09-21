using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class TileMoveSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<TileComponent>> _filter = default;
        readonly EcsPoolInject<TileComponent> _tilePool = default;

        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value) {
                ref TileComponent tileComponent = ref _tilePool.Value.Get(entity);
                
                tileComponent.Transform.position -= Vector3.forward * tileComponent.Speed * Time.deltaTime;
            }
        }
    }
}