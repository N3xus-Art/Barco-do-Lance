using UnityEngine;
using UnityEngine.InputSystem;

public class Moving : MonoBehaviour{

    private bool isMoving; 
    static public bool isInteractingGlobal;
    private Vector2 direction;
    private float speed;

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

        if(context.phase == InputActionPhase.Performed){
           isInteractingGlobal = true;
        }else if(context.phase == InputActionPhase.Canceled){
            isInteractingGlobal = false;       
        }
    }

    void Start(){
        speed = 5f;
    }

    void Update(){
        if(isMoving){
             transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * speed;
        }
    }
}
