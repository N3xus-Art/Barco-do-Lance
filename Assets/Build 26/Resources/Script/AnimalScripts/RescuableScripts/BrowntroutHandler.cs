using UnityEngine;

public class BrowntroutHandler : MarineAnimalHandler, IRescuable{
    //Methdos
    #region
    protected override void Start(){
        base.Start();
        name = "Truta Marrom";
        speed = 5.0f;
        size = 0.6f; // 60cm
    }

    public void Rescue(){
        Debug.Log($"Você resgatou {name}!");
    }
    #endregion
}
