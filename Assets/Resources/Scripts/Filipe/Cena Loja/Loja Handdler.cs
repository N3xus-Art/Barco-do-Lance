using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LojaHanddler : MonoBehaviour
{

public PlayerHandlerLoja Player;
public TMP_Text Texto;

    void Update()
    {

     Texto.SetText("Dinheiro: "+Player.DinDin);

    }

}
