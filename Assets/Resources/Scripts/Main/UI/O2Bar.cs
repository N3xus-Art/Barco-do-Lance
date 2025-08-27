using UnityEngine;
using UnityEngine.UI;
public class O2Bar : MonoBehaviour{
    public O2Controler o2;
    public Image oxygenBar;
    public void Update(){
            oxygenBar.fillAmount = o2.Durability / o2.MaxOxygen;
        }}
