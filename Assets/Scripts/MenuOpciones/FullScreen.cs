using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    public Toggle Toggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Screen.fullScreen)
        {
            Toggle.isOn = true;
        }
        else
        {
            Toggle.isOn = false;

        }
    }
    public void ActiveFullScreen(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }
}
