using System.Linq;
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
    [SerializeField] static public bool isStarted;
    [SerializeField] static public bool isTertriary;
    [SerializeField] static public bool isChangingItem;
    [SerializeField] static public bool isClicking;
    [SerializeField] static public bool isDashing;
    [SerializeField] static public bool isDiagonal;
    [Header("----Variaveis de Movimenta��o----")]
    [SerializeField] public float speed;
    [SerializeField] public float horizontalMoviment;
    [SerializeField] public float verticalMoviment;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public BoxCollider2D box2D;
    [Header("----Verifica��o de Contato----")]
    [SerializeField] public Transform inicialPos;
    [SerializeField] public Transform contactCheckPos;
    [SerializeField] public GameObject contactCheckTop;
    [SerializeField] public topCollisionHandler topCollision;
    [SerializeField] public Vector2 contactCheckSize;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public LayerMask waterLayer;
    [SerializeField] public LayerMask ladderLayer;
    [Header("---- Configuração de Interação ----")]
    [SerializeField] private float interactRange = 5.0f;
    [Tooltip("Quais layers o sistema de interação deve verificar")]
    [SerializeField] private LayerMask interactableLayer;
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
    public void Interact(InputAction.CallbackContext context)
    {
        // Reage quando o botão é pressionado (Performed)
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Botão de interação pressionado!");
            // 1. Verifica todos os colliders na área de interação
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange, interactableLayer);

            if (hits.Length == 0)
            {
                Debug.Log("Nada interativo por perto.");
                return;
            }

            // 2. Encontra o collider mais próximo do jogador
            Collider2D closestHit = hits
                .OrderBy(hit => Vector2.Distance(transform.position, hit.transform.position))
                .FirstOrDefault();

            if (closestHit == null) return;

            // 3. Tenta pegar o script IInteractable no objeto encontrado
            IInteractable interactableObject = closestHit.GetComponentInParent<IInteractable>();

            // 4. Se o script existir, chama o método Interact
            if (interactableObject != null)
            {
                Debug.Log($"Interagindo com: {closestHit.gameObject.name}");
                interactableObject.Interact();
            }
            else
            {
                Debug.LogWarning($"Objeto {closestHit.gameObject.name} está na layer Interagível, mas não tem um script IInteractable!");
            }
            isInteracting = true;
        }else if (context.phase == InputActionPhase.Canceled){
            isInteracting = false;
        }
    }
    public void Tertriary(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            isTertriary = true;
        }else if (context.phase == InputActionPhase.Canceled){
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
