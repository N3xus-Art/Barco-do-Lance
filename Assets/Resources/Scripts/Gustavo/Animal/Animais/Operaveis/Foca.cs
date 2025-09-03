using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Foca : AnimalMarinho, IOperavel
{
    protected override void Start()
    {
        base.Start();
        nome = "Foca";
        velocidade = 5.0f;
        tamanho = 1.5f; // 1.5m
    }

     public void Operar()
    {
        Debug.Log("Você está operando a foca!");
    }
}