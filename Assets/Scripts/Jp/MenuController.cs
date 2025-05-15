using JetBrains.Annotations;
using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    SceneController sceneController;
    public void Start()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        sceneController = new SceneController();
        




    }


}
