using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WheelHandler : MonoBehaviour{
    //Variables
    #region
    [SerializeField] public bool inTrigger;
    [SerializeField] public SceneHandler sceneHandler;
    [SerializeField] public Scene scene;
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
        scene = SceneManager.GetActiveScene();
        if (scene.name == "GameScreen"){
            SceneManager.LoadScene("MapScreen");
            BuildInputHandler.isInteracting = true;
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
