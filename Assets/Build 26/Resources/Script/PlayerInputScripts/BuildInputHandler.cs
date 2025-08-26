using JetBrains.Annotations;
using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BuildInputHandler : MonoBehaviour{
    //Variables
    #region
    [SerializeField] private bool isMoving, isInteracting, isClicking;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector2 direction;
    #endregion

    //Methods
    #region
    public void Move(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Started){
        }else if (context.phase == InputActionPhase.Performed){
            isMoving = true;
            direction = context.ReadValue<Vector2>();
        }else if(context.phase == InputActionPhase.Canceled){
            isMoving = false;
        }
    }

    public void Interact(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Started){
        }else if (context.phase == InputActionPhase.Performed){
            isInteracting = true;
        }else if(context.phase == InputActionPhase.Canceled){
            isInteracting = false;
        }
    }

    public void Click(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Started){
        }else if (context.phase == InputActionPhase.Performed){
            isClicking = true;
        }else if(context.phase == InputActionPhase.Canceled){
            isClicking = false;
        }
    }

    public void Update(){
        if (isMoving) {
            transform.position += new Vector3(direction.x, 0, 0) * Time.deltaTime * speed;
        }else if (isMoving && WaterHandler.onWater){
            transform.position += new Vector3(direction.x, direction.y, 0) * Time.deltaTime * speed;
        }
    }
    #endregion

}
