using UnityEngine;

public class Medusa : AnimalMarinho, ICapturavel
{
    protected override void Start()
    {
        base.Start();
        nome = "Medusa";
        velocidade = 1.0f;
        // tamanho = 0.3f; // 30cm
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
        CapturaUI.Instance.AbrirCaptura(this);
    }
}
