using UnityEngine;
using UnityEngine.SceneManagement;

public class SeasHandler : MonoBehaviour {
    //Variables
    #region
    [SerializeField] public int seaValue;
    [SerializeField] private bool isOn;
    [SerializeField] private SceneHandler sceneHandler;
    [SerializeField] private Scene scene;
    #endregion
    //Methods
    #region
    void OnTriggerEnter2D(Collider2D collider2D){
        isOn = true;
    }

    void OnTriggerExit2D(Collider2D collider2D){
        isOn = false;
        BuildInputHandler.isInteracting = false;
    }

    private void Start(){
        sceneHandler = new SceneHandler();
        scene = SceneManager.GetActiveScene();
    }
    private void Update(){
        if (isOn && BuildInputHandler.isInteracting){
            BuildGameManager.currentSea = seaValue;
            sceneHandler.LoadScene("GameScreen");
            if (DebackyHandler.CurrentMissionGO != null) {

                Debug.Log(DebackyHandler.CurrentMissionGO.GetComponent<CurrentMission>().missionReward);
            
            }
        }
    }
    #endregion


}
