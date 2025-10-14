using UnityEngine;

public class JellyfishHandler : AnimalMarinho, ICapturavel
{
    protected override void Start()
    {
        base.Start();
        nome = "Medusa";
        velocidade = 3.5f;
        // tamanho = 0.4f; // 40cm
    }
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
    public Sprite GetSprite()
    {
        return gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    public void IniciarCaptura()
    {
        Debug.Log($"Captura iniciada no animal {name}");
    }
    public void Interact()
    {
        // Abre a UI de captura
        CapturaUI.Instance.AbrirCaptura(this);
    }
}
