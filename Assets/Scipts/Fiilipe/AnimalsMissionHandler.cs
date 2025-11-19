using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimalsMissionHandler : MonoBehaviour
{
    [SerializeField] private Image OperablesImage;
    [SerializeField] private TMP_Text OperablesText;
    
    
    [SerializeField] private Image RescuablesImage;
    [SerializeField] private TMP_Text RescuablesText;

    public void ChangeAnimals(ScriptableObjectMissions mission)
    {
        if (mission == null) { Debug.Log("Missão Nula, retornando"); return; }

        //Debug.Log("começou a função");
        if (mission.operableAnimals > 0) {

          //  Debug.Log("Tem animais operáveis, trocando imagem");

            OperablesText.gameObject.SetActive(true);
            OperablesImage.gameObject.SetActive(true);

            OperablesText.SetText($"X {mission.operableAnimals}");
            OperablesImage.sprite = mission.OperableImage;
        }
        else{ 
            //Debug.Log("Não tem animais operáveis, retirando as imagens");

            OperablesText.gameObject.SetActive(false);
            OperablesImage.gameObject.SetActive(false);

        }


        if (mission.rescueableAnimals > 0)
        {

            //Debug.Log("Tem animais resgatáveis, trocando imagem");

            RescuablesText.gameObject.SetActive(true);
            RescuablesImage.gameObject.SetActive(true);

            RescuablesText.SetText($"X {mission.rescueableAnimals}");
            RescuablesImage.sprite = mission.RescuableImage;
        }
        else
        {
            //Debug.Log("Não tem animais restáveis, retirando as imagens");

            RescuablesText.gameObject.SetActive(false);
            RescuablesImage.gameObject.SetActive(false);

        }

    }


}
