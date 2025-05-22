using System;
using UnityEngine;
using UnityEngine.UI;

public class LogicaVolumen : MonoBehaviour
{
    public Slider sliderVol;
    public float sliderValueVol;
    public Image imageMute;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageMute.enabled = true;
        sliderVol.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = sliderVol.value;
        RevisarMute();
    }

    public void ChangeSlider(float valor)
    {
        sliderValueVol = valor;
        PlayerPrefs.GetFloat("volumenAudio", sliderValueVol);
        AudioListener.volume = sliderVol.value;
        RevisarMute();
    }

    private void RevisarMute()
    {
        if(sliderValueVol == 0)
        {
            imageMute.enabled = true;
        }
        else
        {
            imageMute.enabled = false;
        }
    }
}
