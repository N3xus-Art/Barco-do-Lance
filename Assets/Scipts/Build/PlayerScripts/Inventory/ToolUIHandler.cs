using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToolUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _toolText;
    [SerializeField] private Image _image;
    [SerializeField] private Image _scrollUtil;

    private PlayerInventoryHandler _playerInventoryHandler;

    public PlayerInventoryHandler PlayerInventoryHandler
    {
        get => _playerInventoryHandler;
        set
        {
            if (_playerInventoryHandler != null)
            {
                _playerInventoryHandler.OnInventoryChanged -= UpdateToolText;
                _playerInventoryHandler.OnEquippedToolChanged -= UpdateToolText;
            }
            _playerInventoryHandler = value;
            if (_playerInventoryHandler != null)
            {
                _playerInventoryHandler.OnInventoryChanged += UpdateToolText;
                _playerInventoryHandler.OnEquippedToolChanged += UpdateToolText;
                UpdateToolText();
            }
        }
    }

    private void Awake()
    {
        if (PlayerInventoryHandler.Instance != null)
        {
            PlayerInventoryHandler = PlayerInventoryHandler.Instance;
        }
    }

    private void OnEnable()
    {
        if (PlayerInventoryHandler.Instance != null)
        {
            PlayerInventoryHandler = PlayerInventoryHandler.Instance;
        }
    }

    private void OnDisable()
    {
        if (_playerInventoryHandler != null)
        {
            _playerInventoryHandler.OnInventoryChanged -= UpdateToolText;
            _playerInventoryHandler.OnEquippedToolChanged -= UpdateToolText;
        }
    }

    private void Start()
    {
        UpdateToolText();
    }

    private void UpdateToolUI(ToolInstanceHandler tool)
    {
        if (_toolText == null || _image == null || _scrollUtil == null) return;

        var currentTool = tool ?? _playerInventoryHandler?.EquippedTool;
        if (currentTool != null)
        {
            _image.sprite = currentTool.Data?.icon;
            if (currentTool is SamburaInstanceHandler sambura)
            {
                _toolText.text = $"{sambura.Data?.toolName} ({sambura.occupiedSpace}/{sambura.maxCapacity})";
                _scrollUtil.fillAmount = sambura.maxCapacity > 0 ? (float)sambura.occupiedSpace / sambura.maxCapacity : 0f;
            }
            else
            {
                _toolText.text = $"{currentTool.Data?.toolName} ({currentTool.CurrentDurability}/{currentTool.MaxDurability})";
                _scrollUtil.fillAmount = currentTool.MaxDurability > 0 ? (float)currentTool.CurrentDurability / currentTool.MaxDurability : 0f;
            }
        }
        else
        {
            _toolText.text = "Ferramenta atual: Nenhuma";
            _image.sprite = null;
            _scrollUtil.fillAmount = 0;
        }
    }

    private void UpdateToolText(ToolInstanceHandler tool) => UpdateToolUI(tool);
    private void UpdateToolText() => UpdateToolUI(null);
}
