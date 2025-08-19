using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class ScriptTesteLeme : MonoBehaviour
{
    bool CollidinoLeme;

    SceneController sceneController;

     void OnTriggerEnter2D(Collider2D collision)
    {
        CollidinoLeme = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CollidinoLeme = false;
    }

    public void EntrarNoLeme()
    {

        if (CollidinoLeme) {

            sceneController = new SceneController();
            sceneController.LoadScene("Scene Map", LoadSceneMode.Single);

        }

    }

}
