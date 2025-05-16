using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfigController : MonoBehaviour
{
    public Slider SliderVolumeGeral,SliderVolumeMusica;
    public float VolumeGeral,VolumeMusica;
    public TMP_Text TextoVolumeGeral;
    public TMP_Text TextoVolumeMusica;

    void Update()
    {

        VolumeGeral = SliderVolumeGeral.value * 100;
        VolumeMusica = SliderVolumeMusica.value * 100;

        TextoVolumeGeral.text = $"Volume Geral: {VolumeGeral}";
        TextoVolumeMusica.text = $"Volume Geral: {VolumeMusica}";



    }

}
