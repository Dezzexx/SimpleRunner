using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {
    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit() {
#if UNITY_EDITOR 
        {
            EditorApplication.ExitPlaymode();
        };
#else
        Application.Quit();
#endif
    }
}
