using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using System.Reflection;

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
    [SerializeField] private Button secondButton;
    [SerializeField] private Button centralButton;
    [SerializeField] private Button fourthButton;
    [SerializeField] private Button lastButton;
    [SerializeField] private int MissionTabID = -1;
    [Header("----Variaveis de Confirmação----")]
    [SerializeField] private bool onMission = false;
    [SerializeField] private bool outMission = false;
    [SerializeField] private GameObject confirmButtonGO;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TMP_Text confirmTMP;
    [SerializeField] private Color acceptColor;
    [SerializeField] private Color endColor;
    [SerializeField] private GameObject CurrentMissionGO;


    #endregion
    //Methods

    //Trigger Methods
    #region
    private void OnTriggerEnter2D(Collider2D collision){
        inTrigger = true;
        keyGO.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision){
        inTrigger = false;
        keyGO.SetActive(false);
    }
    #endregion
    //Tabs Methods
    #region
    private void OnTabClick(int TabIndex){

        currentID = TabIndex;

        if (currentID == MissionTabID && onMission) {

            confirmButtonGO.SetActive(true);

        }
        else if(onMission)
        {

            confirmButtonGO.SetActive(false);

        }

            ImportDataToCanva();

    }
    #endregion
    //Missions Methods
    #region
    public void AcceptMissions(){
        if (!onMission) { 

            GameObject currentMission = new GameObject();
            currentMission.name = "CurrentMission";
            currentMission.AddComponent<CurrentMission>();
            currentMission.GetComponent<CurrentMission>().missionReward = avaliableMissions[currentID].missionReward;
            currentMission.GetComponent<CurrentMission>().RescueableAnimals = avaliableMissions[currentID].rescueableAnimals;
            currentMission.GetComponent<CurrentMission>().OperableAnimals = avaliableMissions[currentID].operableAnimals;
            currentMission.GetComponent<CurrentMission>().Level = avaliableMissions[currentID].Level;
            CurrentMissionGO = currentMission;
            MissionTabID = currentID;
            onMission = true;


            confirmButtonGO.GetComponent<Image>().color = endColor; // mudar isso dps, pra adicionar o botão vermelho
            confirmTMP.SetText("Cancelar");
        }
    }

    public void EndMissions() {
        if (outMission){
            money += CurrentMissionGO.GetComponent<CurrentMission>().missionReward;
            moneyTMP.SetText($"R${money}");
            Destroy(CurrentMissionGO);
            if (currentID == 0) { SortMission(nextMissions,currentID, 1); } else if (currentID == 1) { SortMission(nextMissions,currentID, 2); }else {  SortMission(nextMissions,currentID, 0);}
            
            ImportDataToCanva();

            confirmButtonGO.GetComponent<Image>().color = acceptColor; // mudar isso dps, pra adicionar o botão verde normal
            confirmTMP.SetText("Aceitar");
            outMission = false;
            onMission = false;

        }
    }

    public void CancelMission(){

        if (onMission)
        {

            money += CurrentMissionGO.GetComponent<CurrentMission>().CurrentReward;
            Destroy(CurrentMissionGO);
            onMission = false;

        }

    }

    private void SortMission(List<ScriptableObjectMissions> MissionList,int ID, int MissionLevel = 0)
    {
        
        if (MissionLevel != 0)
        {
            
            var CapableMissions = new List<ScriptableObjectMissions>();

            foreach (var Mission in MissionList)
            {

                if (Mission.Level == MissionLevel)
                {

                    CapableMissions.Add(Mission);

                }

            }

            var Rand = Random.Range(0,CapableMissions.Count);

            avaliableMissions[ID] = CapableMissions[Rand];
            

        }
        else {

            var Rand = Random.Range(0, MissionList.Count);

            avaliableMissions[ID] = MissionList[Rand];

        }
         if (avaliableMissions[ID].Level != 1) MissionList.Remove(avaliableMissions[ID]); 
    }

    private void ImportDataToCanva(){

        //Import data to the canva
        animalImage.sprite = avaliableMissions[currentID].animalSprite;
        animalDescription.SetText($"{avaliableMissions[currentID].animalDescription}");
        missionDescription.SetText($"{avaliableMissions[currentID].missionDescription}");
        missionTMP.SetText($"Recompensa: {avaliableMissions[currentID].missionReward}");

    }

    #endregion
    //Unity Methods
    #region
    private void Start(){
        //Tabs Handler

        firstButton.onClick.AddListener(()=> { OnTabClick(0); });
        secondButton.onClick.AddListener(() => { OnTabClick(1); });
        centralButton.onClick.AddListener(() => { OnTabClick(2); });
        fourthButton.onClick.AddListener(() => { OnTabClick(3); });
        lastButton.onClick.AddListener(() => { OnTabClick(4); });

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
        }else if(!BuildInputHandler.isInteracting && inTrigger){
            debackyScreen.SetActive(false);
        }

        if (!outMission && onMission && CurrentMissionGO != null && CurrentMissionGO.GetComponent<CurrentMission>().End){ 
            outMission = true;
            confirmTMP.SetText("Terminar Missão");
            confirmButtonGO.GetComponent<Image>().color = acceptColor;
        }
        
    }
    #endregion
}