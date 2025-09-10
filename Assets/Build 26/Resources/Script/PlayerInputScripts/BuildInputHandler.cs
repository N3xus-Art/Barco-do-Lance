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
    [SerializeField] static public bool isInteracting;
    [SerializeField] static public bool isClicking;
    [Header("----Variaveis de Movimentação----")]
    [SerializeField] public float speed;
    [SerializeField] private float horizontalMoviment;
    [SerializeField] private float verticalMoviment;
    [SerializeField] private Rigidbody2D rb;
    /*[Header("----Variaveis da Escada----")]
    [SerializeField] private float vertical;
    [SerializeField] private bool isLadder, isClimbing;*/
    [Header("----Verificação de Contato----")]
    [SerializeField] private Transform contactCheckPos;
    [SerializeField] private Vector2 contactCheckSize = new Vector2(0.43f, 0.14f);
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask waterLayer;

    #endregion
    //Input Methdos
    #region
    public void Move(InputAction.CallbackContext context){
       if(context.phase == InputActionPhase.Started){
        }else if (context.phase == InputActionPhase.Performed){
            horizontalMoviment = context.ReadValue<Vector2>().x;
            verticalMoviment = context.ReadValue<Vector2>().y;
            isMoving = true;
       }else if(context.phase == InputActionPhase.Canceled){
           isMoving = false;
       }
    }
    public void Interact(InputAction.CallbackContext context){
       if(context.phase == InputActionPhase.Started){
       }else if (context.phase == InputActionPhase.Performed){
            if (isInteracting){
                isInteracting = false;
            }else{
                isInteracting = true;
            }
       }else if(context.phase == InputActionPhase.Canceled){
       }
    }
    public void Click(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Started){
            isClicking = true;
        }else if (context.phase == InputActionPhase.Performed){
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
            return true;
        }
        return false;
    }
    private bool inWater(){
        if (Physics2D.OverlapBox(contactCheckPos.position, contactCheckSize, 0, waterLayer)){
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
        //Moviment Update
        if (isMoving && isGrounded()){
            rb.linearVelocity = new Vector2(horizontalMoviment * speed, 0);
            rb.gravityScale = 20;
        }
        if (isMoving && inWater()){
            rb.linearVelocity = new Vector2(horizontalMoviment * speed, verticalMoviment * speed);
            rb.gravityScale = 0;
        }
        if (!isMoving){
            rb.linearVelocity = new Vector2(0, 0);
            rb.gravityScale = 0;
        }
        if (!isGrounded() && !inWater()){
            rb.linearVelocity = new Vector2(0, 0);
            rb.gravityScale = 20;
        }
        //Interact Update
        if (isInteracting){
        }
        //Click Update
        if (isClicking){
        }
    }
    #endregion
}