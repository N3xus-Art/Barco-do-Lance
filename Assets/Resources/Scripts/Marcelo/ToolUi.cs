using UnityEngine;
using TMPro;
using System;

public class EquippedToolUI : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TextMeshProUGUI ToolText;

    private void OnEnable()
    {
        if (playerInventory != null)
        {
            playerInventory.OnInventoryChanged += UpdateToolText;
            playerInventory.OnEquippedToolChanged += UpdateToolText;
        }

            
    }

    private void OnDisable()
    {
        if (playerInventory != null)
        {
            playerInventory.OnInventoryChanged += UpdateToolText;
            playerInventory.OnEquippedToolChanged += UpdateToolText;
        }
    }

    private void Start()
    {
        UpdateToolText();
    }


    private void UpdateToolText(ToolInstance tool)
    {
        if (playerInventory.EquippedTool != null)
        {
            ToolText.text = $"Ferramenta atual: {tool.Data.toolName} ({tool.CurrentDurability}/{tool.MaxDurability})";
        }
        else
        {
            ToolText.text = "Ferramenta atual: Nenhuma";
        }
    }
    private void UpdateToolText()
    {
        if (playerInventory.EquippedTool != null)
        {
            ToolText.text = $"Ferramenta atual: {playerInventory.EquippedTool.Data.toolName} ({playerInventory.EquippedTool.CurrentDurability}/{playerInventory.EquippedTool.MaxDurability})";
        }
        else
        {
            ToolText.text = "Ferramenta atual: Nenhuma";
        }
}
}
