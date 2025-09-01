using UnityEngine;

public class Tubarao : AnimalMarinho, IOperavel
{
    protected override void Start()
    {
        base.Start();
        nome = "Tubarão";
        velocidade = 10.0f;
        tamanho = 3.0f; // 3m
    }

    public void Operar()
    {
        Debug.Log("Você está operando o tubarão!");
    }
}
