using UnityEngine;
using UnityEngine.EventSystems;

public enum TipoLixo { Linha, Arame, Craca, Anzol }

public class Lixo : MonoBehaviour, IPointerClickHandler
{
    public TipoLixo tipo;

    public void OnPointerClick(PointerEventData eventData)
    {
        Ferramenta ferramenta = OperacaoUI.Instance.GetFerramentaAtual();

        if (ferramenta == null)
        {
            Debug.Log("Nenhuma ferramenta selecionada!");
            return;
        }

        TentarRemover(ferramenta);
    }

    public void TentarRemover(Ferramenta ferramenta)
    {
        bool correto = false;

        if (tipo == TipoLixo.Craca && ferramenta.tipo == TipoFerramenta.Faca)
            correto = true;

        if ((tipo == TipoLixo.Linha || tipo == TipoLixo.Arame || tipo == TipoLixo.Anzol)
            && ferramenta.tipo == TipoFerramenta.Alicate)
            correto = true;

        if (correto)
        {
            if (ferramenta.Usar())
            {
                Destroy(gameObject);
                Debug.Log("Lixo removido!");
            }
        }
        else
        {
            Debug.Log("Ferramenta incorreta para este lixo!");
        }
    }
}