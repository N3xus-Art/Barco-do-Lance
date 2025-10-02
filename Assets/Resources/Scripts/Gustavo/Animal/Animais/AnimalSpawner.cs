using Unity.VisualScripting;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public Transform AreaSpawn;
    
    /*
    void Start()
    {
        SpawnAnimal(10); // Exemplo: spawna 10 animais
    }
    */

    public void SpawnAnimal(int maxAnimals)
    {
        for (int i = 0; i < maxAnimals; i++)
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
            GameObject spawnedAnimal = Instantiate(animalToSpawn, spawnPosition, Quaternion.identity, AreaSpawn);
            Vector3 parentScale = AreaSpawn.lossyScale;
            if (spawnedAnimal.name.EndsWith("(Clone)"))
            {
                spawnedAnimal.name = spawnedAnimal.name.Replace("(Clone)", "");
            }
            //Descarta o ajuste manual do tamanho e ajusta a escala considerando a escala do pai
            if (spawnedAnimal.name == "Shark_PF") // Ajusta a escala do tubarão para 2.5 metros, considerando a escala do pai
            {
                spawnedAnimal.transform.localScale = new Vector3(
                    2.5f / parentScale.x,
                    2.5f / parentScale.y,
                    2.5f / parentScale.z
                );
            }
            else // Ajusta a escala de animais no geral para 1.5 metros, considerando a escala do pai
                spawnedAnimal.transform.localScale = new Vector3(
                1.5f / parentScale.x,
                1.5f / parentScale.y,
                1.5f / parentScale.z
            );
        }
    }
}
