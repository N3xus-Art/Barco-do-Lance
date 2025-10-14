using UnityEngine;
public enum TipoFerramenta { Faca, Alicate, Tesoura, }

[CreateAssetMenu(fileName = "ToolDataHandler", menuName = "Scriptable Objects/ToolDataHandler")]
public class ToolDataHandler : ScriptableObject
{
    public TipoFerramenta tipo;
    public string toolName;
    public Sprite icon;
    public int maxDurability;
    public int shopPrice;
    public int scrapValue;
    public int itemLevel;
}
