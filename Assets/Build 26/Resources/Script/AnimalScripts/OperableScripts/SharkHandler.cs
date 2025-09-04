using UnityEngine;

public class SharkHandler : MarineAnimalHandler, IOperable{
    //Methdos
    #region
    protected override void Start(){
        base.Start();
        name = "Tubarão";
        speed = 10.0f;
        size = 3.0f; // 300cm
    }

    public void Operate(){
        Debug.Log($"Você está operando {name}!");
    }
    #endregion
}
