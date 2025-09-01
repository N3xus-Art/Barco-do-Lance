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

    public void Operar()
    {
        Debug.Log("Você está operando a tartaruga!");
    }

}
