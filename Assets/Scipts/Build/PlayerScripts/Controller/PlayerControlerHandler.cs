using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControlerHandler : MonoBehaviour{
    //Variables
    #region
    private static PlayerControlerHandler _instance;
    public static PlayerControlerHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("O BLabla no foi inicializado. Certifique-se de que est na cena!");
            }
            return _instance;
        }
    }
    [Header("----Variaveis de Controle----")]
    [SerializeField] public BuildInputHandler biHandler;
    [SerializeField] public GameManagerHandler gmHandler;
    [SerializeField] public CurrentMission CurrentMission;
    [SerializeField] public bool CanMove = true;
    [Header("----Variaveis do o2----")]
    [SerializeField] public float rotation;
    [SerializeField] public float o2 = 100;
    [SerializeField] public int maxO2 = 100;
    [SerializeField] public float o2Cost;
    [SerializeField] public bool isMap;

    // Referência do ponteiro agora é privada e encontrada dinamicamente
    private GameObject pointer;

    [Header("----Variaveis de sprite----")]
    [SerializeField] public GameObject playerModel;
    [SerializeField] public Sprite spriteBase;
    [SerializeField] public Sprite spriteModel;
    [SerializeField] public Sprite spriteBoat;
    [Header("----Variaveis de Loja----")]
    [SerializeField] static public float playerMoney;
    [Header("----Variaveis Item----")]
    [SerializeField] public int itemIndex;
    [SerializeField] public PlayerInventoryHandler playerInventory;
    [SerializeField] public ToolDataHandler pliersData;
    [SerializeField] public ToolDataHandler scissorsData;
    [SerializeField] public ToolDataHandler knifeData;
    [Header("----Variaveis de Morte----")]
    [SerializeField] private GameObject deathTag;
    [SerializeField] private TMP_Text Death_Text;
    #endregion

    //Methods
    #region
    //Contact Methods
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(biHandler.contactCheckPos.position, biHandler.contactCheckSize);
    }
    private bool isGrounded()
    {
        if (Physics2D.OverlapBox(biHandler.contactCheckPos.position, biHandler.contactCheckSize, 0, biHandler.groundLayer))
        {
            return true;
        }
        return false;
    }
    private bool inWater()
    {
        if (Physics2D.OverlapBox(biHandler.contactCheckPos.position, biHandler.contactCheckSize, 0, biHandler.waterLayer))
        {
            return true;
        }
        return false;
    }
    private bool inLadder()
    {
        if (Physics2D.OverlapBox(biHandler.contactCheckPos.position, biHandler.contactCheckSize, 0, biHandler.ladderLayer))
        {
            return true;
        }
        return false;
    }

    //Flip Methods
    public void FlipPlayerModel(){
        if (CanMove){
            if (biHandler.horizontalMoviment > 0)
            {
                playerModel.GetComponent<SpriteRenderer>().flipX = false;
                biHandler.contactCheckPos.localPosition = new Vector3(0.0234f, -0.431f, 0);
            }
            else if (biHandler.horizontalMoviment < 0)
            {
                playerModel.GetComponent<SpriteRenderer>().flipX = true;
                biHandler.contactCheckPos.localPosition = new Vector3(-0.0234f, -0.431f, 0);
            }
        }
    }
    public void RotateModel()
    {
        if (CanMove)
        {
            if (biHandler.horizontalMoviment > 0)
            {
                playerModel.GetComponent<SpriteRenderer>().transform.localRotation = Quaternion.Euler(0, 0, -90);
                biHandler.contactCheckPos.localPosition = new Vector3(0.0234f, -0.431f, 0);
            }
            else if (biHandler.horizontalMoviment < 0)
            {
                playerModel.GetComponent<SpriteRenderer>().transform.localRotation = Quaternion.Euler(0, 0, 90);
                biHandler.contactCheckPos.localPosition = new Vector3(-0.0234f, -0.431f, 0);
            }
        }
    }

    public void FlipVerticalModel(){
        if (CanMove)
        {
            if (biHandler.verticalMoviment > 0)
            {
                playerModel.GetComponent<SpriteRenderer>().transform.localRotation = Quaternion.Euler(0, 0, 0);
                biHandler.contactCheckPos.localPosition = new Vector3(0.0234f, -0.431f, 0);
            }
            else if (biHandler.verticalMoviment < 0)
            {
                playerModel.GetComponent<SpriteRenderer>().transform.localRotation = Quaternion.Euler(0, 0, 180);
                biHandler.contactCheckPos.localPosition = new Vector3(-0.0234f, -0.431f, 0);
            }
        }
    }

    //Initialize Methods
    public void LoadedSceneHandler(Scene sceneName, LoadSceneMode mode)
    {
        GameManagerHandler.CurrentScene = SceneManager.GetActiveScene();
        Initialize();
    }
    private void Initialize()
    {
        // Se estiver na cena do mapa
        if (GameManagerHandler.CurrentScene.name == "MapScreen")
        {
            gameObject.SetActive(true);
            AquariumHandler.Instance.gameObject.SetActive(false);
            biHandler.rb.gravityScale = 0f;
            spriteModel = spriteBoat;
            biHandler.anim.SetBool("isBoat", true);
            playerModel.GetComponent<SpriteRenderer>().sprite = spriteModel;
            playerModel.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            biHandler.inicialPos.localScale = new Vector3(5, 5, 5);
            biHandler.inicialPos.position = new Vector3(1.26f, 1, 0);
            biHandler.box2D.offset = new Vector2(0.0004896298f, -0.001247153f);
            biHandler.box2D.size = new Vector2(0.1741978f, 0.8615327f);
            biHandler.contactCheckSize = new Vector2(0.586f, 0.01f);
            biHandler.contactCheckPos.localPosition = new Vector3(0.0234f, -0.431f, 0);
            BuildInputHandler.isInteracting = false;
            CanMove = true;
            pointer = null;
            isMap = true;

        }
        else if (GameManagerHandler.CurrentScene.name == "HomeScreen")
        {
            biHandler.anim.SetBool("isSwim", false);
            gameObject.SetActive(false);
            AquariumHandler.Instance.gameObject.SetActive(false);
            biHandler.rb.gravityScale = 0f;
            spriteModel = spriteBase;
            biHandler.inicialPos.localScale = new Vector3(5, 5, 5);
            biHandler.inicialPos.position = new Vector3(1.26f, 1, 0);
            biHandler.box2D.offset = new Vector2(0.0004896298f, -0.001247153f);
            biHandler.box2D.size = new Vector2(0.1741978f, 0.8615327f);
            biHandler.contactCheckSize = new Vector2(0.586f, 0.01f);
            biHandler.contactCheckPos.localPosition = new Vector3(0.0234f, -0.431f, 0);
            CanMove = true;
            pointer = null;
            isMap = false;
        }
        // Se estiver na cena de jogo
        else if (GameManagerHandler.CurrentScene.name == "GameScreen")
        {
            gameObject.SetActive(true);
            AquariumHandler.Instance.gameObject.SetActive(true);
            biHandler.rb.gravityScale = 1f;
            spriteModel = spriteBase;
            biHandler.anim.SetBool("isBoat", false);
            playerModel.GetComponent<SpriteRenderer>().sprite = spriteModel;
            playerModel.transform.localScale = new Vector3(0.47f, 0.47f, 0.47f);
            biHandler.inicialPos.localScale = new Vector3(5, 5, 5);
            biHandler.inicialPos.position = new Vector3(1.26f, 1, 0);
            biHandler.box2D.offset = new Vector2(0.0004896298f, -0.001247153f);
            biHandler.box2D.size = new Vector2(0.1741978f, 0.8615327f);
            biHandler.contactCheckSize = new Vector2(0.586f, 0.01f);
            biHandler.contactCheckPos.localPosition = new Vector3(0.0234f, -0.431f, 0);
            deathTag = GameObject.FindWithTag("DeathTag");
            Death_Text = deathTag.GetComponent<TMP_Text>();
            CanMove = true;
            isMap = false;
            playerModel.GetComponent<SpriteRenderer>().transform.localRotation = Quaternion.Euler(0, 0, 0);

            // Tenta encontrar o ponteiro de O2 na cena
            O2PointerTag pointerTag = FindObjectOfType<O2PointerTag>(true);
            if (pointerTag != null)
            {
                pointer = pointerTag.gameObject;
                //Debug.Log("Ponteiro de O2 encontrado e atribuído!");
            }
            else
            {
                //Debug.LogWarning("PlayerControlerHandler: NÃO FOI POSSÍVEL ENCONTRAR o objeto com a tag 'O2PointerTag' na cena!");
                pointer = null;
            }

            UpdateO2Pointer();
        }
        else
        {
            pointer = null;
        }
    }

    //Unity Methods
    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        CanMove = true;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += LoadedSceneHandler;
        biHandler.speed = 5f;
        biHandler.rb = GetComponent<Rigidbody2D>();
        playerModel.GetComponent<SpriteRenderer>().sprite = spriteModel;
        biHandler.box2D = GetComponent<BoxCollider2D>();
    }

    private void UpdateO2Pointer()
    {
        // Define os ângulos mínimo e máximo do ponteiro
        float maxAngle = -163f;  // Exemplo: ponteiro para direita (O2 cheio)
        float minAngle = 163f; // Exemplo: ponteiro para esquerda (O2 zerado)

        // Calcula a porcentagem de O2
        float o2Percent = o2 / maxO2;

        // Interpola o ângulo conforme o O2
        float pointerAngle = Mathf.Lerp(minAngle, maxAngle, o2Percent);

        // Aplica a rotação ao ponteiro
        if (pointer != null)
        {
            RectTransform pointerRect = pointer.GetComponent<RectTransform>();
            if (pointerRect != null)
            {
                pointerRect.localEulerAngles = new Vector3(0, 0, pointerAngle);
            }
            else
            {
                // Fallback para caso não seja um objeto de UI (RectTransform)
                pointer.transform.localEulerAngles = new Vector3(0, 0, pointerAngle);
            }
        }
    }

    public void Update()
    {
        if (BuildInputHandler.isMoving && CanMove)
        {
            if (isGrounded() && !biHandler.topCollision.inLadderLocal)
            {
                biHandler.rb.linearVelocity = new Vector2(biHandler.horizontalMoviment * biHandler.speed, 0);
                biHandler.rb.gravityScale = 1;
                biHandler.box2D.isTrigger = false;
                biHandler.anim.SetBool("isMoving", true);
                if (!isMap){
                    FlipPlayerModel();
                }else{
                    RotateModel();
                    FlipVerticalModel();
                }
            }
            else if (inWater() || inLadder())
            {
                biHandler.rb.linearVelocity = new Vector2(biHandler.horizontalMoviment * biHandler.speed, biHandler.verticalMoviment * biHandler.speed);
                biHandler.rb.gravityScale = 0;
                biHandler.box2D.isTrigger = true;
                biHandler.anim.SetBool("isMoving", false);
                if (!isMap){
                    FlipPlayerModel();
                }else{
                    RotateModel();
                    FlipVerticalModel();
                }
            }
            else if (biHandler.topCollision.inLadderLocal)
            {
                biHandler.rb.linearVelocity = new Vector2(biHandler.horizontalMoviment * biHandler.speed, biHandler.verticalMoviment * biHandler.speed);
                biHandler.rb.gravityScale = 0;
                biHandler.box2D.isTrigger = false;
                if (!isMap){
                    FlipPlayerModel();
                }else{
                    RotateModel();
                    FlipVerticalModel();
                }
            }
        }
        if (!BuildInputHandler.isMoving)
        {
            biHandler.rb.linearVelocity = new Vector2(0, 0);
            biHandler.rb.gravityScale = 0;
            if (!isMap){
                 FlipPlayerModel();
            }else{
                RotateModel();
                FlipVerticalModel();
             }
        }
        if (!isGrounded() && !inWater() && !inLadder())
        {
            biHandler.rb.linearVelocity = new Vector2(0, 0);
            biHandler.rb.gravityScale = 1f;
            biHandler.box2D.isTrigger = false;
            if (!isMap)
            {
                FlipPlayerModel();
            }
            else
            {
                RotateModel();
                FlipVerticalModel();
            }
        }
        if (inWater())
        {
            biHandler.anim.SetBool("isSwim", true);
            biHandler.box2D.offset = new Vector2(-0.00432229f, 0.02040866f);
            biHandler.box2D.size = new Vector2(1.074133f, 0.6016592f);
            if (o2 > 0)
            {
                o2 -= o2Cost * Time.deltaTime;
                o2 = Mathf.Clamp(o2, 0, maxO2);

                if (o2 <= 0) { 
                    Debug.Log(GameManagerHandler.Instance.FadeHandler);
                    GameManagerHandler.Instance.FadeHandler.FadeOut(5f,Morrer); }
            }
                UpdateO2Pointer();
        }
        if (!inWater()) {
            biHandler.anim.SetBool("isSwim", false);
            biHandler.box2D.offset = new Vector2(0.0004896298f, -0.001247153f);
            biHandler.box2D.size = new Vector2(0.1741978f, 0.8615327f);
        }
        if (inWater() && inLadder()) {
            biHandler.anim.SetBool("isSwim", false);
        }
        if (BuildInputHandler.isStarted)
        {
            playerInventory.TryUseEquipped();
            BuildInputHandler.isStarted = false;
        }

        if (BuildInputHandler.isTertriary)
        {
            playerInventory.EquipNext();
            BuildInputHandler.isTertriary = false;
        }
    }


    public void Morrer()
    {
        // desabilita a o movimento e o interagir
        CanMove = false;
        biHandler.enabled = false;

        StartCoroutine(CutsceneMorte());
    }


    private IEnumerator CutsceneMorte()
    {
        // espera meio segundo
        yield return new WaitForSecondsRealtime(0.5f);

        // habilita o texto de "Desmaiou"
        Death_Text.enabled = true;

        // espera 4s
        yield return new WaitForSecondsRealtime(4f);

        // desabilita o texto de "Desmaiou"
        Death_Text.enabled = false;

        Initialize();

        yield return new WaitForSecondsRealtime(0.2f);


        GameManagerHandler.Instance.FadeHandler.FadeIn(1.5f);

    }

    #endregion
}