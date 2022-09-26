using System.Collections.Generic;
using UnityEngine;

namespace Client{
    sealed class SceneData : MonoBehaviour{
        public List<GameObject> UsedTileList = new List<GameObject>();
        public List<Transform> BrainSpawnPoints = new List<Transform>();
        public GameObject BrainPrefab;
        public Vector3 TileSpawnPoint;
        public bool isGameActive;
    }
}
