using System;
using UnityEngine;

public class Comprahandler : MonoBehaviour
{

    public String Nome;

    public GameObject TelaConfirmacao;

    public void Comprar(){


        TelaConfirmacao.SetActive(true);


    }
}
