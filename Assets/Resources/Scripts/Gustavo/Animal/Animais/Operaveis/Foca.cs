using UnityEngine;

public class Foca : AnimalMarinho, IOperavel
{
    protected override void Start()
    {
        base.Start();
        nome = "Foca";
        velocidade = 5.0f;
        tamanho = 1.5f; // 1.5m
    }
    public Sprite GetSprite()
    {
        return gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    public void IniciarOperacao()
    {
        Time.timeScale = 0f;
        OperacaoUI.Instance.AbrirOperacao(this);
    }
    public void FinalizarOperacao()
    {
        Debug.Log("Operação finalizada na foca.");
    }
    private void OnMouseDown()
    {
        IniciarOperacao();
    }
}