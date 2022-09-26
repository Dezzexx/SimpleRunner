using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class BrainSpawnSystem : IEcsRunSystem {  
        readonly EcsFilterInject<Inc<TimerComponent, BrainComponent>> _filter = default;
        readonly EcsCustomInject<SceneData> _sceneData = default;
        readonly EcsPoolInject<BrainComponent> _brainPool = default;
        readonly EcsPoolInject<TimerComponent> _timerPool = default;  

        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value) {
                ref TimerComponent timerComponent = ref _timerPool.Value.Get(entity);
                ref BrainComponent brainComponent = ref _brainPool.Value.Get(entity);
                var randomSpawnPos = Random.Range(0, _sceneData.Value.BrainSpawnPoints.Count);
                var spawnPos = _sceneData.Value.BrainSpawnPoints[randomSpawnPos];

                if (timerComponent.TimeBetweenFrames > 2f) {
                    var brainGameObject = GameObject.Instantiate(brainComponent.BrainPrefab, spawnPos.position, Quaternion.identity);
                    brainComponent.brainTransform.Add(brainGameObject.transform);
                }
            }
        }
    }
}