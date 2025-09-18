using System.Collections.Generic;
using UnityEngine;

public class WheelHandler : MonoBehaviour{
    //Variables
    #region
    [Header("----Variaveis de Trigger----")]
    [SerializeField] private bool inTrigger;
    [SerializeField] private List<GameObject> seas = new List<GameObject>();
    [SerializeField] private GameObject currentSea;
    [SerializeField] private int counter = 0;
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
    //Unity Methdos
    public void Awake(){
        currentSea = seas[counter];
    }
    public void Update() {
        if (inTrigger && Input.GetMouseButtonDown(1)) {
            /*if (currentSea != seas[counter]) {
                currentSea.SetActive(false);
                seas[counter].SetActive(true);
                currentSea = seas[counter];
            }*/
            Debug.Log("getget");
        }
    }
    #endregion
}
