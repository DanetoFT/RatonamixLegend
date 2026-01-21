using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    public GameObject exitCanvas;

    void Start()
    {
        exitCanvas.SetActive(false);
    }
    public void OnConfirmExit()
    {
        exitCanvas.SetActive(true);
    }

    public void OnCloseExit()
    {
        exitCanvas.SetActive(false);
    }

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
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }
}
