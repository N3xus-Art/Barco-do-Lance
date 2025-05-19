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
        sceneController.LoadScene("Scene Navigation");
        

    }

    public void Config()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        sceneController = new SceneController();
        sceneController.LoadScene("Settings Screen");    

    }

    public void Credits()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        sceneController = new SceneController();
        sceneController.LoadScene("Credits Screen");

    }

    public void Exit()
    {
        Application.Quit();

    }




}
