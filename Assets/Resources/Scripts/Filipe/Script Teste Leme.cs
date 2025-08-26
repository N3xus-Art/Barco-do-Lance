using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class ScriptTesteLeme : MonoBehaviour
{
    bool EntrouNoLeme;

    SceneController sceneController;

     void OnTriggerEnter2D(Collider2D other)
    {

        EntrouNoLeme = true;
        Debug.Log("Entrou nessa desgraça");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        EntrouNoLeme = false;
        Debug.Log("Saiu nessa desgraça");

    }

    private void Update()
    {
        /*if (PlayerInputHandler.isInteracting && (EntrouNoLeme))
        {

            sceneController = new SceneController();
            sceneController.LoadScene("Scene Map", LoadSceneMode.Single);

        }*/
    }


}

