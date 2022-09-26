using UnityEngine;
using System.Collections.Generic;

namespace Client {
    struct BrainComponent {
        public GameObject BrainPrefab;
        public List<Transform> brainTransform;
        public float TimeBetweenFrames;
        public float Speed;
        public float RotationSpeed;
        public float RotationAngle;
    }
}