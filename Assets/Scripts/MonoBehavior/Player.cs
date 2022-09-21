using Leopotam.EcsLite;
using UnityEngine;

namespace Client {
    public class Player : MonoBehaviour {
        public EcsWorld World { get; set; }
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Brain")) {
                other.gameObject.SetActive(false);

                ref var collectEventComponent = ref World.GetPool<CollectEvent>().Add(World.NewEntity());
                collectEventComponent.CollectibleValue = 1;
            }
        }
    }
}
