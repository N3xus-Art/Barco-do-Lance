using UnityEngine;

public class PeixeLeao : AnimalMarinho, ICapturavel
{
    public Sprite AnimalSprite;
    protected override void Start()
    {
        base.Start();
        nome = "Peixe Leão";
        velocidade = 3.5f;
        tamanho = 0.4f; // 40cm
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
