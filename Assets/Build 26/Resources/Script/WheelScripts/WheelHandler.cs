using System.Collections.Generic;
using UnityEngine;

public class WheelHandler : MonoBehaviour{
    //Variables
    #region
    [SerializeField] private bool inTrigger;
    [SerializeField] private SceneHandler sceneHandler;
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
        sceneHandler.LoadScene("MapScreen");
    }
    //Unity Methdos
    public void Update() {
        if (inTrigger && BuildInputHandler.isInteracting) {
            ChangeScene();
        }
    }
    #endregion
}
