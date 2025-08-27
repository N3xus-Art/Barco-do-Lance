using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ConfigMenuHandler : MonoBehaviour{
    //Variables
    #region
    private SceneHandler sceneHandler;
    //private int generalVolume, musicVolume, SFXVolume;
    //private bool isFullscreen;
    #endregion

    //Methods
    #region
    public void Back(){
        sceneHandler = new SceneHandler();
        sceneHandler.LoadScene("HomeScreen");
    }
    #endregion
}
