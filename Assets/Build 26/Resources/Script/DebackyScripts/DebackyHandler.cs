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
    [Header("----Variaveis de Confirmação----")]
    [SerializeField] private bool onMission = false;
    [SerializeField] private bool outMission = false;
    [SerializeField] private GameObject confirmButtonGO;
    [SerializeField] private Button confirmButton;
    [SerializeField] private TMP_Text confirmTMP;
    [SerializeField] private Color acceptColor;
    [SerializeField] private Color endColor;
    [SerializeField] private GameObject CurrentMissionGO;
    [SerializeField] private int MissionID;


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

        //Import data to the canva
        animalImage.sprite = avaliableMissions[currentID].animalSprite;
        animalDescription.SetText($"{avaliableMissions[currentID].animalDescription}");
        missionDescription.SetText($"{avaliableMissions[currentID].missionDescription}");
        missionTMP.SetText($"Recompensa: {avaliableMissions[currentID].missionReward}");

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
            onMission = true;


            confirmButtonGO.GetComponent<Image>().color = endColor; // mudar isso dps, pra adicionar o botão vermelho
            confirmTMP.SetText("Cancelar");
        }
    }

    public void EndMissions() {
        if (outMission){
            money += CurrentMissionGO.GetComponent<CurrentMission>().missionReward;
            Destroy(CurrentMissionGO);

            SortMission(nextMissions,2);

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

    private void SortMission(List<ScriptableObjectMissions> MissionList, int MissionLevel = 0)
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

            avaliableMissions[currentID] = CapableMissions[Rand];

        }
        else {


            var Rand = Random.Range(0, MissionList.Count);

            avaliableMissions[currentID] = MissionList[Rand];


        }

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

        //Inicialization data to the canva

        animalImage.sprite = avaliableMissions[currentID].animalSprite;
        animalDescription.SetText($"{avaliableMissions[currentID].animalDescription}");
        missionDescription.SetText($"{avaliableMissions[currentID].missionDescription}");
        missionTMP.SetText($"Recompensa: {avaliableMissions[currentID].missionReward}");


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

        if (!outMission && onMission && CurrentMissionGO != null && CurrentMissionGO.GetComponent<CurrentMission>().End){ 
            outMission = true;
            confirmTMP.SetText("Terminar Missão");
            confirmButtonGO.GetComponent<Image>().color = acceptColor;
        }
        moneyTMP.SetText($"R${money}");
    }
    #endregion
}