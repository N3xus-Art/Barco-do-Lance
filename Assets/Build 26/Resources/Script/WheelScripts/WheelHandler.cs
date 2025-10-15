using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WheelHandler : MonoBehaviour{
    //Variables
    #region
    [SerializeField] private bool inTrigger;
    [SerializeField] private SceneHandler sceneHandler;
    [SerializeField] private Scene scene;
    #endregion

    //Methdos
    #region
    //Trigger Methods
    private void OnTriggerEnter2D(Collider2D collision){
        inTrigger = true;
    }
    private void OnTriggerExit2D(Collider2D collision){
        inTrigger = false;
    }
    //Scene Methdos
    public void ChangeScene(){
        sceneHandler = new SceneHandler();
        scene = SceneManager.GetActiveScene();
        if (scene.name == "GameScreen"){
            sceneHandler.LoadScene("MapScreen");
            BuildInputHandler.isInteracting = false;
        }
        if (scene.name == "MapScreen"){
            sceneHandler.LoadScene("GameScreen");
            BuildInputHandler.isInteracting = false;
        }
    }
    //Unity Methdos
    public void Update() {
        if (inTrigger && BuildInputHandler.isInteracting) {
            ChangeScene();
        }
    }
    #endregion
}
