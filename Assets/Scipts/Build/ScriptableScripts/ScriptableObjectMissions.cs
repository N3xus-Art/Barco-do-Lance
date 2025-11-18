using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Mission_", menuName = "Mission/New Missions")]
public class ScriptableObjectMissions : ScriptableObject
{
    public string missionName;
    [Range(1, 3)] public int Level = 1;
    [TextArea] public string missionDescription;
    public int missionID;
    public int missionReward;
    public int rescueableAnimals;
    public int operableAnimals;
    [Range(0, 2)] public int MissionSea;
    public int rescueableSea = -1;
    public Sprite OperableImage;
    public Sprite RescuableImage;
}
