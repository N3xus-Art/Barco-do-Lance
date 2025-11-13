using UnityEngine;

public interface ICapturable: IInteractable{
    GameObject GetGameObject();
    Sprite GetSprite();
    void StartCapture();
    void Run(Vector3 posicaoDoPlayer);
    void ReturnBehaviour();
}
