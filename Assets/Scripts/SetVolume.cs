using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SetVolume : MonoBehaviour
{

    public float volume;

    public AudioMixer mixer;
    public void Options()
    {
        gameObject.GetComponent<Slider>().value = Mathf.Pow(10, GameStatus.status.volume/20);
    }

    public void setLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20f);
        volume = Mathf.Log10(sliderValue) * 20;
        GameStatus.status.volume = volume;
        Debug.Log("Volume " + Mathf.Pow(10f, volume / 20f));
    }
}
