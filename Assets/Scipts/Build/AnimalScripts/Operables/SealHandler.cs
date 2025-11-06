using System.Xml.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SealHandler : MarineAnimalHandler, IOperable {
    //Methdos
    #region
    protected override void Start() {
        base.Start();
        name = "Foca";
        speed = 5.0f;
    }
    public Sprite GetSprite() {
        return gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    public void StartOperation() {
        Time.timeScale = 0f;
        OperationUI.Instance.OpenOperation(this);
    }
    public void FinalizeOperation() {
        Debug.Log("Operação finalizada na foca.");
    }
    public void Interact(){
        StartOperation();
    }
    #endregion
}
