using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventoryHandler : MonoBehaviour
{
    public static PlayerInventoryHandler Instance { get; private set; }

    [Header("Dinheiro")]
    [SerializeField] public int Money = 100;

    [Header("Capacidade")]
    [SerializeField] private int maxSlots = 20;

    [Header("Itens do Jogador")]
    [SerializeField] private List<ToolInstanceHandler> ownedTools = new List<ToolInstanceHandler>();

    [SerializeField] private string equippedInstanceId;

    // --- Eventos para UI/Audio/Gameplay ---
    public event Action OnInventoryChanged;
    public event Action<int> OnMoneyChanged;
    public event Action<ToolInstanceHandler> OnEquippedToolChanged;

    // --- Propriedades Públicas ---
    public IReadOnlyList<ToolInstanceHandler> OwnedTools => ownedTools;
    public int MaxSlots => maxSlots;
    public int UsedSlots => ownedTools.Count;
    public bool HasFreeSlot => UsedSlots < MaxSlots;
    public ToolInstanceHandler EquippedTool => ownedTools.FirstOrDefault(t => t.InstanceId == equippedInstanceId);

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

    #region Gerenciamento de Dinheiro
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
    #endregion

    #region Gerenciamento de Inventário (Adicionar, Remover)

    public bool TryAddTool(ToolDataHandler data, out ToolInstanceHandler instance)
    {
        instance = null;
        if (data == null) return false;
        if (!HasFreeSlot) return false;
        if (ownedTools.Any(t => t.Data.toolName == data.toolName))
        {
            Debug.Log("Item duplicado: " + data.toolName);
            return false;
        }

        if (data.type == ToolType.Sambura)
        {
            instance = new SamburaInstanceHandler(data);
        }
        else
        {
            instance = new ToolInstanceHandler(data);
        }

        ownedTools.Add(instance);

        if (EquippedTool == null)
        {
            Equip(instance.InstanceId);
        }

        RaiseInventoryChanged();
        return true;
    }

    public bool RemoveToolById(string instanceId)
    {
        int idx = ownedTools.FindIndex(t => t.InstanceId == instanceId);
        if (idx < 0) return false;

        bool wasEquipped = ownedTools[idx].InstanceId == equippedInstanceId;
        ownedTools.RemoveAt(idx);

        if (wasEquipped)
        {
            equippedInstanceId = ownedTools.Count > 0 ? ownedTools[0].InstanceId : null;
            OnEquippedToolChanged?.Invoke(EquippedTool);
        }

        RaiseInventoryChanged();
        return true;
    }

    public ToolInstanceHandler GetToolById(string instanceId) =>
        ownedTools.FirstOrDefault(t => t.InstanceId == instanceId);

    #endregion

    #region Equipar Ferramentas
    public bool Equip(string instanceId)
    {
        var tool = GetToolById(instanceId);
        if (tool == null) return false;

        equippedInstanceId = tool.InstanceId;
        OnEquippedToolChanged?.Invoke(tool);
        Debug.Log("Equip do PlayerInv");
        return true;
    }

    public void EquipNext()
    {
        if (ownedTools.Count == 0) return;
        int idx = Mathf.Max(0, ownedTools.FindIndex(t => t.InstanceId == equippedInstanceId));
        idx = (idx + 1) % ownedTools.Count;
        equippedInstanceId = ownedTools[idx].InstanceId;
        OnEquippedToolChanged?.Invoke(ownedTools[idx]);
        Debug.Log("EquipNext do PlayerInv " + ownedTools[idx]);
    }

    public void EquipToolByType(ToolType type)
    {
        var toolToEquip = ownedTools.FirstOrDefault(t => t.Data.type == type && !t.IsBroken);
        if (toolToEquip != null)
        {
            Equip(toolToEquip.InstanceId);
            Debug.Log("EquipToolByType do PlayerInv");
        }
        else
        {
            Debug.Log($"Nenhuma ferramenta do type {type} encontrada no inventário.");
        }
    }

    // --- Wrappers para botões da Unity ---
    public void EquipFaca() => EquipToolByType(ToolType.Knife);
    public void EquipAlicate() => EquipToolByType(ToolType.Pliers);
    public void EquipTesoura() => EquipToolByType(ToolType.Scissors);
    #endregion

    #region Ações com Ferramentas (Usar, Comprar, Melhorar, Descartar)
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

        RaiseInventoryChanged();
        Debug.Log("TryUseEquipped do PlayerInv");
        return true;
    }

    public void UpgradeEquippedTool()
    {
        var equippedTool = EquippedTool;
        if (equippedTool == null)
        {
            Debug.Log("Nenhuma ferramenta equipada para dar upgrade.");
            return;
        }

        ToolDataHandler currentToolData = equippedTool.Data;
        int nextLevel = currentToolData.itemLevel + 1;
        string resourcePath = $"ScriptableObjects/Tools/{currentToolData.toolName}{nextLevel}";

        ToolDataHandler nextToolData = Resources.Load<ToolDataHandler>(resourcePath);

        if (nextToolData == null)
        {
            Debug.LogError($"Não foi possível encontrar o ToolData para o próximo nível em: {resourcePath}");
            return;
        }

        int oldToolIndex = ownedTools.FindIndex(t => t.InstanceId == equippedInstanceId);
        if (oldToolIndex != -1)
        {
            ownedTools.RemoveAt(oldToolIndex);
        }

        var newToolInstance = new ToolInstanceHandler(nextToolData);
        ownedTools.Insert(oldToolIndex != -1 ? oldToolIndex : 0, newToolInstance);

        Equip(newToolInstance.InstanceId);

        Debug.Log($"Ferramenta {currentToolData.toolName} atualizada para o nível {nextLevel}!");
        RaiseInventoryChanged();
    }

    public bool TryBuyTool(ToolDataHandler data, out ToolInstanceHandler instance)
    {
        instance = null;
        if (data == null || !HasFreeSlot || !CanAfford(data.shopPrice))
        {
            Debug.LogWarning("Falha na compra: pré-requisitos não atendidos.");
            return false;
        }

        if (!Spend(data.shopPrice)) return false;

        if (!TryAddTool(data, out instance))
        {
            Debug.LogWarning($"Falha ao adicionar a ferramenta '{data.toolName}'. Revertendo a compra.");
            Receive(data.shopPrice); // Devolve o dinheiro
            instance = null;
            return false;
        }

        Debug.Log($"Ferramenta '{data.toolName}' comprada com sucesso.");
        return true;
    }

    public bool TryScrapBroken(string instanceId)
    {
        var tool = GetToolById(instanceId);
        if (tool == null || !tool.IsBroken) return false;

        // Opcional: Adicionar dinheiro pela sucata
        // Receive(tool.Data.scrapValue);

        RemoveToolById(instanceId);
        return true;
    }
    #endregion

    #region Lógica de Captura e Armazenamento
    public bool TryCaptureAnimal(ICapturable animal)
    {
        ToolInstanceHandler rede = ownedTools.FirstOrDefault(t => t.Data.type == ToolType.Net && !t.IsBroken);
        bool sucessoNaCaptura;

        if (rede != null)
        {
            Debug.Log("Usando a Rede para capturar. Sucesso garantido!");
            sucessoNaCaptura = true;
            rede.TryUse(1);
        }
        else
        {
            Debug.Log("Nenhuma Rede encontrada. Tentando capturar na mão...");
            if (UnityEngine.Random.value <= 0.1f)
            {
                Debug.Log("Sorte! Capturado na mão!");
                sucessoNaCaptura = true;
            }
            else
            {
                Debug.Log("Falhou em capturar na mão. O animal vai escapar.");
                sucessoNaCaptura = false;
            }
        }

        if (sucessoNaCaptura)
        {
            bool armazenado = TryStoreFishInSambura(animal);
            if (!armazenado)
            {
                Debug.LogWarning("Captura bem-sucedida, mas a Sambura está cheia! O animal escapou.");
                return false;
            }
            return true;
        }

        return false;
    }

    public bool TryStoreFishInSambura(ICapturable animal)
    {
        SamburaInstanceHandler sambura = ownedTools.OfType<SamburaInstanceHandler>().FirstOrDefault(s => !s.isFull);
        if (sambura == null)
        {
            Debug.LogWarning("Nenhuma Sambura com espaço disponível encontrada no inventário!");
            return false;
        }
        return sambura.TryAddFish(animal);
    }
    #endregion

    #region Utilitários
    private void RaiseInventoryChanged() => OnInventoryChanged?.Invoke();
    private void RaiseMoneyChanged() => OnMoneyChanged?.Invoke(Money);
    #endregion
}