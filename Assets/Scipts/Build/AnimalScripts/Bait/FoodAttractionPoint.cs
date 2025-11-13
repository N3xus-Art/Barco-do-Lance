using UnityEngine;
using System.Linq; // Pode ser necessário se você quiser ordenar

public class FoodAttractionPoint : MonoBehaviour
{
    [Header("Configuração")]
    [Tooltip("O raio de atração desta isca")]
    public float attractionRadius = 15f;

    [Tooltip("Por quanto tempo a isca fica ativa (em segundos)")]
    public float duration = 20f;

    void Start()
    {
        // 1. Destrói a si mesmo após a duração
        Destroy(gameObject, duration);

        // 2. Encontra todos os animais marinhos na cena
        MarineAnimalHandler[] allFish = FindObjectsByType<MarineAnimalHandler>(FindObjectsSortMode.None);

        Debug.Log($"Isca ativada. Procurando {allFish.Length} peixes...");

        // 3. Informa os peixes próximos sobre a comida
        foreach (MarineAnimalHandler fish in allFish)
        {
            // Verifica se o peixe está dentro do raio de atração
            if (Vector2.Distance(transform.position, fish.transform.position) <= attractionRadius)
            {
                fish.GoToFood(transform.position);
            }
        }
    }
}