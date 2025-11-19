using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _toolText;
    [SerializeField] private Image _image;
    [SerializeField] private Image _scrollUtil;

    private PlayerInventoryHandler _playerInventoryHandler;
    private ToolInstanceHandler _subscribedTool; // Rastreia a ferramenta que estamos "ouvindo"

    public PlayerInventoryHandler InventoryHandler
    {
        get => _playerInventoryHandler;
        set
        {
            // Se já existia um inventário, removemos os eventos dele
            if (_playerInventoryHandler != null)
            {
                _playerInventoryHandler.OnInventoryChanged -= OnInventoryChangedGlobal;
                _playerInventoryHandler.OnEquippedToolChanged -= OnEquippedToolChanged;
            }

            _playerInventoryHandler = value;

            // Se o novo inventário for válido, assinamos os eventos
            if (_playerInventoryHandler != null)
            {
                _playerInventoryHandler.OnInventoryChanged += OnInventoryChangedGlobal;
                _playerInventoryHandler.OnEquippedToolChanged += OnEquippedToolChanged;

                // Força atualização inicial
                SubscribeToTool(_playerInventoryHandler.EquippedTool);
            }
        }
    }

    private void Awake()
    {
        if (PlayerInventoryHandler.Instance != null)
        {
            InventoryHandler = PlayerInventoryHandler.Instance;
        }
    }

    private void OnDisable()
    {
        // Limpa eventos do Inventário
        if (_playerInventoryHandler != null)
        {
            _playerInventoryHandler.OnInventoryChanged -= OnInventoryChangedGlobal;
            _playerInventoryHandler.OnEquippedToolChanged -= OnEquippedToolChanged;
        }

        // Limpa eventos da Ferramenta específica
        UnsubscribeFromTool();
    }

    // Chamado quando o inventário geral muda (adiciona/remove item)
    private void OnInventoryChangedGlobal()
    {
        // Revalida a ferramenta equipada, pois ela pode ter sido removida ou trocada
        if (_playerInventoryHandler != null)
        {
            SubscribeToTool(_playerInventoryHandler.EquippedTool);
        }
    }

    // Chamado quando o jogador troca de ferramenta explicitamente
    private void OnEquippedToolChanged(ToolInstanceHandler newTool)
    {
        SubscribeToTool(newTool);
    }

    // Lógica para gerenciar a assinatura de eventos da ferramenta individual
    private void SubscribeToTool(ToolInstanceHandler tool)
    {
        // Se já estamos ouvindo essa ferramenta, apenas atualiza a UI e retorna
        if (_subscribedTool == tool)
        {
            UpdateToolUI(_subscribedTool);
            return;
        }

        // Para de ouvir a ferramenta antiga
        UnsubscribeFromTool();

        // Começa a ouvir a nova ferramenta
        _subscribedTool = tool;
        if (_subscribedTool != null)
        {
            _subscribedTool.OnDurabilityChanged += OnToolDurabilityChanged;
        }

        UpdateToolUI(_subscribedTool);
    }

    private void UnsubscribeFromTool()
    {
        if (_subscribedTool != null)
        {
            _subscribedTool.OnDurabilityChanged -= OnToolDurabilityChanged;
            _subscribedTool = null;
        }
    }

    // Este evento vem diretamente do ToolInstanceHandler quando a durabilidade cai
    private void OnToolDurabilityChanged(ToolInstanceHandler tool)
    {
        UpdateToolUI(tool);
    }

    private void UpdateToolUI(ToolInstanceHandler tool)
    {
        if (_toolText == null || _image == null || _scrollUtil == null) return;

        if (tool != null)
        {
            _image.sprite = tool.Data?.icon;

            // Lógica para Sambura (Capacidade) vs Ferramenta Normal (Durabilidade)
            if (tool is SamburaInstanceHandler sambura)
            {
                _toolText.text = $"{sambura.Data?.toolName} ({sambura.occupiedSpace}/{sambura.maxCapacity})";
                _scrollUtil.fillAmount = sambura.maxCapacity > 0 ? (float)sambura.occupiedSpace / sambura.maxCapacity : 0f;
            }
            else
            {
                _toolText.text = $"{tool.Data?.toolName} ({tool.CurrentDurability}/{tool.MaxDurability})";
                _scrollUtil.fillAmount = tool.MaxDurability > 0 ? (float)tool.CurrentDurability / tool.MaxDurability : 0f;
            }
        }
        else
        {
            _toolText.text = "Mão Vazia"; // Ou deixe vazio ""
            _image.sprite = null; // Ou um sprite padrão transparente
            _scrollUtil.fillAmount = 0;
        }
    }
}