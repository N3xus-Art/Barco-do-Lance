using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SiteHandler : MonoBehaviour
{
    public Image Imagem_Animal;
    
    public TMP_Text Descricao_Animal;
    public TMP_Text Texto_Valor_Missao;
    public TMP_Text Texto_Missao;
    
    public Sprite Sprite_Foca;
    public Sprite Sprite_Peixe_Boi;
    public Sprite Sprite_Leao_Marinho;
    
    public int Valor_Missao;
    public int MissaoAtual;

    public GameObject BotaoAtual;

    void Update(){

        switch(MissaoAtual)
        {

            case 1:
                Descricao_Animal.SetText("A foca é uma filha da puta do krl");
                Imagem_Animal.sprite = Sprite_Foca;

                Texto_Missao.SetText("Foca do krl burro pra porra e se prendeu nas coisas");

                break;
            case 2:
                Descricao_Animal.SetText("Peixe boi é foda dmsss bixão insano e engraçado kkkkkkkkkkk ");
                Imagem_Animal.sprite = Sprite_Peixe_Boi;

                Texto_Missao.SetText("Peixe Boi ta na casa dele, vai ver ele q ele é foda");

                break;
            case 3:
                Descricao_Animal.SetText("Leão Marinho tocando safoxone que se foda KKKKKKKKKKKK");
                Imagem_Animal.sprite = Sprite_Leao_Marinho;

                Texto_Missao.SetText("Leão Marinho tocando safoxone enfiou o saxofone no cu KKKKKKKKKKK");

            break;


        }

        Texto_Valor_Missao.SetText($"Valor: {Valor_Missao}R$");
    }
}
