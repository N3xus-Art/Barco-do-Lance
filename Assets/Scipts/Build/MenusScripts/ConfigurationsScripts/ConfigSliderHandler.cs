using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ConfigSliderHandler : MonoBehaviour
{
    //Variables
    #region
    [SerializeField] private Slider slider;
    [SerializeField] private int value;
    [SerializeField] private TMP_Text TMPtext;
    [SerializeField] private string text;
    [SerializeField] public bool isOn = true;
    #endregion

    //Methods
    #region
    public void SliderUpdator()
    {
        value = (int)slider.value;
        TMPtext.text = $"{text}: {value}";
    }
    public void Update()
    {
        SliderUpdator();
    }
    #endregion
}
