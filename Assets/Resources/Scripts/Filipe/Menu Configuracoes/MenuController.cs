using JetBrains.Annotations;
using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuControllerFilipe : MonoBehaviour
{
    SceneController sceneController;
    public void NewGame()
    {
        sceneController = new SceneController();
        sceneController.LoadScene("Cena Navegacao");


    }

    public void Config()
    {

        sceneController = new SceneController();
        sceneController.LoadScene("Menu Configuracoes");

    }

    public void Credits()
    {

        sceneController = new SceneController();
        sceneController.LoadScene("Creditos");

    }

    public void Back()
    {
        
        sceneController = new SceneController();
        sceneController.LoadScene("Cena Tela Inicial");

    }

}
