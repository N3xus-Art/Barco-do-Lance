using System;
using UnityEngine;

public class Comprahandler : MonoBehaviour
{
    public String Nome;
    public GameObject TelaConfirmacao;

    public float Preco;

    public ConfirmarHanddler Confirmar;

    public void Comprar(){

        if (TelaConfirmacao.activeSelf == false){
            
            TelaConfirmacao.SetActive(true);

            Confirmar.Custo = Preco;

        }

    }
}
