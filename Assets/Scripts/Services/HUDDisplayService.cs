using TMPro;
using UnityEngine;

namespace Client {
    sealed class HUDDisplayService {
        public TextMeshProUGUI Text;
        public GameObject WinPanel;
        public GameObject StartPanel;
        public GameObject BloodSplatter;
        public int GoalBrainEating = 15;
    }
}