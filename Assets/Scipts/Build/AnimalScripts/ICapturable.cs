using UnityEngine;

public interface ICapturable: IInteractble{
    GameObject GetGameObject();
    Sprite GetSprite();
    void StartCapture();
    void Run(Vector3 posicaoDoPlayer);
    void ReturnBehaviour();
}
