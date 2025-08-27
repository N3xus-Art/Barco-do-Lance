using System.Collections.Generic;
using UnityEngine;

public class ItemHandlerNew : MonoBehaviour{
    [SerializeField] private Item_SO item;
    [SerializeField] static public int currentActive;
    [SerializeField] static public List<Item_SO> inventory = new List<Item_SO>();
}
