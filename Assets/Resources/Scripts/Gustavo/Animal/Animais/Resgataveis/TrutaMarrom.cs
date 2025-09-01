using UnityEngine;

public class TrutaMarrom : AnimalMarinho, IResgatavel
{
    protected override void Start()
    {
        base.Start();
        nome = "Truta Marrom";
        velocidade = 5.0f;
        tamanho = 0.6f; // 60cm
    }

    public void Resgatar()
    {
        Debug.Log("Você resgatou a truta marrom!");
    }
}
