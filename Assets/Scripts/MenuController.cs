using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private AudioSource sonido;
    public AudioClip clickAudio;
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        sonido = GetComponent<AudioSource>();

    }

    public void StartButton()
    {
        StartCoroutine(ChangeSceneAfterDelay(0.5f));
        Debug.Log("Empieza el juego");
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void ClickAudioOn()
    {
        sonido.PlayOneShot(clickAudio);
    }
    IEnumerator ChangeSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Game");
    }
    IEnumerator MenuSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MenuStart");
    }
}
