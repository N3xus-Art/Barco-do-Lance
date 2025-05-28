using UnityEngine;

public class PlayerHandlerDeath : MonoBehaviour
{
    public float o2, MaxOxygen, OxigenCost;

    void Update()
    {
        o2 = o2 - OxigenCost;
    }

}
