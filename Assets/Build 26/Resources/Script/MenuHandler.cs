using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour{
    //Variables
    #region
    private SceneController sceneController;
    private List<string> scenes = new List<string>(){"Cena Navegacao", "Menu Configuracoes", "Creditos"};
    public int scene;
    public GameObject buttons, confirmExit, yesButton, noButton;
    #endregion

    //Methods
    #region
    public void Start(){
        confirmExit.SetActive(false);
    }
    public void ChangeScene(){
        sceneController = new SceneController();
        sceneController.LoadScene(scenes[scene]);
    }
    public void Exit(){
        confirmExit.SetActive(true);
        //buttons.SetActive(false);
        Debug.lo
    }
    public void ExitCheck(){
        if (this == yesButton){
            Application.Quit();
            Debug.Log("Saiu");
        }else if (this == noButton) {
            confirmExit.SetActive(false);
            buttons.SetActive(true);
            Debug.Log("Saiu");
        }
    }

    #endregion
}
