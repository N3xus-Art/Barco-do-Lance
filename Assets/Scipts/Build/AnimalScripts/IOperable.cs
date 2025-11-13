using UnityEngine;

public interface IOperable: IInteractable{
    Sprite GetSprite();
    void StartOperation();
    void FinalizeOperation();
}
