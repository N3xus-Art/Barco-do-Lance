using UnityEngine;

public interface IOperavel : IInteractble
{
    Sprite GetSprite();
    void IniciarOperacao();
    void FinalizarOperacao();

}
