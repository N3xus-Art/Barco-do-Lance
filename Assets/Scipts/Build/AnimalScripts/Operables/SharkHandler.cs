using UnityEngine;

public class SharkHandler : MarineAnimalHandler, IOperable {
    //Methdos
    #region
    protected override void Start() {
        base.Start();
        name = "Tubarão";
        speed = 5.0f;
        SetCanInteract(true);
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
    public void Interact()
    {
        if (canInteract) { StartOperation(); }
    }

    public void SetCanInteract(bool Can)
    {

        canInteract = Can;

    }
    #endregion
}
