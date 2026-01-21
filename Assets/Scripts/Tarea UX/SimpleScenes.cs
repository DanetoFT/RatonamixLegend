using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleScenes : MonoBehaviour
{
    public void LoadSceneByIndex(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("El índice " + sceneIndex + " no existe en Build Settings.");
        }
    }
}
