using UnityEngine;

public class TrutaMarrom : AnimalMarinho, ICapturavel
{
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
        return gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    public void OnMouseDown()
    {
        // Abre a UI de captura
        CapturaUI.Instance.AbrirCaptura(this);
    }
    public void IniciarCaptura()
    {
        Debug.Log($"Captura iniciada no animal {name}");
    }

}
