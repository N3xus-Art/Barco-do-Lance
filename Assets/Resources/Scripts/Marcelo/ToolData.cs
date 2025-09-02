using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "Scriptable Objects/ToolData")]
public class ToolData : ScriptableObject
{
    public string toolName;
    public Sprite icon;
    public int maxDurability;
    public int shopPrice;
    public int scrapValue;
    public int itemLevel;
}
