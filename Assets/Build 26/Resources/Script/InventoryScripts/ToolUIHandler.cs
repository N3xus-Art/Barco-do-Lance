using UnityEngine;
using TMPro;
using System;

public class EquippedToolUIHandler : MonoBehaviour
{
    public PlayerInventoryHandler playerInventoryHandler;
    public TextMeshProUGUI ToolText;

    private void OnEnable()
    {
        if (playerInventoryHandler != null)
        {
            playerInventoryHandler.OnInventoryChanged += UpdateToolText;
            playerInventoryHandler.OnEquippedToolChanged += UpdateToolText;
        }


    }

    private void OnDisable()
    {
        if (playerInventoryHandler != null)
        {
            playerInventoryHandler.OnInventoryChanged += UpdateToolText;
            playerInventoryHandler.OnEquippedToolChanged += UpdateToolText;
        }
    }

    private void Start()
    {
        UpdateToolText();
    }


    private void UpdateToolText(ToolInstanceHandler tool)
    {
        if (playerInventoryHandler.EquippedTool != null)
        {
            ToolText.text = $"Ferramenta atual: {tool.DataHandler.toolName} ({tool.CurrentDurability}/{tool.MaxDurability})";
        }
        else
        {
            ToolText.text = "Ferramenta atual: Nenhuma";
        }
    }
    private void UpdateToolText()
    {
        if (playerInventoryHandler.EquippedTool != null)
        {
            ToolText.text = $"Ferramenta atual: {playerInventoryHandler.EquippedTool.DataHandler.toolName} ({playerInventoryHandler.EquippedTool.CurrentDurability}/{playerInventoryHandler.EquippedTool.MaxDurability})";
        }
        else
        {
            ToolText.text = "Ferramenta atual: Nenhuma";
        }
    }
}
