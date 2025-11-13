using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TrashType { Wire, Barb, Barnacle, Hook }

public class TrashHandler : MonoBehaviour, IPointerClickHandler{

    public TrashType type;
    public void OnPointerClick(PointerEventData eventData) {
        TryRemove();
    }

    public void TryRemove() {
        ToolInstanceHandler equippedTool = PlayerInventoryHandler.Instance.EquippedTool;
        if (equippedTool == null) {
            Debug.Log("Nenhuma ferramenta equipada!");
            return;
        }
        bool correct = false;
        ToolType equippedToolType = equippedTool.Data.type;

        if(type == TrashType.Barnacle && equippedToolType == ToolType.Knife)
            correct = true;
        //if((type == TrashType.Wire || type == TrashType.Barb || type == TrashType.Hook) && equippedToolType == ToolType.Pliers)
        //correct = true;

        if ((type == TrashType.Wire || type == TrashType.Barb || type == TrashType.Hook) && equippedToolType == ToolType.Knife)
            correct = true;

            if (correct) {
            if (PlayerInventoryHandler.Instance.TryUseEquipped()) {
                Destroy(gameObject);
                OperationUI.Instance.RemoveTrash();
                Debug.Log("Lixo removido!");

            } else {
                Debug.Log("A ferramenta quebrou ou não pôde ser usada!");
            }

        } else {
            Debug.Log("Ferramenta incorreta para esse lixo!");
        }
    }
}
