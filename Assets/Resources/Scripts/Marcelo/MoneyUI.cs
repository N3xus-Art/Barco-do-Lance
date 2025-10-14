using UnityEngine;
using TMPro; 

public class MoneyUI : MonoBehaviour
{
    public PlayerInventory playerInventory; 
    public TextMeshProUGUI moneyText;      

    private void OnEnable()
    {
        if (playerInventory != null)
            playerInventory.OnMoneyChanged += UpdateMoneyText;
    }

    private void OnDisable()
    {
        if (playerInventory != null)
            playerInventory.OnMoneyChanged -= UpdateMoneyText;
    }

    private void Start()
    {
        UpdateMoneyText(playerInventory.Money);
    }

    private void UpdateMoneyText(int currentMoney)
    {
        moneyText.text = $"Money: {currentMoney}";
    }
}
