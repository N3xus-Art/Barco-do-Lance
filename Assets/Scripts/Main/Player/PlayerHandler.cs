using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerHandler : ItemHandler
{
    [SerializeField]
    public float o2, MaxOxygen, OxygenCost;
    [SerializeField]
    public ItemHandler net, sambura, cut, treat;

    void Update(){

        o2 = o2 - OxygenCost;

        if (o2 < 0) {
            o2 = 0;
            Destroy(gameObject);
        }


    }



}
