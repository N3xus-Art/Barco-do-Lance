using UnityEngine;
using UnityEngine.EventSystems;

public class CaptureClickHandler : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData) {
        CaptureUI.Instance.RegisterClick();
    }
}
