using UnityEngine;

[CreateAssetMenu(fileName = "ToolDataHandler", menuName = "Scriptable Objects/ToolDataHandler")]
public class ToolDataHandler : ScriptableObject
{
    public string toolName;
    public Sprite icon;
    public int maxDurability;
    public int shopPrice;
    public int scrapValue;
    public int itemLevel;
}
