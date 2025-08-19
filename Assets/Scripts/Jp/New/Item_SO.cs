using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SO", menuName = "Scriptable Objects/Item")]
public class Item_SO : ScriptableObject{
    [SerializeField] public int InstanceId => GetInstanceID();
    [SerializeField] public int Id;
    [SerializeField] public int durability;
    [SerializeField] public bool isPicked, isDroped, isUsed, isActive;

    public void Pick(){
        if (TableScript.inRange) { 
            isPicked = true;
            Debug.Log("inRange");
        }
    }
    public void Drop(){
        isDroped = true;
    }
    public void Use(string texto){
        if (isPicked) { 
            isUsed = true;
            Debug.Log(texto);
        }
    }

    public void Activate(){
        isActive = true;
    }

    public void Deactivate(){
        isActive = false;
    }

    public void Update() {
        Debug.Log(TableScript.inRange);
        Debug.Log(isPicked);
        Debug.Log(isUsed);
    }
}
