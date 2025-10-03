using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class BuildInputHandler : MonoBehaviour {
    //Variables
    #region
    public static BuildInputHandler Instance { get; private set; }
    [Header("----Boleanas----")]
    [SerializeField] private bool isMoving;
    [SerializeField] static public bool isInteracting;
    [SerializeField] static public bool isInteracting2;
    [SerializeField] static public bool isChangingItem;
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
    [Header("----Variaveis do o2----")]
    [SerializeField] float rotation;
    [SerializeField] private float o2 = 100;
    [SerializeField] private int maxO2 = 100;
    [SerializeField] private float o2Cost;
    [SerializeField] private GameObject pointer;
    [Header("----Variaveis de sprite----")]
    [SerializeField] private Scene scene;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private Sprite spriteBase;
    [SerializeField] private Sprite spriteModel;
    [SerializeField] private Sprite spriteBoat;
    [Header("----Variaveis de Loja----")]
    [SerializeField] static public float playerMoney;
    [Header("----Variaveis Item----")]
    [SerializeField] private int itemIndex;
    [SerializeField] private GameObject buildGameManager;
    [SerializeField] private PlayerInventoryHandler playerInventory;
    [SerializeField] private ToolDataHandler pliersData;
    [SerializeField] private ToolDataHandler scissorsData;
    [SerializeField] private ToolDataHandler knifeData;
    #endregion
    //Input Methdos
    #region
    public void Move(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started) {
        } else if (context.phase == InputActionPhase.Performed) {
            horizontalMoviment = context.ReadValue<Vector2>().x;
            verticalMoviment = context.ReadValue<Vector2>().y;
            isMoving = true;
            if (Mathf.Abs(horizontalMoviment) > 0 && Mathf.Abs(horizontalMoviment) < 1) {
                horizontalMoviment = 1 * Mathf.Sign(horizontalMoviment);
            }
            if (!isDiagonal) {
                if (horizontalMoviment != 0) {
                    verticalMoviment = 0;
                }
            }
        } else if (context.phase == InputActionPhase.Canceled) {
            isMoving = false;
        }
    }
    public void Interact(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started) {
        } else if (context.phase == InputActionPhase.Performed) {
            if (isInteracting) {
                isInteracting = false;
            } else {
                isInteracting = true;
            }
        } else if (context.phase == InputActionPhase.Canceled) {
        }
    }
    public void Interact2(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started) {
        } else if (context.phase == InputActionPhase.Performed) {
            if (isInteracting) {
                isInteracting2 = false;
            } else {
                isInteracting2 = true;
            }
        } else if (context.phase == InputActionPhase.Canceled) {
        }
    }
    public void Click(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started) {
            isClicking = true;
        } else if (context.phase == InputActionPhase.Performed) {
        } else if (context.phase == InputActionPhase.Canceled) {
            isClicking = false;
        }
    }

    public void Dash(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed) {
            isDashing = true;
        } else if (context.phase == InputActionPhase.Canceled) {
            isDashing = false;
        }

    }

    public void ChangeItem(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started) {
            isChangingItem = true;
        } else if (context.phase == InputActionPhase.Performed) {
            playerInventory.EquipNext();
        } else if (context.phase == InputActionPhase.Canceled) {
            isChangingItem = false;
        }
    }

     void showInventory(){
        Debug.Log("\nLista de ferramentas:" + string.Join(", ", playerInventory.OwnedTools) +
                  "\nDinheiro:" + playerInventory.Money +
                  "\nFerramenta equipada: " + playerInventory.EquippedTool
                  );
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
        playerModel.GetComponent<SpriteRenderer>().sprite = spriteModel;
        buildGameManager = GameObject.FindWithTag("GameManager");
        playerInventory = buildGameManager.GetComponent<PlayerInventoryHandler>();
        if (scene.name == "MapScreen"){
            rb.gravityScale = 0f;
            playerModel.GetComponent<SpriteRenderer>().sprite = spriteBoat;
            playerModel.GetComponent <Transform>().localScale = new Vector3(0.15f, 0.15f, 0.15f);
            this.GetComponent<BoxCollider2D>().size = new Vector2(0.5189194f, 0.4115877f);
            this.GetComponent<BoxCollider2D>().offset = new Vector2(0.03019339f, 0.02307379f);
        }
    }
    public void Update(){
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
            if (!isGrounded() && !(inWater() || inLadder()) && scene.name != "MapScreen"){
                rb.linearVelocity = new Vector2(0, 0);
                rb.gravityScale = 20;
                if(horizontalMoviment != 0) {
                    gameObject.GetComponent<Transform>().localScale = new Vector3(horizontalMoviment, 1, 1);
                }
            }
            if (isMoving && scene.name == "MapScreen"){
                rb.linearVelocity = new Vector2(horizontalMoviment * speed, verticalMoviment * speed);
                rb.gravityScale = 0;
                if(horizontalMoviment != 0) {
                    gameObject.GetComponent<Transform>().localScale = new Vector3(horizontalMoviment, 1, 1);
                }
            }
        #endregion
        //Interact Update
        #region
            if (isInteracting2){
                bool success = playerInventory.TryUseEquipped();
                Debug.Log(success ? "Usou a ferramenta equipada" : "Sem ferramentas para usar!");
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
            pointer.GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, -rotation);
        #endregion
        //Item Update
        #region
            if (isChangingItem) { 
                
            }
        // M - Upgrade ferramenta equipada
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Tentando Upgrade ferramenta equipada");
            playerInventory.UpgradeEquippedTool();
            showInventory();
        }
        #endregion
        //Canvas Update
    }
    #endregion
}