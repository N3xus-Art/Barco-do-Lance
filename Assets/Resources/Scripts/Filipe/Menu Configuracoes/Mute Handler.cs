using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MuteHandler : MonoBehaviour
{
    public SliderHandler OptionsHandler;
    public UnityEngine.UI.Image Botao;
    public Sprite SpriteDesmutado;
    public Sprite SpriteMutado;


    public void Click()
    {

        OptionsHandler.Ligado ^= true;

        if(Botao.sprite == SpriteDesmutado) { Botao.sprite = SpriteMutado; } else { Botao.sprite = SpriteDesmutado; }
    
    }



}