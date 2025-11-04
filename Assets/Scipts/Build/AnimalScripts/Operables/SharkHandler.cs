using UnityEngine;

public class SharkHandler : MarineAnimalHandler, IOperable {
    //Methdos
    #region
    protected override void Start() {
        base.Start();
        name = "Tubarão";
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
        Debug.Log("Operação finalizada na tubarão.");
    }
    private void OnMouseDown() {
        StartOperation();
    }
    #endregion
}
