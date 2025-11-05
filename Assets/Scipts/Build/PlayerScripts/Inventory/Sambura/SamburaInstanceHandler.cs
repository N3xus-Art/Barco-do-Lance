// SamburaInstanceHandler.cs

using System.Collections.Generic;
using UnityEngine;

// Esta classe herda tudo da ToolInstanceHandler e adiciona a funcionalidade de armazenamento
[System.Serializable]
public class SamburaInstanceHandler : ToolInstanceHandler
{
    // Lista para armazenar os peixes capturados
    [SerializeField]
    private List<ICapturable> storageFish = new List<ICapturable>();

    public IReadOnlyList<ICapturable> StorageFish => storageFish;
    public int maxCapacity { get; private set; } // A durabilidade será usada como capacidade
    public int occupiedSpace => storageFish.Count;
    public bool isFull => occupiedSpace >= maxCapacity;

    // Construtor que passa os dados da ferramenta para a classe base
    public SamburaInstanceHandler(ToolDataHandler toolData) : base(toolData)
    {
        // Usamos a durabilidade máxima do ScriptableObject como a capacidade da Sambura
        maxCapacity = toolData.maxDurability;
    }

    /// <summary>
    /// Tenta adicionar um peixe na Sambura. Retorna true se conseguir.
    /// </summary>
    public bool TryAddFish(ICapturable peixe)
    {
        if (isFull)
        {
            Debug.Log("A Sambura está cheia!");
            return false;
        }

        storageFish.Add(peixe);
        Debug.Log($"Peixe '{peixe.GetGameObject().name}' adicionado à Sambura. Espaço: {occupiedSpace}/{maxCapacity}");
        // Dispara um evento para a UI saber que a durabilidade (capacidade) mudou
        RaiseDurabilityChanged();
        return true;
    }

    /// <summary>
    /// Limpa a Sambura, por exemplo, ao descarregar os peixes no aquário.
    /// </summary>
    public void CleanSambura()
    {
        storageFish.Clear();
        Debug.Log("Sambura esvaziada.");
        RaiseDurabilityChanged();
    }

    // Sobrescrevemos a propriedade CurrentDurability para refletir o espaço ocupado
    public new int CurrentDurability => maxCapacity - occupiedSpace;
}