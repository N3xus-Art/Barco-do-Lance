using UnityEngine;
using UnityEngine.UIElements;

public class Lixo : MonoBehaviour
{
    [SerializeField] MouseDownEvent evt;

    private void OnMouseDown(MouseDownEvent evt)
    {
        bool leftMouseButtonPressed = 0 != (evt.pressedButtons & (1 << (int)MouseButton.LeftMouse));
        bool rightMouseButtonPressed = 0 != (evt.pressedButtons & (1 << (int)MouseButton.RightMouse));
        bool middleMouseButtonPressed = 0 != (evt.pressedButtons & (1 << (int)MouseButton.MiddleMouse));
        Debug.Log($"Mouse Down event. Triggered by {(MouseButton)evt.button}.");
        Debug.Log($"Pressed buttons: Left button: {leftMouseButtonPressed} Right button: {rightMouseButtonPressed} Middle button: {middleMouseButtonPressed}");
    }

    void OnMouseOver()
    {
        OnMouseDown(evt);
    }
    void OnMouseExit()
    {
        Debug.Log("Mouce saiu de: " + gameObject.name);        
    }
}
