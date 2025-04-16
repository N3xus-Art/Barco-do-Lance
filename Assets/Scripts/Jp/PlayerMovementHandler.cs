using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField] float velocity;
    bool isMoving;
    bool isInteracting;
    Vector2 direction;


    //Classe Move que contém a movimentação do jogador
    public void Move(InputAction.CallbackContext context){
        
        if(context.phase == InputActionPhase.Started){
           
        }else if(context.phase == InputActionPhase.Performed){
            isMoving = true;
            direction = context.ReadValue<Vector2>();
            
        
        }else if(context.phase == InputActionPhase.Canceled){
            isMoving = false;       
        
        }
    }

    public void Interact(InputAction.CallbackContext context){

        if(context.phase == InputActionPhase.Started){
           
        }else if(context.phase == InputActionPhase.Performed){
            isInteracting = true;
            
        }else if(context.phase == InputActionPhase.Canceled){
            isInteracting = false;       
        }
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * velocity;
        }
        
        if(isInteracting){
            Debug.Log("Foi");

        }

    }


}
