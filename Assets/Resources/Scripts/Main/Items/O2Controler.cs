using UnityEngine;
public class O2Controler : ItemHandler{
    [SerializeField] public float MaxOxygen, OxygenCost;
    [SerializeField] public GameObject Player;
    void Update(){
        this.Durability = this.Durability - OxygenCost;
        if (this.Durability < 0){
            this.Durability = 0;
            Destroy(Player);
        }}}