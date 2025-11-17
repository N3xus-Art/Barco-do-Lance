using UnityEngine;
using TMPro;

public class MoneyUIHandler : MonoBehaviour{
    public TextMeshProUGUI moneyText;

    private void OnEnable(){
        if (PlayerControlerHandler.Instance.playerInventory != null)
            PlayerControlerHandler.Instance.playerInventory.OnMoneyChanged += UpdateMoneyText;
    }

    private void OnDisable(){
        if (PlayerControlerHandler.Instance.playerInventory != null)
            PlayerControlerHandler.Instance.playerInventory.OnMoneyChanged -= UpdateMoneyText;
    }

    private void Start(){
        UpdateMoneyText(PlayerControlerHandler.Instance.playerInventory.Money);
    }

    private void UpdateMoneyText(int currentMoney){
        moneyText.text = $"Money: {currentMoney}";
    }
}
