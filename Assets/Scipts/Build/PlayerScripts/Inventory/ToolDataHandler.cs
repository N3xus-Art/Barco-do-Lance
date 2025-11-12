using UnityEngine;
public enum ToolType { Knife, Pliers, Scissors, Sambura, Net, Racao}

[CreateAssetMenu(fileName = "ToolDataHandler", menuName = "Scriptable Objects/ToolDataHandler")]
public class ToolDataHandler : ScriptableObject
{
    public ToolType type;
    public string toolName;
    public Sprite icon;
    public int maxDurability;
    public int shopPrice;
    public int scrapValue;
    public int itemLevel;
}
