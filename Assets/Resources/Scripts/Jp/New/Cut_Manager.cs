using UnityEngine;

public class Cut_Manager : MonoBehaviour{
    [SerializeField] private Item_SO item;

    private void Update(){
        if (item != null && item.isActive) {
            //item.Use("Cortado");
        }
    }

}
