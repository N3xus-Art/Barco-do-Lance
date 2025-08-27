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
    [Header("----Boleanas----")]
    [SerializeField] private bool isMoving;
    [SerializeField] private bool isInteracting;
    [SerializeField] private bool isClicking;
    [Header("----Variaveis de Movimentação----")]
    [SerializeField] private float speed;
    [SerializeField] private float horizontalMoviment;
    [SerializeField] private float verticalMoviment;
    [SerializeField] private Rigidbody2D rb;
    [Header("----Verificação de Contato----")]
    [SerializeField] private Transform contactCheckPos;
    [SerializeField] private Vector2 contactCheckSize = new Vector2(0.13f, 0.05f);
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask waterLayer;
    #endregion
    //Input Methdos
    #region
    public void Move(InputAction.CallbackContext context){
       if(context.phase == InputActionPhase.Started){
       }else if (context.phase == InputActionPhase.Performed){
           isMoving = true;
           horizontalMoviment = context.ReadValue<Vector2>().x;
           verticalMoviment = context.ReadValue<Vector2>().y;
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
    #endregion
    //ContactCheck Methods
    #region
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(contactCheckPos.position, contactCheckSize);
    }
    private bool isGrounded(){
        if (Physics2D.OverlapBox(contactCheckPos.position, contactCheckSize, 0, groundLayer)){
            rb.gravityScale = 20;
            return true;
        }
        return false;
    }
    private bool inWater(){
        if (Physics2D.OverlapBox(contactCheckPos.position, contactCheckSize, 0, waterLayer)){
            rb.gravityScale = 0;
            return true;
        }
        return false;
    }
    #endregion
    //Unity Methods
    #region
    public void Awake(){
        speed = 5f;
        rb = this.GetComponent<Rigidbody2D>();
    }
    public void Update(){
        if (isMoving && isGrounded()){
            rb.linearVelocity = new Vector2(horizontalMoviment * speed, 0);
        }else if (isMoving && inWater()){
            rb.linearVelocity = new Vector2(horizontalMoviment * speed, verticalMoviment * speed);
        }else if (!isMoving){
            rb.linearVelocity = new Vector2(0, 0);
        }
    }
    #endregion
}