using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DebackyHandler : MonoBehaviour {
    //Variables
    #region
    [Header("----Variaveis de Trigger----")]
    [SerializeField] private bool inTrigger;
    [SerializeField] private GameObject keyGO;
    [SerializeField] private GameObject debackyScreen;
    [Header("----Variaveis do Canvas----")]
    [SerializeField] private float money;
    [SerializeField] private TMP_Text moneyTMP;
    [SerializeField] private Image animalImage;
    [SerializeField] private TMP_Text animalDescription;
    [SerializeField] private TMP_Text missionTMP;
    [SerializeField] private TMP_Text missionDescription;
    [SerializeField] private int missionValue;
    [SerializeField] private int currentID;
    [SerializeField] private List<ScriptableObjectMissions> nextMissions = new List<ScriptableObjectMissions>();
    [SerializeField] private ScriptableObjectMissions[] avaliableMissions = new ScriptableObjectMissions[]{};
    [Header("----Variaveis das Abas----")]
    [SerializeField] private Button firstButton;
    [SerializeField] private Button centralButton;
    [SerializeField] private Button lastButton;
    [Header("----Variaveis de Confirmação----")]
    [SerializeField] private bool onMission = false;
    [SerializeField] private bool outMission = false;
    [SerializeField] private GameObject confirmButtonGO;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TMP_Text confirmTMP;
    [SerializeField] private Color acceptColor;
    [SerializeField] private Color endColor;
    #endregion
    //Methods
    #region
    //Trigger Methods
    private void OnTriggerEnter2D(Collider2D collision){
        inTrigger = true;
        keyGO.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision){
        inTrigger = false;
        keyGO.SetActive(false);
    }
    //Tabs Methods
    private void OnFirstTabClick(){
        currentID = 0;
    }
    private void OnCentralTabClick(){
        currentID = 1;
    }
    private void OnLastTabClick(){
        currentID = 2;
    }
    //Missions Methods
    public void AcceptMissions(){
        if (!onMission) { 
        GameObject currentMission = new GameObject();
        currentMission.name = "CurrentMission";
        currentMission.AddComponent<CurrentMission>();
        currentMission.GetComponent<CurrentMission>().missionReward = avaliableMissions[currentID].missionReward;
        currentMission.GetComponent<CurrentMission>().RescueableAnimals = avaliableMissions[currentID].rescueableAnimals;
        currentMission.GetComponent<CurrentMission>().OperableAnimals = avaliableMissions[currentID].operableAnimals;
        onMission = true;
        }
    }

    public void EndMissions() {
        if (outMission){
            money += GameObject.Find("CurrentMission").GetComponent<CurrentMission>().missionReward;
            Destroy(GameObject.Find("CurrentMission"));
            nextMissions.Add(avaliableMissions[currentID]);
            avaliableMissions[currentID] = nextMissions[0];
            nextMissions.RemoveAt(0);
            onMission = false;
            outMission = false;
        }
    }

    //Unity Methods
    private void Start(){
        //Tabs Handler
        Button firstTab = firstButton.GetComponent<Button>();
        Button centralTab = centralButton.GetComponent<Button>();
        Button lastTab = lastButton.GetComponent<Button>();
        firstTab.onClick.AddListener(OnFirstTabClick);
        centralTab.onClick.AddListener(OnCentralTabClick);
        lastTab.onClick.AddListener(OnLastTabClick);
        //Confirm Button
        Button confirmButton = confirmButtonGO.GetComponent<Button>();
        confirmButton.onClick.AddListener(AcceptMissions);
        confirmButton.onClick.AddListener(EndMissions);
    }
    private void Update(){
        //Open DebackyScreen
        if (BuildInputHandler.isInteracting && inTrigger){
            debackyScreen.SetActive(true);
        }else if(!BuildInputHandler.isInteracting && inTrigger){
            debackyScreen.SetActive(false);
        }
        //Import data to the canva
        animalImage.sprite = avaliableMissions[currentID].animalSprite;
        animalDescription.SetText($"{avaliableMissions[currentID].animalDescription}");
        missionDescription.SetText($"{avaliableMissions[currentID].missionDescription}");
        missionTMP.SetText($"Recompensa: {avaliableMissions[currentID].missionReward}");
        moneyTMP.SetText($"R${money}");
        //Updates the confirm button
        if (onMission){
            confirmButtonGO.GetComponent<Image>().color = endColor;
        }else {
            confirmButtonGO.GetComponent<Image>().color = acceptColor;
            confirmTMP.SetText("Aceitar");
        }
        if (!outMission && onMission && GameObject.Find("CurrentMission") != null && GameObject.Find("CurrentMission").GetComponent<CurrentMission>().End){ 
            outMission = true;
        }
        if (outMission){
            confirmTMP.SetText("Terminar Missão");
            confirmButtonGO.GetComponent<Image>().color = acceptColor;
        }
    }
    #endregion
}
