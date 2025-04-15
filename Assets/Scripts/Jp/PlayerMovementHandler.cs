using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementHandler : MonoBehaviour
{

    //Classe Move que contém a movimentação do jogador
    public void Move(InputAction.CallbackContext context){

        if(context.phase == InputActionPhase.Performed){
            Debug.Log("Move");
        }
        
        

    }

}
