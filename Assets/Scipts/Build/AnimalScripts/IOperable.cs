using UnityEngine;

public interface IOperable{
    Sprite GetSprite();
    void StartOperation();
    void FinalizeOperation();
}
