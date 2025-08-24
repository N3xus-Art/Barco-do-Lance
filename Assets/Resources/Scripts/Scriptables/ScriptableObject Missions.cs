using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Missão", menuName = "Missão/Nova Missão")]
public class ScriptableObjectMissions : ScriptableObject
{
    public string MissionName;
    public Sprite AnimalSprite;
    public string MissionDescription;
    public string AnimalDescription;

    public int missionID;
    public float missionReward;
    public int RescueableAnimals;
    public int OperableAnimals;

}
