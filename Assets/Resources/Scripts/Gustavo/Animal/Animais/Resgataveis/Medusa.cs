using UnityEngine;

public class Medusa : AnimalMarinho, IResgatavel
{
    protected override void Start()
    {
        base.Start();
        nome = "Medusa";
        velocidade = 1.0f;
        tamanho = 0.3f; // 30cm
    }

    public void Resgatar()
    {
        Debug.Log("Você resgatou a medusa!");
    }
}
