using Unity.VisualScripting;
using UnityEngine;

public class Treet_Manager : MonoBehaviour{
    [SerializeField] private Item_SO item;

    private void Update(){
        if (item != null && item.isActive) {
            item.Use("Jogado");
        }
    }
}
