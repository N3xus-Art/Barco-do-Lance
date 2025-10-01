using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMusicVolume()
    {
        float volumeMusic = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volumeMusic)*20);

    }

    public void SetSFXVolume()
    {

        float volumeSFX = SFXSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volumeSFX)*20);





    }



}
