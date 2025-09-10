using System;
using UnityEngine;

[Serializable]
public class ToolInstanceHandler
{
    
    // Unique per-owned-item so you can track/equip/scrap specific copies
    [SerializeField] private string instanceId;

    [SerializeField] private ToolDataHandler dataHandler;
    [SerializeField] private int currentDurability;

    // ---- Events (for UI or sound hooks) ----
    public event Action<ToolInstanceHandler> OnDurabilityChanged;
    public event Action<ToolInstanceHandler> OnBroken;

    // ---- Public API ----
    public string InstanceId => instanceId;
    public ToolDataHandler DataHandler => dataHandler;
    public int CurrentDurability => currentDurability;
    public int MaxDurability => dataHandler != null ? dataHandler.maxDurability : 0;
    public bool IsBroken => currentDurability <= 0;
    public float Durability01 => MaxDurability > 0 ? (float)currentDurability / MaxDurability : 0f;

    public ToolInstanceHandler(ToolDataHandler toolData)
    {
        if (toolData == null) throw new ArgumentNullException(nameof(toolData));
        dataHandler = toolData;
        instanceId = Guid.NewGuid().ToString("N");
        currentDurability = dataHandler.maxDurability;
    }

    /// <summary>
    /// Tries to use the tool and apply wear (default 1). Returns false if broken (no wear applied).
    /// </summary>
    public bool TryUse(int wearAmount = 1)
    {
        if (wearAmount <= 0) wearAmount = 1;
        if (IsBroken) return false;

        int prev = currentDurability;
        currentDurability = Mathf.Clamp(currentDurability - wearAmount, 0, MaxDurability);
        if (currentDurability != prev)
        {
            OnDurabilityChanged?.Invoke(this);
            if (IsBroken)
                OnBroken?.Invoke(this);
        }
        return true;
    }

    /// <summary>
    /// Repairs by an amount. Clamps to MaxDurability.
    /// </summary>
    public void Repair(int amount)
    {
        if (amount <= 0 || IsBroken) return; // design choice: broken tools can’t be repaired; scrap instead
        int prev = currentDurability;
        currentDurability = Mathf.Clamp(currentDurability + amount, 0, MaxDurability);
        if (currentDurability != prev)
            OnDurabilityChanged?.Invoke(this);
    }

    /// <summary>
    /// Restores to full durability (if not broken). Handy for cheats/debug.
    /// </summary>
    public void RestoreFull()
    {
        if (IsBroken) return;
        if (currentDurability != MaxDurability)
        {
            currentDurability = MaxDurability;
            OnDurabilityChanged?.Invoke(this);
        }
    }
}
