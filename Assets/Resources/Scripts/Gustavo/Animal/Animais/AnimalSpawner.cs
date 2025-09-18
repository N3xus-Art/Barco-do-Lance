using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public Transform AreaSpawn;
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4f;
    public float maxY = 4f;

    void Start()
    {
        // Exemplo: spawna 5 animais ao iniciar
        for (int i = 0; i < 5; i++)
        {
            SpawnAnimal();
        }
    }

    void SpawnAnimal()
    {
        // Escolhe um prefab aleatoriamente do array
        GameObject animalToSpawn = animalPrefabs[Random.Range(0, animalPrefabs.Length)];
        RectTransform area = AreaSpawn.GetComponent<RectTransform>();

        // Gera uma posição aleatória dentro da área definida
        Vector2 spawnPosition = new Vector2(
                Random.Range(-area.rect.width / 2, area.rect.width / 2),
                Random.Range(-area.rect.height / 2, area.rect.height / 2)
            );

        // Instancia o animal na posição aleatória
        Instantiate(animalToSpawn, spawnPosition, Quaternion.identity);
    }
}