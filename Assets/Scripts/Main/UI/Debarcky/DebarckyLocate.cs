using UnityEngine;

public class DebarckyLocate : MonoBehaviour{
    public PlayerInputHandler playerInputHandler;
    public GameObject Key, Player, betaDebarcky;
    public float range = 2f;
    public bool isColliding;

    public void OnCollisionEnter2D(Collision2D collision){
          if (collision.gameObject.name == "Player"){
                isColliding = true;
         }
    }

    public void OnCollisionExit2D(Collision2D collision){
         if (collision.gameObject.name == "Player"){
            isColliding = false;
            betaDebarcky.SetActive(false);
        }
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
            }
        }
    }

}
