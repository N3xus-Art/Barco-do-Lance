using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SiteHandler : MonoBehaviour
{
    public float DinDin;
    public TMP_Text Texto_DinDin;
    public Image Imagem_Animal;
    
    public TMP_Text Descricao_Animal;
    public TMP_Text Texto_Valor_Missao;
    public TMP_Text Texto_Missao;

    public int Valor_Missao;

    public int ID_Atual;


    public List<ScriptableObjectMissions> ProximasMissoes = new List<ScriptableObjectMissions> ();

    public ScriptableObjectMissions[] MissoesDisponiveis = new ScriptableObjectMissions[] { };


    void Update()
    {
        Imagem_Animal.sprite = MissoesDisponiveis[ID_Atual].animalSprite;
        Texto_Valor_Missao.SetText($"Recompensa: { MissoesDisponiveis[ID_Atual].missionReward } R$");

        Texto_DinDin.SetText($"R${DinDin}");

    }
}
