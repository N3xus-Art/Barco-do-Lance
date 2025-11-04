using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;

public class DebackyHandler : MonoBehaviour{
    #region Variables
    [Header("----Variaveis de Trigger----")]
    [SerializeField] private bool inTrigger;
    [SerializeField] private GameObject keyGO;
    [SerializeField] private GameObject debackyScreen;
    [SerializeField] private PlayerInventoryHandler playerInventory;
    [SerializeField] public PlayerControlerHandler playerCH;
    [Header("----Variaveis do Canvas----")]
    [SerializeField] private Image animalImage;
    [SerializeField] private TMP_Text animalDescription;
    [SerializeField] private TMP_Text missionTMP;
    [SerializeField] private TMP_Text missionDescription;
    [SerializeField] private int missionValue;
    [SerializeField] private int currentID;
    [SerializeField] private List<ScriptableObjectMissions> nextMissions = new List<ScriptableObjectMissions>();
    [SerializeField] private ScriptableObjectMissions[] avaliableMissions = new ScriptableObjectMissions[] { };
    [Header("----Variaveis das Abas----")]
    [SerializeField] private Button firstButton;
    [SerializeField] private Button secondButton;
    [SerializeField] private Button centralButton;
    [SerializeField] private Button fourthButton;
    [SerializeField] private Button lastButton;
    [SerializeField] private int MissionTabID = -1;
    [SerializeField] private Button NextButton;
    [SerializeField] private Button PreviousButton;
    [Header("----Variaveis de Confirmação----")]
    [SerializeField] private bool onMission = false;
    [SerializeField] private bool outMission = false;
    [SerializeField] private GameObject confirmButtonGO;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TMP_Text confirmTMP;
    #endregion 
    //Methods

    #region Trigger Methods
    private void OnTriggerEnter2D(Collider2D collision){
        inTrigger = true;
        keyGO.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision){
        inTrigger = false;
        keyGO.SetActive(false);
    }
    #endregion
    #region Tabs Methods
    private void OnTabClick(int TabIndex){
        currentID = TabIndex;

        if (currentID == MissionTabID && onMission){
            confirmButtonGO.SetActive(true);
        }
        else if (onMission){
            confirmButtonGO.SetActive(false);
        }

        if (currentID == 0) { 
            PreviousButton.gameObject.SetActive(false); 
        }else{ 
            PreviousButton.gameObject.SetActive(true); 
        }

        if (currentID == avaliableMissions.Length - 1){ 
            NextButton.gameObject.SetActive(false);
        }else{ 
            NextButton.gameObject.SetActive(true); 
        }
        ImportDataToCanva();
    }

    private void TabChange(int i){
        var j = currentID + i;
        if ((j != -1) && (j != avaliableMissions.Length)){
            currentID = j;
            ImportDataToCanva();
            if (currentID == 0){ 
                PreviousButton.gameObject.SetActive(false); 
            }else{ 
                PreviousButton.gameObject.SetActive(true);
            }

            if (currentID == avaliableMissions.Length - 1){ 
                NextButton.gameObject.SetActive(false); 
            }else{ 
                NextButton.gameObject.SetActive(true);
            }

            if (currentID == MissionTabID && onMission){
                confirmButtonGO.SetActive(true);
            }else if (onMission){
                confirmButtonGO.SetActive(false);
            }
        }
    }

    #endregion
    #region Missions Methods
    public void AcceptMissions(){
        if (!onMission){
            playerCH.CurrentMission.missionReward = avaliableMissions[currentID].missionReward;
            playerCH.CurrentMission.RescueableAnimals = avaliableMissions[currentID].rescueableAnimals;
            playerCH.CurrentMission.OperableAnimals = avaliableMissions[currentID].operableAnimals;
            playerCH.CurrentMission.Level = avaliableMissions[currentID].Level;
            playerCH.CurrentMission.MissionSea = avaliableMissions[currentID].MissionSea;
            MissionTabID = currentID;
            onMission = true;

            if (playerCH.CurrentMission.MissionSea == GameManagerHandler.currentSea){
                Debug.Log("teste");
            }
            confirmButton.interactable = false;
            confirmTMP.SetText("Em Missão");
        }
    }

    public void EndMissions(){
        if (outMission){
            var money = playerCH.CurrentMission.GetComponent<CurrentMission>().missionReward;
            playerInventory.Receive(money);
            playerCH.CurrentMission.missionReward = 0;
            playerCH.CurrentMission.RescueableAnimals = 0;
            playerCH.CurrentMission.OperableAnimals = 0;
            playerCH.CurrentMission.Level = 0;
            playerCH.CurrentMission.MissionSea = 0;
            if (currentID == 0){ 
                SortMission(nextMissions, currentID, 1); 
            }else if (currentID == 1){ 
                SortMission(nextMissions, currentID, 2); 
            }else{ SortMission(nextMissions, currentID, 0); 
            }
            ImportDataToCanva();
            confirmTMP.SetText("Aceitar");
            outMission = false;
            onMission = false;
        }
    }

    public void CancelMission(){
        if (onMission){
            var money = playerCH.CurrentMission.GetComponent<CurrentMission>().CurrentReward;
            playerInventory.Receive(money);
            Destroy(playerCH.CurrentMission);
            onMission = false;
        }
    }

    private void SortMission(List<ScriptableObjectMissions> MissionList, int ID, int MissionLevel = 0){
        var CapableMissions = new List<ScriptableObjectMissions>();
        foreach (var Mission in MissionList){
            if (((Mission.Level == MissionLevel && Mission != avaliableMissions[ID]) || (MissionLevel == 0)) && !avaliableMissions.Contains(Mission)){
                CapableMissions.Add(Mission);
            }
        }
        var Rand = Random.Range(0, CapableMissions.Count);
        avaliableMissions[ID] = CapableMissions[Rand];
        if (avaliableMissions[ID].Level == 3) MissionList.Remove(avaliableMissions[ID]);
    }

    private void ImportDataToCanva(){
        //Import data to the canva
        missionDescription.SetText($"{avaliableMissions[currentID].missionDescription}");
        missionTMP.SetText($"Recompensa:    R${avaliableMissions[currentID].missionReward}");
    }
    #endregion
    #region Unity Methods

    private void Start(){
        //Tabs Handler
        playerCH = PlayerControlerHandler.Instance.GetComponent<PlayerControlerHandler>();
        playerInventory = PlayerControlerHandler.Instance.GetComponent<PlayerInventoryHandler>();
        firstButton.onClick.AddListener(() => { OnTabClick(0); });
        secondButton.onClick.AddListener(() => { OnTabClick(1); });
        centralButton.onClick.AddListener(() => { OnTabClick(2); });
        fourthButton.onClick.AddListener(() => { OnTabClick(3); });
        lastButton.onClick.AddListener(() => { OnTabClick(4); });
        PreviousButton.onClick.AddListener(() => { TabChange(-1); });
        NextButton.onClick.AddListener(() => { TabChange(1); });

        //Confirm Button
        confirmButton.onClick.AddListener(AcceptMissions);
        confirmButton.onClick.AddListener(EndMissions);
        SortMission(nextMissions, 0, 1);
        SortMission(nextMissions, 1, 2);
        SortMission(nextMissions, 2, 0);
        SortMission(nextMissions, 3, 0);
        SortMission(nextMissions, 4, 0);
        ImportDataToCanva();
    }

    private void Update(){
        //Open DebackyScreen
        if (BuildInputHandler.isInteracting && inTrigger){
            debackyScreen.SetActive(true);
        }else if (!BuildInputHandler.isInteracting && !inTrigger){
            debackyScreen.SetActive(false);
        }

        if (!outMission && onMission && playerCH.CurrentMission != null && playerCH.CurrentMission.End) {
            outMission = true;
            confirmTMP.SetText("Terminar Missão");
            confirmButton.interactable = true;
        }
    }
    #endregion
}