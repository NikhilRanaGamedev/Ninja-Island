using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume", 1);
    }

    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("Volume", GetComponent<Slider>().value);
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
    }
}
