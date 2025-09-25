using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IslandHandler : MonoBehaviour{
    [SerializeField] private string Index;
    [SerializeField] private bool ison;
    [SerializeField] private GameObject moneyGO;
    [SerializeField] private TMP_Text moneytext;
    SceneController sceneController;
    void OnTriggerEnter2D(Collider2D other){
        Index = this.gameObject.name;
        Debug.Log(Index);
        Debug.Log(BuildInputHandler.isInteracting);
        if (BuildInputHandler.isInteracting){
            Time.timeScale = 0;
            sceneController = new SceneController();
            sceneController.LoadScene(Index, LoadSceneMode.Additive);
            ison = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        Index = "";
        ison = false;

    }
    private void Start(){
            moneyGO = GameObject.FindGameObjectWithTag("ShopMoneyText");
            moneytext = moneyGO.GetComponent<TMP_Text>();    
    }
    private void Update(){
        Debug.Log(moneyGO);
        Debug.Log(moneytext);
        if (ison){
            moneytext.SetText("Dinheiro: " + BuildInputHandler.playerMoney);
        }
    }

}
