using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class TileInitSystem : IEcsInitSystem {
        readonly EcsCustomInject<StaticData> _staticData = default;
        readonly EcsCustomInject<SceneData> _sceneData = default;
        readonly EcsPoolInject<TileComponent> _tilePool = default;
        readonly EcsWorldInject _world = default;

        public void Init (IEcsSystems systems) {
            _sceneData.Value.TileSpawnPoint = Vector3.zero;

            for (int i = 0; i < _staticData.Value.TileCount; i++) {
                CreateTile();
            }
        }

        private void CreateTile() {
            var entity = _world.Value.NewEntity();
                
            var randomTile = Random.Range(0, _staticData.Value.Tiles.Count);
            var tilePrefab = _staticData.Value.Tiles[randomTile];
            var spawnPoint = _sceneData.Value.TileSpawnPoint;

            var prevTile = GameObject.Instantiate(tilePrefab, spawnPoint, Quaternion.identity);
            _sceneData.Value.UsedTileList.Add(prevTile);
            _sceneData.Value.TileSpawnPoint += Vector3.Scale(prevTile.GetComponentInChildren<Renderer>().bounds.size, Vector3.forward);

            ref var tileComponent = ref _tilePool.Value.Add(entity);
            tileComponent.Speed = _staticData.Value.MoveSpeed;
            tileComponent.Transform = prevTile.transform;
        }
    }
}