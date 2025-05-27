using UnityEngine;

public class AbaHandler : MonoBehaviour
{
    public int Missao;
    public SiteHandler Site;
    
    public void Aba(){
        Site.MissaoAtual = Missao;

        Site.BotaoAtual = gameObject;
    }
}
