using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerHandler : MonoBehaviour{
    //Variables
    #region
    private static GameManagerHandler _instance;
    public static GameManagerHandler Instance{
        get{
            if (_instance == null){
                Debug.LogError("O GameManager não foi inicializado. Certifique-se de que está na cena!");
            }
            return _instance;
        }
    }
    public static SceneHandler SceneControlerHandler => Instance.sceneHandler;
    [SerializeField] private List<SeaIdHandler> SeaPrefabs = new List<SeaIdHandler>();
    [SerializeField] private GameObject currentSeaGO;
    [SerializeField] public static int currentSea = 0;
    [SerializeField] private SceneHandler sceneHandler;
    [SerializeField] public bool onScene;
    [SerializeField] private bool hasSpwn;
    [SerializeField] public static Scene CurrentScene;
    #endregion
    //Methods
    #region
    void SpawnOcean(int _currentSea) {
        SeaIdHandler sea = SeaPrefabs.First(item => item.GetSeaId == currentSea);
        GameObject ocean = Instantiate(sea.gameObject, new Vector3(0, -124.5f, 0), Quaternion.identity);
        List<AnimalSpawnerHandler> animalSpawners = ocean.GetComponentsInChildren<AnimalSpawnerHandler>().ToList<AnimalSpawnerHandler>();
        hasSpwn = true;
    }
    public void Awake(){
        _instance = this;
        DontDestroyOnLoad(gameObject);
        CurrentScene = SceneManager.GetActiveScene();
        hasSpwn = false;
        if (_instance != null && _instance != this){
            Destroy(gameObject);
            return;
        }
        if (!hasSpwn && CurrentScene.name == "GameScreen") { 
            SpawnOcean(currentSea);
        }
    } 
    #endregion

}
