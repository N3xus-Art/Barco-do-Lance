using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour{
    //Variables
    #region
    private SceneHandler sceneHandler;
    private List<string> scenes = new List<string>(){"Cena Navegacao", "ConfigurationsScreen", "Creditos"};
    public int scene;
    public GameObject buttons, confirmExit;
    #endregion

    //Methods
    #region
    public void ChangeScene(){
        sceneHandler = new SceneHandler();
        sceneHandler.LoadScene(scenes[scene]);
    }
    public void Show(){
            confirmExit.SetActive(true);
            buttons.SetActive(false);
    }
    public void Exit(){
        Application.Quit();
    }
    public void Hide(){
        confirmExit.SetActive(false);
        buttons.SetActive(true);
    }
    #endregion
}
