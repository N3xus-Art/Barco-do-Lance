using UnityEngine;

public class Tubarao : AnimalMarinho, IOperavel
{
    public Sprite animalSprite;
    protected override void Start()
    {
        base.Start();
        nome = "Tubarão";
        velocidade = 10.0f;
        tamanho = 3.0f; // 3m
    }

    public Sprite GetSprite()
    {
        return animalSprite;
    }

    public void IniciarOperacao()
    {
        Time.timeScale = 0f;
        OperacaoUI.Instance.AbrirOperacao(this);
    }
    public void FinalizarOperacao()
    {
        Debug.Log("Operação finalizada no tubarão.");
    }
    private void OnMouseDown()
    {
        IniciarOperacao();
    }
}
