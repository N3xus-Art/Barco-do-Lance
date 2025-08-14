using UnityEngine;

public class TempItem : MonoBehaviour{

    private bool isOn = false;
    public Moving Moving;

    private void OnTriggerEnter2D(Collider2D collision){
        isOn = true;
    }

    private void OnTriggerExit2D(Collider2D collision){
        isOn= false;
    }

    private void Update() {
        if (isOn) {
            if (Moving){

            }
        }
    }
}
