using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerHandler : MonoBehaviour{
    public Image BarraOxigenio;
    public float o2, MaxOxygen, OxygenCost;
    void Update(){

        o2 = o2 - OxygenCost;
        BarraOxigenio.fillAmount = o2 / MaxOxygen; 

        if (o2 < 0) {
            o2 = 0;
            Destroy(gameObject);
        }

    }
}
