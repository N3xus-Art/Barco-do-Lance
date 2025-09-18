using UnityEngine;

public class AnimalSpawnerHandler : MonoBehaviour{
    //Variable
    #region
    public GameObject[] animalPrefabs;
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4f;
    public float maxY = 4f;
    public Transform boxPos;
    public Vector2 boxSize = new Vector2(0, 0);
    #endregion

    //Methods
    #region
    private void OnDrawGizmosSelected(){
        boxSize = new Vector2(maxX, maxY);
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(boxPos.position, boxSize);
    }
    void Start(){
        // Exemplo: spawna 5 animais ao iniciar
        for (int i = 0; i < 5; i++){
            SpawnAnimal();
        }
    }
    void SpawnAnimal(){
        // Escolhe um prefab aleatoriamente do array
        GameObject animalToSpawn = animalPrefabs[Random.Range(0, animalPrefabs.Length)];

        // Gera uma posição aleatória dentro da área definida
        Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        // Instancia o animal na posição aleatória
        Instantiate(animalToSpawn, spawnPosition, Quaternion.identity);
    }
    #endregion
}
