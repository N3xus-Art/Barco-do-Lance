using UnityEngine;

public class TrutaMarrom : AnimalMarinho, ICapturavel
{
    public Sprite AnimalSprite;
    protected override void Start()
    {
        base.Start();
        nome = "Truta Marrom";
        velocidade = 5.0f;
        tamanho = 0.6f; // 60cm
    }
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
    public Sprite GetSprite()
    {
        return AnimalSprite;
    }
    public void IniciarCaptura()
    {
        Time.timeScale = 0f;
        CapturaUI.Instance.AbrirCaptura(this);
    }
    private void OnMouseDown()
    {
        IniciarCaptura();
    }
}
