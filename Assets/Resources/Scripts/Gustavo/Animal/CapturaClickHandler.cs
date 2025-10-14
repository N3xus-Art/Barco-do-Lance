using UnityEngine;
using UnityEngine.EventSystems;

public class CapturaClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        CapturaUI.Instance.RegistrarClique();
    }
}