using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{

    public Slider slider;
    public float sliderValue;
    public Image imageMute;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumen",0.5f);
        AudioListener.volume = sliderValue;
        IsMute();
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumen", sliderValue);
        AudioListener.volume = slider.value;
        IsMute();


    }

    public void IsMute()
    {
        if (sliderValue ==0)
        {
            imageMute.enabled = true;
        }
        else
        {
            imageMute.enabled= false;
        }

    }
}
