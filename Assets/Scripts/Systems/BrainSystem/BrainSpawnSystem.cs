using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class BrainSpawnSystem : IEcsRunSystem {  
        readonly EcsFilterInject<Inc<BrainComponent, TimeComponent>> _filter = default;
        readonly EcsPoolInject<BrainComponent> _brainPool = default;
        readonly EcsPoolInject<TimeComponent> _timerPool = default;  
        readonly EcsCustomInject<StaticData> _staticData= default;

        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value) {
                ref BrainComponent brainComponent = ref _brainPool.Value.Get(entity);
                ref TimeComponent timerComponent = ref _timerPool.Value.Get(entity);
                var spawnPos = new Vector3(-4f, 2.5f, 263f);

                if (timerComponent.TimeBetweenFrames >= 2f) {
                    var brainGameObject = GameObject.Instantiate(brainComponent.BrainPrefab, spawnPos, Quaternion.identity);
                }
            }
        }
    }
}