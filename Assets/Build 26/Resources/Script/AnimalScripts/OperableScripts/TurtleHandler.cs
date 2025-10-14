using UnityEngine;

public class TurtleHandler : AnimalMarinho, IOperavel
{
    protected override void Start()
    {
        base.Start();
        nome = "Tartaruga";
        velocidade = 1.5f;
        // tamanho = 3.0f; // 3m
    }
    public Sprite GetSprite()
    {
        return gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void IniciarOperacao()
    {
        Time.timeScale = 0f;
        OperacaoUIHandler.Instance.AbrirOperacao(this);
    }
    public void FinalizarOperacao()
    {
        Debug.Log("Operação finalizada no tubarão.");
    }
    public void Interact()
    {
        IniciarOperacao();
    }
}
