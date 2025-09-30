using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission_", menuName = "Mission/New Missions")]
public class ScriptableObjectMissions : ScriptableObject{
    public string missionName;
    public Sprite animalSprite;
    [Range(1,3)]public int Level = 1;
    [TextArea] public string missionDescription;
    [TextArea] public string animalDescription;
    public int missionID;
    public float missionReward;
    public int rescueableAnimals;
    public int operableAnimals;
}
