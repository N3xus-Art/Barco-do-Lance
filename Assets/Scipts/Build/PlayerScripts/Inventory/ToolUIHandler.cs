using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

// O nome do seu arquivo é ToolUIHandler.cs, mas a classe é EquippedToolUIHandler <-- meu mano copilot julgou o nome da classe
public class EquippedToolUIHandler : MonoBehaviour
{
    public PlayerInventoryHandler playerInventoryHandler;
    public TextMeshProUGUI ToolText;
    public Image image;
    public Image ScrollUtil;

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
            playerInventoryHandler.OnInventoryChanged -= UpdateToolText;
            playerInventoryHandler.OnEquippedToolChanged -= UpdateToolText;
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

        var currentTool = tool ?? playerInventoryHandler?.EquippedTool;
            Debug.Log("Primeiro check ui");
        if (currentTool != null)
        {
            image.sprite = currentTool.Data.icon;
            Debug.Log("Antes do If");

            // Verifica se a ferramenta equipada é uma instância de Sambura.
            if (currentTool is SamburaInstanceHandler sambura)
            {
                // Se for uma Sambura, exibe a informação de capacidade.
                ToolText.text = $"{sambura.Data.toolName} ({sambura.occupiedSpace}/{sambura.maxCapacity})";
                // A barra de preenchimento (ScrollUtil) reflete o espaço ocupado.
                ScrollUtil.fillAmount = (float)sambura.occupiedSpace / sambura.maxCapacity;
            }
            else
            {
                // Se for qualquer outra ferramenta, exibe a durabilidade normalmente.
                ToolText.text = $"{currentTool.Data.toolName} ({currentTool.CurrentDurability}/{currentTool.MaxDurability})";
                ScrollUtil.fillAmount = (float)currentTool.CurrentDurability / currentTool.MaxDurability;
            Debug.Log("n sambura");
            }
        }
        else
        {
            ToolText.text = "Ferramenta atual: Nenhuma";
            image.sprite = null; // Opcional: Limpar o ícone se não houver ferramenta
            ScrollUtil.fillAmount = 0;
            Debug.Log("Else toolui");
        }
    }

    private void UpdateToolText(ToolInstanceHandler tool) => UpdateToolUI(tool);
    private void UpdateToolText() => UpdateToolUI(null);
}