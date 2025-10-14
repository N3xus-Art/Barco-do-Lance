using UnityEngine;

public interface ICapturavel : IInteractble
{
    GameObject GetGameObject();
    Sprite GetSprite();
    void IniciarCaptura();
    // void AnimalAction();
}
