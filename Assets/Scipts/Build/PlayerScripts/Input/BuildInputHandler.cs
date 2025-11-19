using System.Linq;
using TMPro;
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
    [Header("---- Prefabs de Itens ----")]
    [SerializeField] private GameObject racaoPrefab;
    [SerializeField] public Animator anim;

    #endregion
    //Methods
    #region
    public void Menu(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Botão de Menu pressionado!");
            SceneManager.LoadScene("HomeScreen");
        }
    }
    public void Move(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Performed){
            horizontalMoviment = context.ReadValue<Vector2>().x;
            verticalMoviment = context.ReadValue<Vector2>().y;
            isMoving = true;
            anim.SetBool("isMoving", isMoving);
            if (Mathf.Abs(horizontalMoviment) > 0 && Mathf.Abs(horizontalMoviment) < 1){
                horizontalMoviment = 1 * Mathf.Sign(horizontalMoviment);
            }
            if (!isDiagonal && horizontalMoviment != 0){
                verticalMoviment = 0;
            }
        }else if (context.phase == InputActionPhase.Canceled){
            isMoving = false;
            anim.SetBool("isMoving", isMoving);
        }
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("Botão de interação pressionado!");
            // 1. Verifica todos os colliders na área de interação
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange, interactableLayer);

            if (hits.Length == 0)
            {
                Debug.Log("Nada interativo por perto.");
                // Verifica se está no mar e tem ração
                bool estaNaAgua = Physics2D.OverlapBox(transform.position, contactCheckSize, 0, waterLayer);

                if (estaNaAgua)
                {
                    // Usa a ração
                    bool success = PlayerInventoryHandler.Instance.TryUseRacao();
                    if (success)
                    {
                        if (racaoPrefab != null)
                        {
                            Instantiate(racaoPrefab, transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Debug.LogError("Prefab da Ração não está configurado no BuildInputHandler!");
                        }
                    }
                }
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
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isInteracting = false;
        }
    }

    public void UseFood(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // 1. Tenta consumir um item de ração do inventário
            bool success = PlayerInventoryHandler.Instance.TryUseRacao();

            // 2. Se foi bem-sucedido, instancia o prefab da ração na água
            if (success)
            {
                if (racaoPrefab != null)
                {
                    Instantiate(racaoPrefab, transform.position, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("Prefab da Ração não está configurado no BuildInputHandler!");
                }
            }
            else
            {
                Debug.Log("Sem ração para usar!");
                // Opcional: Tocar um som de "falha"
            }
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
