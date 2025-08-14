using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMapHandler : MonoBehaviour
{

    string Index = "";

    SceneController sceneController;

    void OnTriggerEnter2D(Collider2D other)
    {

        Index = other.name;

    }

    void OnTriggerExit2D(Collider2D other)
    {

        Index = "";

    }

    public void ChangeScene(InputAction.CallbackContext context) 
    {

       if (context.phase == InputActionPhase.Started) 
       {
        
            if (Index != "") {

                sceneController = new SceneController();
                sceneController.LoadScene(Index, LoadSceneMode.Additive);

            }
            
        
       }
    
    }

}
