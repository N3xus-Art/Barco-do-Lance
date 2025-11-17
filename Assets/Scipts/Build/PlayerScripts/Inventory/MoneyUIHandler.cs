using UnityEngine;
using TMPro;

public class MoneyUIHandler : MonoBehaviour{
    public TextMeshProUGUI moneyText;
    public PlayerInventoryHandler playerInventory;

    private void OnEnable(){
        if (playerInventory != null)
            playerInventory.OnMoneyChanged += UpdateMoneyText;
    }

    private void OnDisable(){
        if (playerInventory != null)
            playerInventory.OnMoneyChanged -= UpdateMoneyText;
    }

    private void Start(){
        playerInventory = PlayerControlerHandler.Instance.playerInventory;
        UpdateMoneyText(playerInventory.Money);
    }

    private void UpdateMoneyText(int currentMoney){
        moneyText.text = $"Money: {currentMoney}";
    }
}
