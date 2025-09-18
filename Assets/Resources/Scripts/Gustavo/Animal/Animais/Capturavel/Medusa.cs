using UnityEngine;

public class Medusa : AnimalMarinho, ICapturavel
{
    public Sprite AnimalSprite;
    protected override void Start()
    {
        base.Start();
        nome = "Medusa";
        velocidade = 1.0f;
        tamanho = 0.3f; // 30cm
    }
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
    public Sprite GetSprite()
    {
        return AnimalSprite;
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
