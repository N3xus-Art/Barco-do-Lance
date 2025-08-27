using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigMuteHandler : MonoBehaviour{
    //Variables
    #region
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite muted, unmuted;
    [SerializeField] private ConfigSliderHandler sliderConfigs;
    #endregion

    //Methods
    #region
    public void Click(){
        sliderConfigs.isOn ^= true;
        if (buttonImage.sprite == unmuted){
            buttonImage.sprite = muted;
        }else if (buttonImage.sprite == muted){
            buttonImage.sprite = unmuted;
        }
    }
    #endregion
}
