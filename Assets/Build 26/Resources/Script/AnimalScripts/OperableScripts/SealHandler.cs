using UnityEngine;

public class SealHandler : MarineAnimalHandler, IOperable{
    //Methdos
    #region
    protected override void Start(){
        base.Start();
        name = "Foca";
        speed = 0.5f;
        size = 1.5f; // 80cm
    }

    public void Operate(){
        Debug.Log($"Você está operando {name}!");
    }
    #endregion
}
