using UnityEngine;

public class WaterHandler : MonoBehaviour{
    //Variables
    #region
    static public bool onWater;
    [SerializeField] private new Collider2D collider2D;
    #endregion

    //Methods
    #region
    private void OnTriggerEnter2D(Collider2D collision){
        onWater = true;
    }
    private void OnTriggerExit2D(Collider2D collision){
        onWater= false;
    }

    #endregion
}
