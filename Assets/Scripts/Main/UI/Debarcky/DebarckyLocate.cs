using UnityEngine;

public class DebarckyLocate : MonoBehaviour{
    public PlayerInputHandler playerInputHandler;
    public GameObject Key, Player, betaDebarcky;
    public float range = 2f;
    public bool isColliding;

    public void OnCollisionEnter(Collision collision){
          if (collision.gameObject.name == "Pc"){
                isColliding = true;
            Debug.Log("On2");
         }
        Debug.Log("On1");
    }

    public void OnCollisionExit(Collision collision){
         if (collision.gameObject.name == "PC"){
            isColliding = false;
            betaDebarcky.SetActive(false);
            Debug.Log("exit2");
        }
        Debug.Log("exit1");
    }

    void Update(){
        if (Vector3.Distance(transform.position, Player.transform.position) <= range){
                Key.SetActive(true);
        }
        else{
            Key.SetActive(false);
        }

        if (playerInputHandler.isInteracting){
            if (isColliding){ 
                betaDebarcky.SetActive(true);
                Debug.Log("intera2");
            }
            Debug.Log(isColliding);
        }
    }

}
