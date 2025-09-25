using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IslandHandler : MonoBehaviour{
    //Variables
    #region
    [Header("----Variaveis de Trigger----")]
    [SerializeField] private bool ison;
    [Header("----Variaveis de Canva----")]
    [SerializeField] private GameObject shopGO;
    [SerializeField] private GameObject normalScreen;
    [SerializeField] private GameObject confirmScreen;
    [SerializeField] private GameObject poorScreen;
    [SerializeField] private Button closeButton;
    [Header("----Variaveis de Items----")]
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private int money;
    [SerializeField] private int firstItemPrice;
    [SerializeField] private int secondItemPrice;
    [SerializeField] private int thirdItemPrice;
    [SerializeField] private string firstItemName;
    [SerializeField] private string secondItemName;
    [SerializeField] private string thirdItemName;
    [SerializeField] private Button firstButton;
    [SerializeField] private Button secoundButton;
    [SerializeField] private Button thirdButton;
    #endregion
    //Methods
    #region
    //Trigger Methods
    void OnTriggerEnter2D(Collider2D collider2D){
            ison = true;
        }

        void OnTriggerExit2D(Collider2D collider2D){
            ison = false;
        }

    //Button Methods
        public void OnCloseClick(){
            ison = false;
            shopGO.SetActive(false);
            Time.timeScale = 1;
        }

        //Confirms
            public void ConfirmFirst() {
                if (money < firstItemPrice){
                    poorScreen.SetActive(true);
                    return;
                }
                money -= firstItemPrice;
                confirmScreen.SetActive(false);
            }
            public void ConfirmSecound() {
                if (money < secondItemPrice)
                {
                    poorScreen.SetActive(true);
                    return;
                }
                money -= secondItemPrice;
                confirmScreen.SetActive(false);
            }
            public void ConfirmThird() {
                if (money < thirdItemPrice)
                {
                    poorScreen.SetActive(true);
                    return;
                }
                money -= thirdItemPrice;
                confirmScreen.SetActive(false);
            }


        public void Cancel() { 
            confirmScreen.SetActive(false);
        }

        public void Continue(){
            confirmScreen.SetActive(false);
            poorScreen.SetActive(false);
        }

        public void Buy() { 
            if(confirmScreen.activeSelf == true) { 
                confirmScreen.SetActive(true);
            }

        }

    //Unity Methods
        private void Start(){
            Button closeTab = closeButton.GetComponent<Button>();
            Button firstTab = firstButton.GetComponent<Button>();
            Button secoundTab = secoundButton.GetComponent<Button>();
            Button thirdTab = thirdButton.GetComponent<Button>();
            closeTab.onClick.AddListener(OnCloseClick);
            firstTab.onClick.AddListener(ConfirmFirst);
            secoundTab.onClick.AddListener(ConfirmSecound);
            thirdTab.onClick.AddListener(ConfirmThird);
    }
        private void Update(){
            if (BuildInputHandler.isInteracting && ison){
                Time.timeScale = 0;
                shopGO.SetActive(true);
                normalScreen.SetActive(true);
            }
            moneyText.SetText("Dinheiro: " + money);
            if (ison == false){
                shopGO.SetActive(false);
                Time.timeScale = 1;
            }
        }
    #endregion
}
