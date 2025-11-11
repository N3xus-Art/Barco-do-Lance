using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SamburaInstanceHandler : ToolInstanceHandler
{
    [SerializeField]
    private List<GameObject> peixesArmazenados = new List<GameObject>();

    public IReadOnlyList<GameObject> PeixesArmazenados => peixesArmazenados;
    public int maxCapacity { get; private set; }
    public int occupiedSpace => peixesArmazenados.Count;
    public bool EstaCheia => occupiedSpace >= maxCapacity;

    public SamburaInstanceHandler(ToolDataHandler toolData) : base(toolData)
    {
        maxCapacity = toolData.maxDurability;
    }

    public bool TentarAdicionarPeixe(GameObject peixeObjeto)
    {
        if (EstaCheia)
        {
            Debug.Log("A Sambura está cheia!");
            return false;
        }

        peixeObjeto.SetActive(false);

        if (PlayerInventoryHandler.Instance != null)
        {
            peixeObjeto.transform.SetParent(PlayerInventoryHandler.Instance.transform);
        }

        peixesArmazenados.Add(peixeObjeto);
        Debug.Log($"Peixe '{peixeObjeto.name}' adicionado à Sambura. Espaço: {occupiedSpace}/{maxCapacity}");
        RaiseDurabilityChanged();
        return true;
    }

    public void LimparSambura()
    {
        peixesArmazenados.Clear();
        Debug.Log("Sambura esvaziada.");
        RaiseDurabilityChanged();
    }

    public new int CurrentDurability => maxCapacity - occupiedSpace;
}