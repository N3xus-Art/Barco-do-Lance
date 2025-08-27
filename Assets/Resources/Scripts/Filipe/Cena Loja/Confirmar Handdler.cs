using Unity.VisualScripting;
using UnityEngine;

public class ConfirmarHanddler : MonoBehaviour
{
    public PlayerHandlerLoja Player;

    public GameObject TelaConfirmacao;
    public GameObject TelaFaltaGrana;

    public float Custo;

    public void Confirmar(){

        if (Player.DinDin < Custo){

            TelaFaltaGrana.SetActive(true);
            return;

        }

        Player.DinDin -= Custo;
        TelaConfirmacao.SetActive(false);

    }

    public void Cancelar(){

        TelaConfirmacao.SetActive(false);

    }

    public void Continuar(){

        TelaConfirmacao.SetActive(false);
        TelaFaltaGrana.SetActive(false);

    }

}
