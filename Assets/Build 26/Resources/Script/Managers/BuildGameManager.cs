using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildGameManager : MonoBehaviour{
    //Variables
    #region
    static BuildGameManager Instance;
    #endregion

    //Methods
    #region
    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(Instance != this){
            Destroy(gameObject);
        }
    }
    #endregion

}
