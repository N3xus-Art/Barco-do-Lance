using UnityEngine;

public class AbaHandler : MonoBehaviour
{
    public int ID;
    public SiteHandler Site;

    public void Aba()
    {
        Site.ID_Atual = ID;
    }
}
