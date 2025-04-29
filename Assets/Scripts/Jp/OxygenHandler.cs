using UnityEngine;
using UnityEngine.UI;

public class OxygenHandler : MonoBehaviour{
    public Image BarraOxigenio;
    public GameObject playerObject;
    public GameObject playerDestroy = GameObject.Find("Player");
    public float MaxOxygen, OxygenCost, Oxygen;

    void Start()
    {
        Oxygen = playerObject.GetComponent<PlayerHandler>().o2;
    }

    void Update(){
        Oxygen -= OxygenCost;

        if (Oxygen < 0) {
            Oxygen = 0;
            Debug.Log(playerDestroy);
        }
        BarraOxigenio.fillAmount = Oxygen / MaxOxygen; 
    }
}
