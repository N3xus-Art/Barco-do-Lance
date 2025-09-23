using UnityEngine;

public class Tartaruga : AnimalMarinho, IOperavel
{
    protected override void Start()
    {
        base.Start();
        nome = "Tartaruga";
        velocidade = 0.3f;
        tamanho = 0.8f; // 80cm
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
        Debug.Log("Operação finalizada na tartaruga.");
    }
    private void OnMouseDown()
    {
        IniciarOperacao();
    }

}
