using UnityEngine;

public class TurtleHandler : MarineAnimalHandler, IOperable{
    //Methdos
    #region
    protected override void Start(){
        base.Start();
        name = "Tartaruga";
        speed = 0.3f;
        size = 0.8f; // 80cm
    }

    public void Operate(){
        Debug.Log($"Você está operando {name}!");
    }
    #endregion
}
