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
    [SerializeField] static public bool isDashing;
    [SerializeField] static public bool isDiagonal;
    [Header("----Variaveis de Movimentação----")]
    [SerializeField] public float speed;
    [SerializeField] private float horizontalMoviment;
    [SerializeField] private float verticalMoviment;
    [SerializeField] private Rigidbody2D rb;
    [Header("----Verificação de Contato----")]
    [SerializeField] private Transform contactCheckPos;
    [SerializeField] private Vector2 contactCheckSize = new Vector2(0.43f, 0.14f);
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask waterLayer;
    [SerializeField] private LayerMask ladderLayer;
    [Header("----Verificação do o2----")]
    [SerializeField] float rotation;
    [SerializeField] private float o2 = 100;
    [SerializeField] private int maxO2 = 100;
    [SerializeField] private float o2Cost;
    [SerializeField] private GameObject pointer;
    [SerializeField] private Scene scene;

    #endregion
    //Input Methdos
    #region
    public void Move(InputAction.CallbackContext context){
       if(context.phase == InputActionPhase.Started){
       }else if (context.phase == InputActionPhase.Performed){
            horizontalMoviment = context.ReadValue<Vector2>().x;
            verticalMoviment = context.ReadValue<Vector2>().y;
            isMoving = true;
            if (Mathf.Abs(horizontalMoviment) > 0 && Mathf.Abs(horizontalMoviment) < 1){
                horizontalMoviment = 1 * Mathf.Sign(horizontalMoviment);
            }
            if (!isDiagonal) {
                if (horizontalMoviment != 0){
                    verticalMoviment = 0;
                }
            }
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

    public void Dash(InputAction.CallbackContext context){
        if(context.phase == InputActionPhase.Performed){
            isDashing = true;
        }else if (context.phase == InputActionPhase.Canceled){
            isDashing= false;
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
    private bool inLadder(){
        if (Physics2D.OverlapBox(contactCheckPos.position, contactCheckSize, 0, ladderLayer)){
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
        scene = SceneManager.GetActiveScene();
    }
    public void Update(){
        if (scene.name == "MapScreen" && pointer == null){
            return;
        }
        if (scene.name == "MapScreen"){
            rb.gravityScale = 0f;
        }
        //Moviment Update
        #region
        if (isMoving && isGrounded()){
                rb.linearVelocity = new Vector2(horizontalMoviment * speed, 0);
                rb.gravityScale = 20;
                if(horizontalMoviment != 0) {
                    gameObject.GetComponent<Transform>().localScale = new Vector3(horizontalMoviment, 1, 1);
                }
            }
            if (isMoving && (inWater() || inLadder())){
                rb.linearVelocity = new Vector2(horizontalMoviment * speed, verticalMoviment * speed);
                rb.gravityScale = 0;
                if(horizontalMoviment != 0) {
                    gameObject.GetComponent<Transform>().localScale = new Vector3(horizontalMoviment, 1, 1);
                }
            }
            if (!isMoving){
                rb.linearVelocity = new Vector2(0, 0);
                rb.gravityScale = 0;
                if(horizontalMoviment != 0) {
                    gameObject.GetComponent<Transform>().localScale = new Vector3(horizontalMoviment, 1, 1);
                }
            }
            if (!isGrounded() && !(inWater() || inLadder())){
                rb.linearVelocity = new Vector2(0, 0);
                rb.gravityScale = 20;
                if(horizontalMoviment != 0) {
                    gameObject.GetComponent<Transform>().localScale = new Vector3(horizontalMoviment, 1, 1);
                }
            }
        #endregion
        //Interact Update
        if (isInteracting){
            }
        //Click Update
            if (isClicking){
            }
        //Dashing Update
            if (isDashing && inWater()){
                if (horizontalMoviment != 0 && o2 > 0) {
                    gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + (5 * Time.deltaTime * Mathf.Sign(horizontalMoviment)), gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
                    o2 -= o2Cost;
                }
                if (verticalMoviment != 0 && o2 > 0){
                    gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + (5 * Time.deltaTime * Mathf.Sign(verticalMoviment)), gameObject.transform.localPosition.z);
                    o2 -= o2Cost;
                }
        }
        //o2 System
            if (o2 > 0 && inWater()){
                o2 -= 1 * Time.deltaTime;
            }
            rotation = -160f + (o2 / maxO2) * (160f - (-160f));
            // tank full oxigen 160, zero oxigen -160
            pointer.GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, -rotation);
    }
    #endregion
}