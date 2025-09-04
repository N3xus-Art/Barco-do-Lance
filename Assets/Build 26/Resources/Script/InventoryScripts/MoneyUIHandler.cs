using UnityEngine;
using TMPro;

public class MoneyUIHandler : MonoBehaviour
{
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
        UpdateMoneyText(playerInventoryHandler.Money);
    }

    private void UpdateMoneyText(int currentMoney)
    {
        moneyText.text = $"Money: {currentMoney}";
    }
}
