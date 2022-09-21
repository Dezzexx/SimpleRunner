using System.Collections.Generic;
using UnityEngine;

namespace Client{
    sealed class SceneData : MonoBehaviour{
        public List<GameObject> UsedTileList = new List<GameObject>();
        public Vector3 TileSpawnPoint;
        public bool isGameEnable;
    }
}
