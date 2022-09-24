using Leopotam.EcsLite;
using UnityEngine;

namespace Client {
    public class Player : MonoBehaviour {
        public EcsWorld World { get; set; }
        private Object _praticleBrainDestroy;

        private void Start() {
            _praticleBrainDestroy = Resources.Load<ParticleSystem>("Particle/BrainExplosion");
        }
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Brain")) {
                var brainPosition = other.gameObject.transform.position;
                var brainExplosion = GameObject.Instantiate(_praticleBrainDestroy, brainPosition, Quaternion.identity);
                other.gameObject.SetActive(false);
                
                ref var collectEventComponent = ref World.GetPool<CollectEvent>().Add(World.NewEntity());
                collectEventComponent.CollectibleValue = 1;
            }
        }
    }
}
