using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] public float Durability, CurentLevel, Upgrade2, Upgrade3, Cost, ItemID;

    public void Awake()
    {
        if (CurentLevel <= 0)
        {
            CurentLevel = 0;
        }
        else if (CurentLevel == 2)
        {
            Durability = Upgrade2;
        }
        else if (CurentLevel == 3)
        {
            Durability = Upgrade3;
        }
    }
}
