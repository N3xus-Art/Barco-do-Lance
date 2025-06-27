using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SiteHandler : MonoBehaviour
{
    public Image Imagem_Animal;
    
    public TMP_Text Descricao_Animal;
    public TMP_Text Texto_Valor_Missao;
    public TMP_Text Texto_Missao;
    
    public Sprite Sprite_Foca;
    public Sprite Sprite_Peixe_Boi;
    public Sprite Sprite_Sapo;
    public Sprite Sprite_Leao_Marinho;
    public Sprite Sprite_Tatu;

    public int Valor_Missao;

    public int ID_Atual;

    public Queue<int> ProximasMissoes = new Queue<int>();

    [HideInInspector]
    public int[] MissoesDisponiveis = new int[3] { 0, 1, 2 };

    void Start()
    {
        ProximasMissoes.Enqueue(3);
        ProximasMissoes.Enqueue(4);
        ProximasMissoes.Enqueue(5);
        ProximasMissoes.Enqueue(6);
        ProximasMissoes.Enqueue(7);
        ProximasMissoes.Enqueue(8);
        ProximasMissoes.Enqueue(9);
    }

    void Update()
    {

        switch (MissoesDisponiveis[ID_Atual])
        {

            case 0:
                Descricao_Animal.SetText("A foca é uma filha da puta do krl");
                Imagem_Animal.sprite = Sprite_Foca;

                Texto_Missao.SetText("Foca do krl burro pra porra e se prendeu nas coisas");

                break;
            case 1:
                Descricao_Animal.SetText("Peixe boi é foda dmsss bixão insano e engraçado kkkkkkkkkkk ");
                Imagem_Animal.sprite = Sprite_Peixe_Boi;

                Texto_Missao.SetText("Peixe Boi ta na casa dele, vai ver ele q ele é foda");

                break;
            case 2:
                Descricao_Animal.SetText("Leão Marinho tocando safoxone que se foda KKKKKKKKKKKK");
                Imagem_Animal.sprite = Sprite_Leao_Marinho;

                Texto_Missao.SetText("Leão Marinho tocando safoxone enfiou o saxofone no cu KKKKKKKKKKK");

                break;

            case 3:

                Descricao_Animal.SetText("Tatu vai comer teu cuKKKKKKKKKKKK");
                Imagem_Animal.sprite = Sprite_Tatu;

                Texto_Missao.SetText("O tatu comeu seu cu KKKKKKKKKKK e morreu todo fudido o fuleco");

                break;

            case 4:

                Descricao_Animal.SetText("Sapos passos cus");
                Imagem_Animal.sprite = Sprite_Sapo;

                Texto_Missao.SetText("Sapos passos cus");

                break;

            case 5:



                break;

            case 6:



                break;

            case 7:



                break;

            case 8:



                break;

            case 9:


            
                break;

        }

        Texto_Valor_Missao.SetText($"Valor: {Valor_Missao}R$");

        Debug.Log("ID ATUAL: "+ID_Atual);
        Debug.Log("MISSÃO ATUAL: " + MissoesDisponiveis[ID_Atual]);

    }
}
