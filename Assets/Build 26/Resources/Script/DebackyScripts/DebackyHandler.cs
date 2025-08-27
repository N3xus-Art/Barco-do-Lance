using UnityEngine;

public class DebackyHandler : MonoBehaviour{
    //Variables
    #region
    [SerializeField] private bool inTrigger;
    [SerializeField] private GameObject keyGO;
    #endregion
    //Methods
    #region
    private void OnTriggerEnter2D(Collider2D collision){
        inTrigger = true;
        keyGO.SetActive(true);
        if (BuildInputHandler.isInteracting){
            Debug.Log("To no Debacky");
        }
    }
    private void OnTriggerExit2D(Collider2D collision){
        inTrigger = false;
        keyGO.SetActive(false);
    }
    #endregion
}
