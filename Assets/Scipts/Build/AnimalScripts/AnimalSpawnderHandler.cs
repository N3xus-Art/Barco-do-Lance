using UnityEngine;

public class AnimalSpawnerHandler : MonoBehaviour {
    public GameObject[] animalPrefabs;
    public Transform AreaSpawn;

    public GameObject SpecificAnimal;
    public int MissionLevel;
    public int SpecificAnimalQtt;


    // maxAnimals: número máximo de animais a serem spawnados por instancia
    // specificAnimal: animal específico a ser spawnado (pode ser null)
    // specificAnimalQtt: quantidade do animal específico a ser spawnado
    // missionLevel: nível da missão (1, 2 ou 3)
    public void SpawnAnimal(int maxAnimals, GameObject specificAnimal = null, int specificAnimalQtt = 0, int missionLevel = 1) {
        for (int i = 0; i < maxAnimals; i++) {
            // Usa o animal específico se fornecido, senão escolhe aleatoriamente
            GameObject animalToSpawn;

            if (missionLevel == 3 && AreaSpawn.name != "AreaSpawnHard")
                specificAnimalQtt = 0;
            else if (missionLevel == 2 && AreaSpawn.name == "AreaSpawnMedium")
                specificAnimalQtt = 0;
            else if (missionLevel == 1 && AreaSpawn.name == "AreaSpawnEasy")
                specificAnimalQtt = 0;

            if (specificAnimal != null && i < specificAnimalQtt)
                animalToSpawn = specificAnimal;
            else
                animalToSpawn = animalPrefabs[Random.Range(0, animalPrefabs.Length)];

            Vector3 areaSize = AreaSpawn.GetComponent<Renderer>().bounds.size;

            Vector3 spawnPosition = new Vector3(
                Random.Range(AreaSpawn.position.x - areaSize.x / 2, AreaSpawn.position.x + areaSize.x / 2),
                AreaSpawn.position.y,
                Random.Range(AreaSpawn.position.z - areaSize.z / 2, AreaSpawn.position.z + areaSize.z / 2)
            );

            GameObject spawnedAnimal = Instantiate(animalToSpawn, spawnPosition, Quaternion.identity, AreaSpawn);
            Vector3 parentScale = AreaSpawn.lossyScale;

            if (spawnedAnimal.name.EndsWith("(Clone)")) {
                spawnedAnimal.name = spawnedAnimal.name.Replace("(Clone)", "");
            }

            float size;

            if (spawnedAnimal.name == "Shark_PF") {
                size = Random.Range(2.5f, 4.0f);
                spawnedAnimal.transform.localScale = new Vector3(
                    size / parentScale.x,
                    size / parentScale.y,
                    size / parentScale.z
                );
            } else {
                size = Random.Range(1.0f, 2.0f);
                spawnedAnimal.transform.localScale = new Vector3(
                    size / parentScale.x,
                    size / parentScale.y,
                    size / parentScale.z
                );
            }
        }
    }
}
