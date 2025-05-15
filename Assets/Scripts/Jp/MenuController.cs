using JetBrains.Annotations;
using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    SceneController sceneController;
    public void NewGame()
    {
        sceneController = new SceneController();
        sceneController.LoadScene("Cena Navegacao");
        

    }

    public void Config()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        sceneController = new SceneController();
        sceneController.LoadScene("Tela de Configuracoes");    

    }


    

}
