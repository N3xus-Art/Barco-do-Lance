using System;
using UnityEngine;

public enum ToolStatee
{
    Normal,
    Broken
}

[Serializable]
public class ToolInstance
{
    // Unique per-owned-item so you can track/equip/scrap specific copies
    [SerializeField] private string instanceId;

    [SerializeField] private ToolData data;
    [SerializeField] private int currentDurability;
    [SerializeField] private ToolState state;

    // ---- Events (for UI or sound hooks) ----
    public event Action<ToolInstance> OnDurabilityChanged;
    public event Action<ToolInstance> OnBroken;

    // ---- Public API ----
    public string InstanceId => instanceId;
    public ToolData Data => data;
    public ToolState State => state;
    public int CurrentDurability => currentDurability;
    public int MaxDurability => data != null ? data.maxDurability : 0;
    public bool IsBroken => state == ToolState.Broken;
    public float Durability01 => MaxDurability > 0 ? (float)currentDurability / MaxDurability : 0f;

    public ToolInstance(ToolData toolData)
    {
        if (toolData == null) throw new ArgumentNullException(nameof(toolData));
        data = toolData;
        instanceId = Guid.NewGuid().ToString("N");
        currentDurability = data.maxDurability;
        state = ToolState.Normal;
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
            if (currentDurability <= 0)
            {
                state = ToolState.Broken;
                OnBroken?.Invoke(this);
            }
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

    public override string ToString()
    {
        return data != null
            ? $"{data.toolName} [{currentDurability}/{MaxDurability}]"
            : $"<Null ToolData> [{currentDurability}/{MaxDurability}]";
    }
}
