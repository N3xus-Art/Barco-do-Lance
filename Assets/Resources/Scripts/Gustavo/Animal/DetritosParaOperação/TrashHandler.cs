using UnityEngine;
using UnityEngine.EventSystems;


public class TrashHandler : MonoBehaviour, IPointerClickHandler
{
    public TipoLixo tipo;

    public void OnPointerClick(PointerEventData eventData)
    {
        TentarRemover();
    }

    public void TentarRemover()
    {
        ToolInstanceHandler ferramentaEquipada = PlayerInventoryHandler.Instance.EquippedTool;

        if (ferramentaEquipada == null)
        {
            Debug.Log("Nenhuma ferramenta equipada!");
            return;
        }

        bool correto = false;
        TipoFerramenta tipoFerramentaEquipada = ferramentaEquipada.Data.tipo;

        if (tipo == TipoLixo.Craca && tipoFerramentaEquipada == TipoFerramenta.Faca)
            correto = true;

        if ((tipo == TipoLixo.Linha || tipo == TipoLixo.Arame || tipo == TipoLixo.Anzol)
            && tipoFerramentaEquipada == TipoFerramenta.Alicate)
            correto = true;

        if (correto)
        {
            if (PlayerInventory.Instance.TryUseEquipped())
            {
                Destroy(gameObject);
                Debug.Log("Lixo removido!");
            }
            else
            {
                Debug.Log("A ferramenta quebrou ou não pôde ser usada!");
            }
        }
        else
        {
            Debug.Log("Ferramenta incorreta para este lixo!");
        }
    }
}