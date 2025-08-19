using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Missão", menuName = "Missão/Nova Missão")]
public class ScritableObjectMissions : ScriptableObject
{
    public int missionID;
    public float missionReward;
    public int RescueableAnimals;
    public int OperableAnimals;

}
