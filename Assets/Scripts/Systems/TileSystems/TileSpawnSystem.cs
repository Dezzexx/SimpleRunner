using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class TileSpawnSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<TileComponent>> _filter = default;
        readonly EcsCustomInject<SceneData> _sceneData = default;
        readonly EcsPoolInject<TileComponent> _tilePool = default;

        private float _tileDeleteZBounds = -120f;

        

        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value) {
                ref TileComponent tileComponent = ref _tilePool.Value.Get(entity);

                TileSpawner();
            }
        }

        private void TileSpawner() {
            foreach (var entity in _filter.Value) {
                if (_sceneData.Value.UsedTileList[0].transform.position.z < _tileDeleteZBounds) {
                    var tile = _sceneData.Value.UsedTileList[0];
                    _sceneData.Value.UsedTileList.RemoveAt(0);
                    
                    var lastTileInList = _sceneData.Value.UsedTileList[_sceneData.Value.UsedTileList.Count - 1];

                    var tilePosition = tile.transform.position;
                    tilePosition.z = lastTileInList.transform.position.z + lastTileInList.GetComponentInChildren<Renderer>().bounds.size.z;
                    tile.transform.position = tilePosition;

                    _sceneData.Value.UsedTileList.Add(tile);
                }
            }
        }
    }
}