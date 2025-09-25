using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class EquippedToolUIHandler : MonoBehaviour
{
    public PlayerInventoryHandler playerInventory;
    public TextMeshProUGUI ToolText;
    public Image image;
    public Image ScrollUtil;

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
            playerInventory.OnInventoryChanged -= UpdateToolText;
            playerInventory.OnEquippedToolChanged -= UpdateToolText;
        }
    }

    private void Start()
    {
        UpdateToolText();
    }


    // Atualiza a UI da ferramenta equipada
    private void UpdateToolUI(ToolInstanceHandler tool)
    {
        if (ToolText == null || image == null || ScrollUtil == null) return;

        var currentTool = tool ?? playerInventory?.EquippedTool;
        if (currentTool != null)
        {
            ToolText.text = $"Ferramenta atual: {currentTool.Data.toolName} ({currentTool.CurrentDurability}/{currentTool.MaxDurability})";
            image.sprite = currentTool.Data.icon;
            ScrollUtil.fillAmount = (float)currentTool.CurrentDurability / currentTool.MaxDurability;
        }
        else
        {
            ToolText.text = "Ferramenta atual: Nenhuma";
        }
    }

    private void UpdateToolText(ToolInstanceHandler tool) => UpdateToolUI(tool);
    private void UpdateToolText() => UpdateToolUI(null);
}
