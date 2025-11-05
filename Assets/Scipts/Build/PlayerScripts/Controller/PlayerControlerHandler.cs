using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerControlerHandler : MonoBehaviour{
    //Variables
    #region
    private static PlayerControlerHandler _instance;
    public static PlayerControlerHandler Instance{
        get{
            if (_instance == null){
                Debug.LogError("O BLabla não foi inicializado. Certifique-se de que está na cena!");
            }
            return _instance;
        }
    }
    [Header("----Variaveis de Controle----")]
    [SerializeField] public BuildInputHandler biHandler;
    [SerializeField] public GameManagerHandler gmHandler;
    [SerializeField] public CurrentMission CurrentMission;
    [Header("----Variaveis do o2----")]
    [SerializeField] public float rotation;
    [SerializeField] public float o2 = 100;
    [SerializeField] public int maxO2 = 100;
    [SerializeField] public float o2Cost;
    [SerializeField] public GameObject pointer;
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
    #endregion
    //Methods
    #region
    //Contact Methods
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(biHandler.contactCheckPos.position, biHandler.contactCheckSize);
    }
    private bool isGrounded(){
        if (Physics2D.OverlapBox(biHandler.contactCheckPos.position, biHandler.contactCheckSize, 0, biHandler.groundLayer)){
            return true;
        }
        return false;
    }
    private bool inWater(){
        if (Physics2D.OverlapBox(biHandler.contactCheckPos.position, biHandler.contactCheckSize, 0, biHandler.waterLayer)){
            return true;
        }
        return false;
    }
    private bool inLadder(){
        if (Physics2D.OverlapBox(biHandler.contactCheckPos.position, biHandler.contactCheckSize, 0, biHandler.ladderLayer)){
            return true;
        }
        return false;
    }

    //Flip Methods
    public void FlipPlayerModel(){
        if (biHandler.horizontalMoviment > 0){
            playerModel.GetComponent<SpriteRenderer>().flipX = false;
            biHandler.contactCheckPos.localPosition = new Vector3(0.0234f, -0.431f, 0);
        }
        else if (biHandler.horizontalMoviment < 0){
            playerModel.GetComponent<SpriteRenderer>().flipX = true;
            biHandler.contactCheckPos.localPosition = new Vector3(-0.0234f, -0.431f, 0);
        }
    }

    //Initialize Methods
    public void LoadedSceneHandler(Scene sceneName, LoadSceneMode mode){
        GameManagerHandler.CurrentScene = SceneManager.GetActiveScene();
        Initialize();
    }
    private void Initialize(){
        if (GameManagerHandler.CurrentScene.name == "MapScreen"){
            biHandler.rb.gravityScale = 0f;
            spriteModel = spriteBoat;
            biHandler.inicialPos.localScale = new Vector3(5, 5, 5);
            biHandler.inicialPos.position = new Vector3(1.26f, 1, 0);
            biHandler.box2D.offset = new Vector2(0.0004896298f, -0.001247153f);
            biHandler.box2D.size = new Vector2(0.1741978f, 0.8615327f);
            biHandler.contactCheckSize = new Vector2(0.586f, 0.01f);
            biHandler.contactCheckPos.localPosition = new Vector3(0.0234f, -0.431f, 0);
        } else if (GameManagerHandler.CurrentScene.name == "GameScreen"){
            biHandler.rb.gravityScale = 1f;
            spriteModel = spriteBase;
            biHandler.inicialPos.localScale = new Vector3(5, 5, 5);
            biHandler.inicialPos.position = new Vector3(1.26f, 1, 0);
            biHandler.box2D.offset = new Vector2(0.0004896298f, -0.001247153f);
            biHandler.box2D.size = new Vector2(0.1741978f, 0.8615327f);
            biHandler.contactCheckSize = new Vector2(0.586f, 0.01f);
            biHandler.contactCheckPos.localPosition = new Vector3(0.0234f, -0.431f, 0);
        }
    }

    //Unity Methods
    public void Awake(){
        if (_instance != null && _instance != this){
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += LoadedSceneHandler;
        biHandler.speed = 5f;
        biHandler.rb = GetComponent<Rigidbody2D>();
        playerModel.GetComponent<SpriteRenderer>().sprite = spriteModel;
        biHandler.box2D = GetComponent<BoxCollider2D>();
    }

    public void Update(){
        if (BuildInputHandler.isMoving){
            if (isGrounded() && !biHandler.topCollision.inLadderLocal){
                biHandler.rb.linearVelocity = new Vector2(biHandler.horizontalMoviment * biHandler.speed, 0);
                biHandler.rb.gravityScale = 1;
                biHandler.box2D.isTrigger = false;
                FlipPlayerModel();
            }else if (inWater() || inLadder()){
                biHandler.rb.linearVelocity = new Vector2(biHandler.horizontalMoviment * biHandler.speed, biHandler.verticalMoviment * biHandler.speed);
                biHandler.rb.gravityScale = 0;
                biHandler.box2D.isTrigger = true;
                FlipPlayerModel();
            }else if (biHandler.topCollision.inLadderLocal){
                biHandler.rb.linearVelocity = new Vector2(biHandler.horizontalMoviment * biHandler.speed, biHandler.verticalMoviment * biHandler.speed);
                biHandler.rb.gravityScale = 0;
                biHandler.box2D.isTrigger = false;
                FlipPlayerModel();
            }
        }
        if (!BuildInputHandler.isMoving){
            biHandler.rb.linearVelocity = new Vector2(0, 0);
            biHandler.rb.gravityScale = 0;
            FlipPlayerModel();
        }
        if (!isGrounded() && !inWater() && !inLadder()){
            biHandler.rb.linearVelocity = new Vector2(0, 0);
            biHandler.rb.gravityScale = 1f;
            biHandler.box2D.isTrigger = false;
            FlipPlayerModel();
        }

        /*
        if (BuildInputHandler.isMoving && GameManagerHandler.CurrentScene.name == "MapScreen"){
            biHandler.rb.linearVelocity = new Vector2(biHandler.horizontalMoviment * biHandler.speed, biHandler.verticalMoviment * biHandler.speed);
            biHandler.rb.gravityScale = 0;
            if (biHandler.horizontalMoviment != 0)
            {
                gameObject.GetComponent<Transform>().localScale = new Vector3(biHandler.horizontalMoviment, 1, 1);
            }
        }*/
    }
    #endregion
}
