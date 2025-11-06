using UnityEngine;
using System.Xml.Linq;
using UnityEditor.Experimental.GraphView;

public class TurtleHandler : MarineAnimalHandler, IOperable {
    //Methdos
    #region
    protected override void Start() {
        base.Start();
        name = "Tartaruga";
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
        Debug.Log("Operação finalizada na tartaruga.");
    }
    public void Interact(){
        StartOperation();
    }
    #endregion
}
