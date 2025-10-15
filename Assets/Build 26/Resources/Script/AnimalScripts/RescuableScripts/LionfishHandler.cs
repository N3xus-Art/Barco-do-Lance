using UnityEngine;

public class LionfishHandler : MarineAnimalHandler, IRescuable{
    //Methdos
    #region
    protected override void Start(){
        base.Start();
        name = "Peixe Leão";
        speed = 3.5f;
        size = 0.4f; // 40cm
    }

    public void Rescue(){
        Debug.Log($"Você resgatou {name}!");
    }
    #endregion
}
