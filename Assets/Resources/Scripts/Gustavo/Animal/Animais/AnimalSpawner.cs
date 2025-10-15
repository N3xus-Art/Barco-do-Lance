using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public Transform AreaSpawn;

    void Start()
    {
        // Exemplo: spawna 5 animais ao iniciar
        for (int i = 0; i < 10; i++)
        {
            SpawnAnimal();
        }
    }

    void SpawnAnimal()
    {
        // Escolhe um prefab aleatoriamente do array
        GameObject animalToSpawn = animalPrefabs[Random.Range(0, animalPrefabs.Length)];

        // Obtém o tamanho da área de spawn
        Vector3 areaSize = AreaSpawn.GetComponent<Renderer>().bounds.size;

        // Gera uma posição aleatória dentro da área definida
        Vector3 spawnPosition = new Vector3(
            Random.Range(AreaSpawn.position.x - areaSize.x / 2, AreaSpawn.position.x + areaSize.x / 2),
            AreaSpawn.position.y,
            Random.Range(AreaSpawn.position.z - areaSize.z / 2, AreaSpawn.position.z + areaSize.z / 2)
        );

        // Instancia o animal na posição aleatória
        Instantiate(animalToSpawn, spawnPosition, Quaternion.identity, AreaSpawn);
    }
}
