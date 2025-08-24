using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerNew : MonoBehaviour{
    static GameManagerNew Instance;

    void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(Instance != this){
            Destroy(gameObject);
        }
    }
    void Start(){
        Debug.Log(ItemHandlerNew.inventory[ItemHandlerNew.currentActive]);
    }
    void Update(){
        
        /*if (TableScript.inRange && Moving.isInteractingGlobal && !item.isPicked){
            item.Pick();
        }else if (TableScript.inRange && Moving.isInteractingGlobal && item.isPicked){
            item.Drop();
        }else if (!TableScript.inRange && Moving.isInteractingGlobal && item.isPicked){
            item.Use();
        }*/
    }
}
