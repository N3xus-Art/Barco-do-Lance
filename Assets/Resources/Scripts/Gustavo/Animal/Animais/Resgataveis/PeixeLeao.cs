using UnityEngine;

public class PeixeLeao : AnimalMarinho, IResgatavel
{
    protected override void Start()
    {
        base.Start();
        nome = "Peixe Leão";
        velocidade = 3.5f;
        tamanho = 0.4f; // 40cm
    }

    public void Resgatar()
    {
        Debug.Log("Você resgatou o peixe leão!");
    }
}
