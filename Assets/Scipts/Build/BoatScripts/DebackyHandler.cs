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
    [SerializeField] private CurrentMission currentMission;
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

        // muda o valor atual da tab
        currentID = TabIndex;
        
        // checa se tem missão ativa e se ta na tab dela
        if (currentID == MissionTabID && onMission){

            // ativa o botão de missão 
            confirmButtonGO.SetActive(true);
        }
        // se nn mas ta com missão
        else if (onMission){

            // desativa o botão de missão
            confirmButtonGO.SetActive(false);
        }

        // checa se ta na primeira tab, se sim desativa o botão de voltar
        if (currentID == 0) { PreviousButton.gameObject.SetActive(false); }else{ PreviousButton.gameObject.SetActive(true); }
        
        // checa se ta na primeira tab, se sim desativa o botão de avançar
        if (currentID == avaliableMissions.Length - 1){ NextButton.gameObject.SetActive(false); }else{ NextButton.gameObject.SetActive(true); }

        ImportDataToCanva();
    }

    private void TabChange(int i){
        
        var j = currentID + i;

        if ((j != -1) && (j != avaliableMissions.Length)){

            // muda o valor atual da tab
            currentID = j;
            
            // checa se tem missão ativa e se ta na tab dela
            if (currentID == MissionTabID && onMission)
            {

                // ativa o botão de missão 
                confirmButtonGO.SetActive(true);
            }
            // se nn mas ta com missão
            else if (onMission)
            {

                // desativa o botão de missão
                confirmButtonGO.SetActive(false);
            }

            // checa se ta na primeira tab, se sim desativa o botão de voltar
            if (currentID == 0) { PreviousButton.gameObject.SetActive(false); } else { PreviousButton.gameObject.SetActive(true); }

            // checa se ta na primeira tab, se sim desativa o botão de avançar
            if (currentID == avaliableMissions.Length - 1) { NextButton.gameObject.SetActive(false); } else { NextButton.gameObject.SetActive(true); }

            ImportDataToCanva();
        }
    }

    #endregion
    #region Missions Methods
    public void AcceptMissions(){
        if (!onMission){

            // criando o componente de missão e atribuindo os ids
            currentMission = playerCH.AddComponent<CurrentMission>();
            playerCH.CurrentMission = currentMission;
            
            // colocando as variáveis da missão nela
            currentMission.missionReward = avaliableMissions[currentID].missionReward;
            currentMission.RescueableAnimals = avaliableMissions[currentID].rescueableAnimals;
            currentMission.OperableAnimals = avaliableMissions[currentID].operableAnimals;
            currentMission.Level = avaliableMissions[currentID].Level;
            currentMission.MissionSea = avaliableMissions[currentID].MissionSea;

            // atribuindo a tab na qual pegou a missão
            MissionTabID = currentID;
            currentMission.TabID = currentID;
            
            onMission = true;

            //Criar os animais do oceano atual
            if (currentMission.MissionSea == GameManagerHandler.currentSea){
                GameManagerHandler.SpawnAnimalsInCurrentSea(playerCH.CurrentMission);
                Debug.Log("teste");
            }

            //Definir o que acontece quando não está no oceano atual
            confirmButton.interactable = false;
            confirmTMP.SetText("Em Missão");
        }
    }

    public void EndMissions(){
        if (outMission){
            
            //recebendo o valor da missão
            var money = playerCH.CurrentMission.GetComponent<CurrentMission>().missionReward;
            playerInventory.Receive(money);

            //destruindo o componente da missão
            Destroy(playerCH.CurrentMission);

            //sorteando uma nova missão
            if (currentID == 0){ 
                SortMission(nextMissions, currentID, 1);
            }
            else if (currentID == 1){ 
                SortMission(nextMissions, currentID, 2); 
            
            }else{ SortMission(nextMissions, currentID, 0); 
            }

            GameManagerHandler.Instance.CurrentAvaliableMissions[currentID] = avaliableMissions[currentID];

            // importando os dados da missão nova e reconfigurando o botão de missão
            ImportDataToCanva();
            confirmTMP.SetText("Aceitar");
            outMission = false;
            onMission = false;
        }
    }

    public void CancelMission(){
        if (onMission){

            // Recebendo o valor atual da missão
            var money = playerCH.CurrentMission.CurrentReward;

            //destruindo o componente da missão
            playerInventory.Receive(money);

            Destroy(playerCH.CurrentMission);
            onMission = false;
        }
    }

    private void SortMission(List<ScriptableObjectMissions> MissionList, int ID, int MissionLevel = 0){

        // lista de missões que pode pegar nas circustâncias dadas
        var CapableMissions = new List<ScriptableObjectMissions>();

        // checa quais das missões que ele tem disponível para pegar batem com as circustâncias
        foreach (var Mission in MissionList){

            // Checa as circustânias dada
            if (((Mission.Level == MissionLevel && Mission != avaliableMissions[ID]) || (MissionLevel == 0)) && !avaliableMissions.Contains(Mission)){
                
                // adiciona a missão a lista se ela bate com as circustâncias dadas
                CapableMissions.Add(Mission);
            }

        }

        // Randomiza
        var Rand = Random.Range(0, CapableMissions.Count);
        
        // Adiciona a Missão nova na tab
        avaliableMissions[ID] = CapableMissions[Rand];

        // Se a missão for de nivel 3, retira ela da lista de missões que ele pode pegar
        if (avaliableMissions[ID].Level == 3) MissionList.Remove(avaliableMissions[ID]);
    }

    private void ImportDataToCanva(){
        
        // Importa data pro canva
        missionDescription.SetText($"{avaliableMissions[currentID].missionDescription}");
        missionTMP.SetText($"Recompensa:    R${avaliableMissions[currentID].missionReward}");
    }
    #endregion
    #region Unity Methods

    private void Start(){

        //Pegando as Instâncias
        playerCH = PlayerControlerHandler.Instance.GetComponent<PlayerControlerHandler>();
        playerInventory = PlayerControlerHandler.Instance.GetComponent<PlayerInventoryHandler>();
        
        // Adicionando as funções pros botões 
        firstButton.onClick.AddListener(() => { OnTabClick(0); });
        secondButton.onClick.AddListener(() => { OnTabClick(1); });
        centralButton.onClick.AddListener(() => { OnTabClick(2); });
        fourthButton.onClick.AddListener(() => { OnTabClick(3); });
        lastButton.onClick.AddListener(() => { OnTabClick(4); });
        PreviousButton.onClick.AddListener(() => { TabChange(-1); });
        NextButton.onClick.AddListener(() => { TabChange(1); });

        confirmButton.onClick.AddListener(AcceptMissions);
        confirmButton.onClick.AddListener(EndMissions);


        // checa se ele já sorteou as missões (trocar nextmissions por uma lista de missões do game manager) 
        if (GameManagerHandler.Instance.CurrentAvaliableMissions[0] == null) {

            // se ele nn tiver sorteado as missões, sorteia 
            SortMission(nextMissions, 0, 1);
            SortMission(nextMissions, 1, 2);
            SortMission(nextMissions, 2, 0);
            SortMission(nextMissions, 3, 0);
            SortMission(nextMissions, 4, 0);


            // armazena as missões numa lista (depois passar essa lista para o GameManager)
            for(var i = 0; i < avaliableMissions.Length; i++)
            {
                GameManagerHandler.Instance.CurrentAvaliableMissions[i] = avaliableMissions[i];
            }

        }
        else {

            // se já tiver sorteado, importa as missões

            for (var i = 0; i < avaliableMissions.Length; i++)
            {
                avaliableMissions[i] = GameManagerHandler.Instance.CurrentAvaliableMissions[i];

            }

            if (playerCH.CurrentMission != null)
            {
                // seta a missão atual no site
                onMission = true;
                MissionTabID = playerCH.CurrentMission.TabID;

                if (MissionTabID == currentID)
                {
                    // reconfigura o botão missão
                    confirmButton.interactable = false;
                    confirmTMP.SetText("Em Missão");
                }
                else {

                    // reconfigura o botão missão
                    confirmButtonGO.SetActive(false);
                    confirmButton.interactable = false;
                    confirmTMP.SetText("Em Missão");

                }
            }
        }
             
        ImportDataToCanva();
    }

    private void Update(){
        
        // Abre a Tela do site
        if (BuildInputHandler.isInteracting && inTrigger){
            debackyScreen.SetActive(true);
        }else if (!BuildInputHandler.isInteracting && !inTrigger){
            debackyScreen.SetActive(false);
        }

        // checa se a missão acabou
        if (!outMission && onMission && playerCH.CurrentMission != null && playerCH.CurrentMission.End) {
            
            // reconfigura o botão missão 
            outMission = true;
            confirmTMP.SetText("Terminar Missão");
            confirmButton.interactable = true;
        }

    
    }
    #endregion
}