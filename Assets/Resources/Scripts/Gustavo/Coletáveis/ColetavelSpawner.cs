using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ColetavelSpawner : MonoBehaviour
{
    public List<GameObject> collectiblesPF;

    private BoxCollider2D spawnArea;

    void Awake()
    {
        spawnArea = GetComponent<BoxCollider2D>();
        spawnArea.isTrigger = true;

        SpawnCollectble();
    }

    private void SpawnCollectble()
    {
        // 1. Filtra os coletáveis que ainda não foram coletados.
        List<GameObject> availableCollectibles = new List<GameObject>();
        foreach (var prefab in collectiblesPF)
        {
            Collectible collectible = prefab.GetComponent<Collectible>();
            if (collectible != null)
            {
                if (PlayerPrefs.GetInt(collectible.collectibleID) != 1)
                {
                    availableCollectibles.Add(prefab);
                }
            }
        }

        // 2. Se houver coletáveis disponíveis, sorteia um e instancia.
        if (availableCollectibles.Count > 0)
        {
            int indexSorteado = Random.Range(0, availableCollectibles.Count);
            GameObject prefabParaSpawnar = availableCollectibles[indexSorteado];

            // 3. Calcula uma posição 2D aleatória.
            Vector2 posicaoAleatoria = GetRandomPosition();

            // Instancia o coletável na posição sorteada.
            Instantiate(prefabParaSpawnar, posicaoAleatoria, Quaternion.identity);

            Debug.Log("Spawnou o coletável: " + prefabParaSpawnar.GetComponent<Collectible>().collectibleID);
        }
        else
        {
            Debug.Log("Nenhum coletável novo para spawnar nesta área. Todos já foram coletados!");
        }
    }

    private Vector2 GetRandomPosition()
    {

        Bounds bounds = spawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(x, y);
    }
}