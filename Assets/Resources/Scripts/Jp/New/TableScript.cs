using UnityEngine;

public class TableScript : MonoBehaviour{
    static public bool inRange;
    private void OnTriggerEnter2D(Collider2D collision){
        inRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision){
        inRange = false;
    }
}
