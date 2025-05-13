using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerHandler : MonoBehaviour{
    public Image BarraOxigenio;
    public float o2, MaxOxygen, OxygenCost;
    public int durCut, durWeb, durTreat, durSanbu;
    [SerializeField]
    private bool nivel2;

    [SerializeField]
    private bool nivel3;

    void Update(){

        o2 = o2 - OxygenCost;
        BarraOxigenio.fillAmount = o2 / MaxOxygen; 

        if (o2 < 0) {
            o2 = 0;
            Destroy(gameObject);
        }

        if(nivel2){
            durCut = 20;
            durSanbu = 20;
            durTreat = 20;
            durWeb = 20;
        }else if(nivel3){
            durCut = 30;
            durSanbu = 30;
            durTreat = 30;
            durWeb = 30;
        }else{
            durCut = 10;
            durSanbu = 10;
            durTreat = 10;
            durWeb = 10;
        }

    }



}
