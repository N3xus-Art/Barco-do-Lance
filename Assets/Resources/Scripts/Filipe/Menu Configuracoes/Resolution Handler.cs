using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionHandler : MonoBehaviour
{

    public string[] Resolucoes    = { "1920x1080","1600x1200", "1000x1000" };
    public int[] ResolucoesWidth  = { 1920,1600, 1000 };
    public int[] ResolucoesHeight = { 1080,1200, 1000 };

    public int Index = 0;
    public int MaxIndex;

    public TMP_Text ResolutionText;
    
    public void Update()
    {
        
        ResolutionText.text = Resolucoes[Index];

        MaxIndex = Resolucoes.Length - 1;

        Screen.SetResolution(ResolucoesWidth[Index], ResolucoesHeight[Index], false);
    }

}

