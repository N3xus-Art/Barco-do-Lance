using UnityEngine;

public class JellyfishHandler : MarineAnimalHandler, IRescuable{
    //Methdos
    #region
    protected override void Start(){
        base.Start();
        name = "Medusa";
        speed = 1.0f;
        size = 0.1f; // 10cm
    }

    public void Rescue(){
        Debug.Log($"Você resgatou {name}!");
    }
    #endregion
}
