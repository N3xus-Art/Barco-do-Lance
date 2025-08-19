using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SO", menuName = "Scriptable Objects/Item")]
public class Item_SO : ScriptableObject{
     public int InstanceId => GetInstanceID();
     public int Id;
     public int durability;
     public bool isPicked, isUsed, isActive;

    public void Pick(bool _isPicked){
        isPicked = _isPicked;
    }
    public void Drop(){
        isPicked = false;
    }
    public void Use(string texto, bool _isUsed){
        isUsed = _isUsed;
        Debug.Log(texto);
    }

    public void Activate(bool _isActive){
        isActive = _isActive;
    }
}
