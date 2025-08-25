using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour{
    //Variables
    #region
    private SceneController sceneController;
    private List<string> scenes = new List<string>(){"Cena Navegacao", "Menu Configuracoes", "Creditos"};
    public int scene;
    public GameObject ConfirmExit;
    #endregion

    //Methods
    #region
    public void ChangeScene(){
        sceneController = new SceneController();
        sceneController.LoadScene(scenes[scene]);
    }
    public void Exit(){
        ConfirmExit.SetActive(true);
    }
    #endregion
}
