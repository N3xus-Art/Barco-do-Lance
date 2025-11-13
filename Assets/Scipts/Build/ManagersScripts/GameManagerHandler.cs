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
    [SerializeField] private PlayerControlerHandler pcHandler;
    [SerializeField] public bool fishSpwn = false;
    [SerializeField] public ScriptableObjectMissions[] CurrentAvaliableMissions = new ScriptableObjectMissions[] { };

    #endregion

    public static void SpawnAnimalsInCurrentSea(CurrentMission mission){
        Debug.Log($"GameManagerHandler.SapwnOcean - ocean.GetComponentsInChildren<AnimalSpawnerHandler>() {Instance.currentSeaGO.GetComponentsInChildren<AnimalSpawnerHandler>().Length}");
        List<AnimalSpawnerHandler> animalSpawners = new List<AnimalSpawnerHandler>(Instance.currentSeaGO.GetComponentsInChildren<AnimalSpawnerHandler>());

        animalSpawners[mission.Level - 1].SpawnAnimal(mission.OperableAnimals);
        animalSpawners[mission.Level - 1].SpawnAnimal(mission.RescueableAnimals,1);

    } 
    //Methods
    #region
    void SpawnOcean(int _currentSea) {
        SeaIdHandler sea = SeaPrefabs.First(item => item.GetSeaId == currentSea);
        currentSeaGO = Instantiate(sea.gameObject, new Vector3(0, -124.5f, 0), Quaternion.identity);
        currentSea = sea.GetSeaId;
        hasSpwn = true;
    }
    public void Awake(){
        if (_instance != null && _instance != this){
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    } 

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CurrentScene = SceneManager.GetActiveScene();
        Debug.Log($"GameManagerHandler.OnSceneLoaded - Carregou a cena {CurrentScene.name} - hasSpwn: {hasSpwn}");
        if (!hasSpwn && CurrentScene.name == "GameScreen"){
            Debug.Log($"GameManagerHandler.OnSceneLoaded - Carregou a cena {scene.name}"); 
            SpawnOcean(currentSea);
            SceneManager.activeSceneChanged += OnChangeScene;
            onScene = true;
            if (pcHandler.CurrentMission != null && !fishSpwn && (pcHandler.CurrentMission.MissionSea == currentSea)){
                SpawnAnimalsInCurrentSea(pcHandler.CurrentMission);
                fishSpwn = true;
            }
            if (pcHandler.CurrentMission != null && (pcHandler.CurrentMission.RescubleSea == currentSea)){

                Debug.Log("Entregou os peixes");
                
                // Checar se ele tem peixes no aquário
                    //Checar quantos peixes ele tem no aquário
                    //Tirar os peixes do aquário
                    //Diminuir o número de peixes resgatáveis da missão
                
            }

        }
    }

    public void OnChangeScene(Scene currentScene, Scene newScene){
        Debug.Log($"GameManagerHandler.OnChangeScene - currentScene = {currentScene.name} - newScene = {newScene} - hasSpw = {hasSpwn}"); 
        hasSpwn = false;
        fishSpwn = false;
    }

    #endregion

}
