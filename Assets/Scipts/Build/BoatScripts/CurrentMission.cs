using UnityEngine;

public class CurrentMission : MonoBehaviour{

    public int missionReward;
    public int RescueableAnimals;
    public int OperableAnimals;
    public bool onMission;
    public bool End = false;
    public int CurrentReward;
    public int Level;
    public int MissionSea;
    public int TabID;    

    void Update(){
        if (OperableAnimals == 0 && RescueableAnimals == 0 && End == false) { 
            End = true; 
        }
    }
}
