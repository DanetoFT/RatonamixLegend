using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenuController : MonoBehaviour
{
    private AudioSource sonido;
    public AudioClip clickAudio;

    public GameObject pausePanel;
    public static bool isPaused = false;

    public static bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        sonido = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                isPaused = true;
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                isPaused = false;
                pausePanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
    public void ClickAudioOn()
    {
        sonido.PlayOneShot(clickAudio);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void QuitPause()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}
