using System.Data;
using UnityEngine;

public class AceitarHandler : MonoBehaviour
{
    public GameManager gameManager;

    public SiteHandler Site;

    public void Click()
    {
        if (Site.ProximasMissoes.Count > 0)
        {
            Site.MissoesDisponiveis[Site.ID_Atual] = Site.ProximasMissoes.Peek();
            Site.ProximasMissoes.Dequeue();
           
        }
    }
}
