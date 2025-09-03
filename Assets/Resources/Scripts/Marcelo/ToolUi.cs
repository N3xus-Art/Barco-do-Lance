using UnityEngine;
using TMPro; 

public class EquippedToolUI : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TextMeshProUGUI ToolText;

    private void OnEnable()
    {
        if (playerInventory != null)
            playerInventory.OnEquippedToolChanged += UpdateToolText;
    }

    private void OnDisable()
    {
        if (playerInventory != null)
            playerInventory.OnEquippedToolChanged -= UpdateToolText;
    }

    private void Start()
    {
        UpdateToolText(playerInventory.EquippedTool);
    }

    private void UpdateToolText(ToolInstance tool)
    {
        if (tool != null)
        {
            ToolText.text = $"Ferramenta atual: {tool.Data.toolName} ({tool.CurrentDurability}/{tool.MaxDurability})";
        }
        else
        {
            ToolText.text = "Ferramenta atual: Nenhuma";
        }
    }
}
