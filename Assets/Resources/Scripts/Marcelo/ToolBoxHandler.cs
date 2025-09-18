using System;
using UnityEngine;

public class ToolBoxHandler : MonoBehaviour
{
    [Header("----Variaveis de Trigger----")]
    [SerializeField] private GameObject keyGO;
    [SerializeField] private GameObject ToolBoxScreen;

    //Trigger Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ToolBoxScreen.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ToolBoxScreen.SetActive(false);
    }
}
