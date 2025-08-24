using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Treet_Manager : MonoBehaviour{
    [SerializeField] public Item_SO item;

    void Update(){
        if (TableScript.inRange && Moving.isInteractingGlobal && !item.isPicked && !item.isActive) {
            item.Pick(true);
            item.Activate(true);
        }else if(TableScript.inRange && Moving.isInteractingGlobal&& item.isPicked && item.isActive){
            item.Pick(false); 
            item.Activate(false);
        }else if (Moving.isInteractingGlobal && item.isPicked && item.isActive){
            item.Use("Jogou", true);
            item.Use("", false);
        }
    }
}
