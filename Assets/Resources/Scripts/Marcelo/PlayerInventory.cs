using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    [Header("Dinheiro")]
    [SerializeField] public int Money = 100;

    [Header("Capacidade")]
    [SerializeField] private int maxSlots = 20;

    [Header("Itens do Jogador")]
    [SerializeField] private List<ToolInstance> ownedTools = new List<ToolInstance>();

    // Ferramenta equipada
    [SerializeField] private string equippedInstanceId;

    // Eventos para UI/Audio/Gameplay 
    public event Action OnInventoryChanged;
    public event Action<int> OnMoneyChanged;
    public event Action<ToolInstance> OnEquippedToolChanged;
    public IReadOnlyList<ToolInstance> OwnedTools => ownedTools;
    public int MaxSlots => maxSlots;
    public int UsedSlots => ownedTools.Count;
    public bool HasFreeSlot => UsedSlots < MaxSlots;

    public ToolInstance EquippedTool => ownedTools.FirstOrDefault(t => t.InstanceId == equippedInstanceId);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        RaiseMoneyChanged();
        RaiseInventoryChanged();
    }

    // ---------- Helpers de Dinheiro ----------
    public bool CanAfford(int price) => price <= Money;

    public bool Spend(int amount)
    {
        if (amount <= 0) return true;
        if (Money < amount) return false;
        Money -= amount;
        RaiseMoneyChanged();
        return true;
    }

    public void Receive(int amount)
    {
        if (amount <= 0) return;
        Money += amount;
        RaiseMoneyChanged();
    }

    // ---------- Core inventory ----------
    public bool TryAddTool(ToolData data, out ToolInstance instance)
    {
        instance = null;
        if (data == null) return false;
        if (!HasFreeSlot) return false;

        // Impede a adição de itens únicos duplicados
        if (ownedTools.Any(t => t.Data.toolName == data.toolName))
        {
            Debug.Log("Item duplicado: " + data.toolName);
            return false;
        }

        instance = new ToolInstance(data);
        ownedTools.Add(instance);

        // Auto-equipa a primeira ferramenta
        if (EquippedTool == null)
        {
            equippedInstanceId = instance.InstanceId;
            OnEquippedToolChanged?.Invoke(instance);
        }

        RaiseInventoryChanged();
        return true;
    }

    public bool RemoveToolById(string instanceId)
    {
        int idx = ownedTools.FindIndex(t => t.InstanceId == instanceId);
        if (idx < 0) return false;

        bool removingEquipped = ownedTools[idx].InstanceId == equippedInstanceId;
        ownedTools.RemoveAt(idx);

        if (removingEquipped)
        {
            equippedInstanceId = ownedTools.Count > 0 ? ownedTools[0].InstanceId : null;
            OnEquippedToolChanged?.Invoke(EquippedTool);
        }

        RaiseInventoryChanged();
        return true;
    }

    public ToolInstance GetToolById(string instanceId) =>
        ownedTools.FirstOrDefault(t => t.InstanceId == instanceId);

    // ---------- Equip / cycle ----------
    public bool Equip(string instanceId)
    {
        var tool = GetToolById(instanceId);
        if (tool == null) return false;

        equippedInstanceId = tool.InstanceId;
        OnEquippedToolChanged?.Invoke(tool);
        return true;
    }

    public void EquipNext()
    {
        if (ownedTools.Count == 0) return;
        int idx = Mathf.Max(0, ownedTools.FindIndex(t => t.InstanceId == equippedInstanceId));
        idx = (idx + 1) % ownedTools.Count;
        equippedInstanceId = ownedTools[idx].InstanceId;
        OnEquippedToolChanged?.Invoke(ownedTools[idx]);
    }

    public void EquipToolByType(TipoFerramenta tipo)
    {
        var toolToEquip = ownedTools.FirstOrDefault(t => t.Data.tipo == tipo && !t.IsBroken);
        if (toolToEquip != null)
        {
            Equip(toolToEquip.InstanceId);
        }
        else
        {
            Debug.Log($"Nenhuma ferramenta do tipo {tipo} encontrada no inventário.");
        }
    }

    // --- Wrappers para botões da Unity ---
    public void EquipFaca() => EquipToolByType(TipoFerramenta.Faca);
    public void EquipAlicate() => EquipToolByType(TipoFerramenta.Alicate);
    public void EquipTesoura() => EquipToolByType(TipoFerramenta.Tesoura);

    public void UpgradeEquippedTool()
    {
        var equippedTool = EquippedTool;
        if (equippedTool == null)
        {
            Debug.Log("Nenhuma ferramenta equipada para dar upgrade.");
            return;
        }

        ToolData currentToolData = equippedTool.Data;
        int nextLevel = currentToolData.itemLevel + 1;
        string nextToolName = currentToolData.toolName;

        // Assumindo que a convenção de nomeação para o ScriptableObject é ToolName+Level, por exemplo, Scissors2
        string resourcePath = $"Scripts/Marcelo/Tools/{nextToolName}{nextLevel}";

        ToolData nextToolData = Resources.Load<ToolData>(resourcePath);

        if (nextToolData == null)
        {
            Debug.LogError($"Não foi possível encontrar o ToolData para o próximo nível em: {resourcePath}");
            return;
        }

        // Remove a ferramenta antiga
        int oldToolIndex = ownedTools.FindIndex(t => t.InstanceId == equippedInstanceId);
        if (oldToolIndex != -1)
        {
            ownedTools.RemoveAt(oldToolIndex);
        }

        // Adiciona a nova ferramenta
        var newToolInstance = new ToolInstance(nextToolData);
        if (oldToolIndex != -1)
        {
            ownedTools.Insert(oldToolIndex, newToolInstance);
        }
        else
        {
            ownedTools.Add(newToolInstance);
        }

        // Equipa a nova ferramenta
        Equip(newToolInstance.InstanceId);

        Debug.Log($"Ferramenta {currentToolData.toolName} atualizada para o nível {nextLevel}!");
        RaiseInventoryChanged();
    }

    // ---------- Use (wear) ----------

    /// Usa a ferramenta equipada (wearAmount default 1). Retorna false se nenhuma ou quebrada.
    public bool TryUseEquipped(int wearAmount = 1)
    {
        var tool = EquippedTool;
        if (tool == null) return false;

        bool used = tool.TryUse(wearAmount);
        if (!used) return false;

        if (tool.IsBroken)
        {
            EquipNext();
        }

        // Notifica a UI que uma ferramenta dentro do inventário mudou de estado
        RaiseInventoryChanged();
        return true;
    }

    // ---------- Buy / Scrap ----------
    // Compra uma nova ferramenta usando seu preço de loja. Retorna a nova instância se bem-sucedido.
    public bool TryBuyTool(ToolData data, out ToolInstance instance)
    {
        instance = null;
        Debug.Log("Iniciando tentativa de compra de ferramenta...");

        if (data == null)
        {
            Debug.LogWarning("Falha na compra: ToolData é nulo.");
            return false;
        }

        if (!HasFreeSlot)
        {
            Debug.LogWarning("Falha na compra: Não há slots livres no inventário.");
            return false;
        }

        if (!CanAfford(data.shopPrice))
        {
            Debug.LogWarning($"Falha na compra: Dinheiro insuficiente. Preço: {data.shopPrice}, Dinheiro atual: {Money}");
            return false;
        }

        if (!Spend(data.shopPrice))
        {
            Debug.LogError("Falha na compra: Não foi possível gastar o dinheiro.");
            return false;
        }

        Debug.Log($"Tentando adicionar a ferramenta '{data.toolName}' ao inventário...");
        bool added = TryAddTool(data, out instance);
        if (!added)
        {
            Debug.LogWarning($"Falha ao adicionar a ferramenta '{data.toolName}'. Realizando rollback...");
            Receive(data.shopPrice);
            instance = null;
            return false;
        }

        Debug.Log($"Ferramenta '{data.toolName}' comprada com sucesso e adicionada ao inventário.");
        return true;
    }
    // Descarta uma ferramenta quebrada. Retorna false se a tool não estiver quebrada.
    public bool TryScrapBroken(string instanceId)
    {
        var tool = GetToolById(instanceId);
        if (tool == null) return false;
        if (!tool.IsBroken) return false;
        RemoveToolById(instanceId);
        return true;
    }

    // ---------- Utility ----------
    private void RaiseInventoryChanged() => OnInventoryChanged?.Invoke();
    private void RaiseMoneyChanged() => OnMoneyChanged?.Invoke(Money);
}
