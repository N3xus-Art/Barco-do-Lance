using UnityEngine;

public interface IOperable: IInteractble{
    Sprite GetSprite();
    void StartOperation();
    void FinalizeOperation();
}
