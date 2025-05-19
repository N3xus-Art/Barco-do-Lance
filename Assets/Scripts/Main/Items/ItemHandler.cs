using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] int Durability;
    [SerializeField] int CurentLevel;
    [SerializeField] int Upgrade2;
    [SerializeField] int Upgrade3;
    [SerializeField] int Cost;
    [SerializeField] int ID;

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
