using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
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

    // ---------- Use (wear) ----------

    /// Uses the currently equipped tool (wearAmount default 1). Returns false if none or broken.
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

        // Notify UI that a tool inside inventory changed state
        RaiseInventoryChanged();
        return true;
    }

    // ---------- Buy / Scrap ----------
    // Buys a brand-new tool using its shopPrice. Returns the new instance if successful.
    public bool TryBuyTool(ToolData data, out ToolInstance instance)
    {
        instance = null;
        if (data == null) return false;
        if (!HasFreeSlot) return false;
        if (!CanAfford(data.shopPrice)) return false;

        if (!Spend(data.shopPrice)) return false;

        bool added = TryAddTool(data, out instance);
        if (!added)
        {
            // roll back spend if something went wrong
            Receive(data.shopPrice);
            instance = null;
            return false;
        }
        return true;
    }

    // Scraps a BROKEN tool for cash. Returns false if the tool isn't broken.
    public bool TryScrapBroken(string instanceId, out int payout)
    {
        payout = 0;
        var tool = GetToolById(instanceId);
        if (tool == null) return false;
        if (!tool.IsBroken) return false;

        payout = tool.Data.scrapValue;
        Receive(payout);
        RemoveToolById(instanceId);
        return true;
    }

    // ---------- Utility ----------
    private void RaiseInventoryChanged() => OnInventoryChanged?.Invoke();
    private void RaiseMoneyChanged() => OnMoneyChanged?.Invoke(Money);
}
