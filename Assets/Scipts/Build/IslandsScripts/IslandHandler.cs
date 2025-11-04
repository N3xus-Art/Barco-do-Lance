using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IslandHandler : MonoBehaviour {
    //Variables
    #region
    [Header("----Variaveis de Trigger----")]
    [SerializeField] private bool ison;
    [SerializeField] private PlayerInventoryHandler playerInventory;
    [Header("----Variaveis de Canva----")]
    [SerializeField] private GameObject shopGO;
    [SerializeField] private GameObject normalScreen;
    [SerializeField] private GameObject confirmScreen;
    [SerializeField] private GameObject poorScreen;
    [SerializeField] private Button closeButton;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private int itemSlot;
    [Header("----Variaveis do primeiro item----")]
    [SerializeField] private string firstItemName;
    [SerializeField] private int firstItemPrice;
    [SerializeField] private GameObject firstGameObject;
    [SerializeField] private ToolDataHandler firstData;
    [SerializeField] private TMP_Text firstItemText;
    [SerializeField] private Sprite firstSprite;
    [Header("----Variaveis do segundo item----")]
    [SerializeField] private string secondItemName;
    [SerializeField] private int secondItemPrice;
    [SerializeField] private GameObject secondGameObject;
    [SerializeField] private ToolDataHandler secondData;
    [SerializeField] private TMP_Text secondItemText;
    [SerializeField] private Sprite secondSprite;
    [Header("----Variaveis do terceiro item----")]
    [SerializeField] private string thirdItemName;
    [SerializeField] private int thirdItemPrice;
    [SerializeField] private GameObject thirdGameObject;
    [SerializeField] private ToolDataHandler thirdData;
    [SerializeField] private TMP_Text thirdItemText;
    [SerializeField] private Sprite thirdSprite;
    [Header("----Variaveis dos Botões----")]
    [SerializeField] private Button firstButton;
    [SerializeField] private Button secoundButton;
    [SerializeField] private Button thirdButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button denyButton;
    #endregion
    //Methods
    #region
    //Trigger Methods
    void OnTriggerEnter2D(Collider2D collider2D) {
        ison = true;
    }

    void OnTriggerExit2D(Collider2D collider2D) {
        ison = false;
    }

    //Button Methods
    public void OnCloseClick() {
        ison = false;
        shopGO.SetActive(false);
        Time.timeScale = 1;
    }

    //Confirms
    private void OnTabClick(int TabIndex) {
        if (playerInventory.Money < firstItemPrice || playerInventory.Money < secondItemPrice || playerInventory.Money < thirdItemPrice) {
            poorScreen.SetActive(true);
        }
        itemSlot = TabIndex;
        confirmScreen.SetActive(true);
    }
    public bool ConfirmCheck(int price) {
        if (itemSlot == 0) {
            if (playerInventory.TryBuyTool(firstData, out var tool)) {
                Debug.Log("Comprou " + firstData.toolName);
            } else {
                Debug.Log("Não conseguiu comprar o Alicate");
            }
        } else if (itemSlot == 1) {
            if (playerInventory.TryBuyTool(secondData, out var tool)) {
                Debug.Log("Comprou " + secondData.toolName);
            } else {
                Debug.Log("Não conseguiu comprar o Alicate");
            }
        } else if (itemSlot == 2) {
            if (playerInventory.TryBuyTool(thirdData, out var tool)) {
                Debug.Log("Comprou " + thirdData.toolName);
            } else {
                Debug.Log("Não conseguiu comprar o Alicate");
            }
        }
        confirmScreen.SetActive(false);
        return true;
    }
    public void OnDeny() {
        confirmScreen.SetActive(false);
    }

    public void OnConfirm() {
        if (itemSlot == 0) {
            ConfirmCheck(firstItemPrice);
        } else if (itemSlot == 1) {
            ConfirmCheck(secondItemPrice);
        } else if (itemSlot == 2) {
            ConfirmCheck(thirdItemPrice);
        }
        itemSlot = 4;
    }

    //Unity Methods
    private void Start() {
        //Button Starters
        Button closeTab = closeButton.GetComponent<Button>();
        Button firstTab = firstButton.GetComponent<Button>();
        Button secoundTab = secoundButton.GetComponent<Button>();
        Button thirdTab = thirdButton.GetComponent<Button>();
        Button confirmTab = confirmButton.GetComponent<Button>();
        Button denyTab = denyButton.GetComponent<Button>();
        closeTab.onClick.AddListener(OnCloseClick);
        firstTab.onClick.AddListener(() => { OnTabClick(0); });
        secoundTab.onClick.AddListener(() => { OnTabClick(1); });
        thirdTab.onClick.AddListener(() => { OnTabClick(2); });
        confirmTab.onClick.AddListener(OnConfirm);
        denyTab.onClick.AddListener(OnDeny);
        //Item Startres
        firstItemText.SetText($"{firstItemName} : {firstItemPrice}");
        secondItemText.SetText($"{secondItemName} : {secondItemPrice}");
        thirdItemText.SetText($"{thirdItemName} : {thirdItemPrice}");
        firstGameObject.GetComponent<Image>().sprite = firstSprite;
        secondGameObject.GetComponent<Image>().sprite = secondSprite;
        thirdGameObject.GetComponent<Image>().sprite = thirdSprite;
    }
    private void Update() {
        if (BuildInputHandler.isInteracting && ison) {
            Time.timeScale = 0;
            shopGO.SetActive(true);
            normalScreen.SetActive(true);
        }
        moneyText.SetText("Dinheiro: " + playerInventory.Money);
        if (ison == false) {
            shopGO.SetActive(false);
            Time.timeScale = 1;
        }
        if(playerInventory == null) {
            playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHandler>();
        }
    }
    #endregion
}
