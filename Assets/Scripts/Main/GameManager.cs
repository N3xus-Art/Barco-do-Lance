using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager Instance;
    SceneController sceneController;

    void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(Instance != this){
            Destroy(gameObject);
        }
    }

    void Start()
    {
        sceneController = new SceneController();
        sceneController.LoadScene("CENA JP", LoadSceneMode.Additive);
        
    }

    void Update()
    {
        
    }
}
