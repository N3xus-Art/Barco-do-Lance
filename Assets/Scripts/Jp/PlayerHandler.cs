using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerHandler : MonoBehaviour{
    public float o2, MaxOxygen, OxygenCost;
    void Update(){

        o2 = o2 - OxygenCost;
        if (o2 < 0) {
            o2 = 0;
            Destroy(gameObject);
        }

    }



}
