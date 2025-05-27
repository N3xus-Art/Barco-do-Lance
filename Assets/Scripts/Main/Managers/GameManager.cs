using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager Instance;
    SceneController sceneController;
    public bool OnBoat;

    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(Instance != this){
            Destroy(gameObject);
        }
    }

    void Start(){

    }

    void Update(){
        Scene activeScene = SceneManager.GetActiveScene();
        sceneController = new SceneController();
        if (activeScene.name == "Scene Navigation"){
            OnBoat = true;
        }else{
            OnBoat = false;
        }

    }
}
