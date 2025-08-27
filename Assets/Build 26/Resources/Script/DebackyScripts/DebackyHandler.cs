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
    [SerializeField] private ScriptableObjectMissions[] currentMissions = new ScriptableObjectMissions[] { };
        #endregion
    //Methods
    #region
    private void OnTriggerEnter2D(Collider2D collision){
        inTrigger = true;
        keyGO.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision){
        inTrigger = false;
        keyGO.SetActive(false);
    }
    private void Update(){
        if (BuildInputHandler.isInteracting && inTrigger){
            debackyScreen.SetActive(true);
        }
        animalImage.sprite = currentMissions[currentID].AnimalSprite;
        missionTMP.SetText($"Recompensa: { currentMissions[currentID].missionReward}");
        moneyTMP.SetText($"R${money}");
    }
    #endregion
}
