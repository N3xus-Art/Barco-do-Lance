using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour{
    //Variables
    #region
    private SceneController sceneController;
    private List<string> scenes = new List<string>(){"Cena Navegacao", "Menu Configuracoes", "Creditos"};
    public int scene;
    public GameObject buttons, confirmExit;
    #endregion

    //Methods
    #region
    public void ChangeScene(){
        sceneController = new SceneController();
        sceneController.LoadScene(scenes[scene]);
    }
    public void Show(){
            confirmExit.SetActive(true);
            buttons.SetActive(false);
    }
    public void Exit(){
        Debug.Log("Exit");
        Application.Quit();
    }
    public void Hide(){
        Debug.Log("Hide");
        confirmExit.SetActive(false);
        buttons.SetActive(true);
    }
    #endregion
}
