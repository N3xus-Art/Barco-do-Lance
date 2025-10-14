using UnityEngine;
using TMPro;

public class MoneyUIHandler : MonoBehaviour
{
    public GameObject buildGameManager;
    public PlayerInventoryHandler playerInventoryHandler;
    public TextMeshProUGUI moneyText;

    private void OnEnable()
    {
        if (playerInventoryHandler != null)
            playerInventoryHandler.OnMoneyChanged += UpdateMoneyText;
    }

    private void OnDisable()
    {
        if (playerInventoryHandler != null)
            playerInventoryHandler.OnMoneyChanged -= UpdateMoneyText;
    }

    private void Start()
    {
        buildGameManager = GameObject.FindWithTag("GameManager");
        playerInventoryHandler = buildGameManager.GetComponent<PlayerInventoryHandler>();
        UpdateMoneyText(playerInventoryHandler.Money);
    }
    private void Update(){
        UpdateMoneyText(playerInventoryHandler.Money);
    }
    private void UpdateMoneyText(int currentMoney)
    {
        moneyText.text = $"Money: {currentMoney}";
    }
}
