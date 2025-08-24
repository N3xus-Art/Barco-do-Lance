using JetBrains.Annotations;
using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    SceneController sceneController;
    public GameObject ConfirmExit;
    public void NewGame()
    {
        sceneController = new SceneController();
        sceneController.LoadScene("Scene Navigation");
    }

    public void Config()
    {
        sceneController = new SceneController();
        sceneController.LoadScene("Settings Screen");    
    }

    public void Credits()
    {
        sceneController = new SceneController();
        sceneController.LoadScene("Credits Screen P");
    }

    public void Exit()
    {
        ConfirmExit.SetActive(true);
    }




}
