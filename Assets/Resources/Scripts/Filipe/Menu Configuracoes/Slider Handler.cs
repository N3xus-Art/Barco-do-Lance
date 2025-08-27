using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderHandler: MonoBehaviour
{
    public Slider Slider;
    public int Valor;
    public TMP_Text TMP_text;
    public string Texto;
    public bool Ligado = true;

    void Update()
    {
        Valor = (int)Slider.value;

        TMP_text.text = $"{Texto}: {Valor}";
    }

}
