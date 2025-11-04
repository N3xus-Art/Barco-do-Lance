using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class BuildInputHandler : MonoBehaviour{
    //Variables
    #region
    [Header("----Boleanas----")]
    [SerializeField] static public bool isMoving;
    [SerializeField] static public bool isInteracting;
    [SerializeField] static public bool isTertriary;
    [SerializeField] static public bool isChangingItem;
    [SerializeField] static public bool isClicking;
    [SerializeField] static public bool isDashing;
    [SerializeField] static public bool isDiagonal;
    [Header("----Variaveis de Movimentação----")]
    [SerializeField] public float speed;
    [SerializeField] public float horizontalMoviment;
    [SerializeField] public float verticalMoviment;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public BoxCollider2D box2D;
    [Header("----Verificação de Contato----")]
    [SerializeField] public Transform inicialPos;
    [SerializeField] public Transform contactCheckPos;
    [SerializeField] public GameObject contactCheckTop;
    [SerializeField] public topCollisionHandler topCollision;
    [SerializeField] public Vector2 contactCheckSize;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public LayerMask waterLayer;
    [SerializeField] public LayerMask ladderLayer;
    #endregion
    //Methods
    #region
    public void Move(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            horizontalMoviment = context.ReadValue<Vector2>().x;
            verticalMoviment = context.ReadValue<Vector2>().y;
            isMoving = true;
            if (Mathf.Abs(horizontalMoviment) > 0 && Mathf.Abs(horizontalMoviment) < 1){
                horizontalMoviment = 1 * Mathf.Sign(horizontalMoviment);
            }
            if (!isDiagonal && horizontalMoviment != 0){
                verticalMoviment = 0;
            }
        }else if (context.phase == InputActionPhase.Canceled){
            isMoving = false;
        }
    }
    public void Interact(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            isInteracting = true;
        }else if (context.phase == InputActionPhase.Canceled){
            isInteracting = false;
        }
    }
    public void Tertriary(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            isTertriary = true;
        }else if (context.phase == InputActionPhase.Canceled){
            isTertriary = false;
        }
    }
    public void Click (InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            isClicking = true;
        }else if (context.phase == InputActionPhase.Canceled){
            isClicking = false;
        }
    }
    public void Dash(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            isDashing = true;
        }else if (context.phase == InputActionPhase.Canceled){
            isDashing = false;
        }
    }
    #endregion
}
