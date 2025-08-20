using JetBrains.Annotations;
using System;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Credits : MonoBehaviour
{
   SceneController controller;
    public void CreditoP()
    {
        controller = new SceneController();
        controller.LoadScene("Credits Screen P");
    }
    public void CreditoD()
    {
        controller = new SceneController();
        controller.LoadScene("Credits Screen D");
    }
    public void CreditoA()
    {
        controller = new SceneController();
        controller.LoadScene("Credits Screen A");
    }
}
