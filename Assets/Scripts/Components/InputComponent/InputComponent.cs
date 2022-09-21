using UnityEngine;

namespace Client {
    struct InputComponent {
        public Vector2 StartTouchingPosition;
        public Vector2 CurrentTouchingPosition;
        public bool StopTouching;
        public float SwipeRange;
    }
}