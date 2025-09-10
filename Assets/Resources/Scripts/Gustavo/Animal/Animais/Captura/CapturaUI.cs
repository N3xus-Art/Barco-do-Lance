using UnityEngine.UI;
using UnityEngine;

public class CapturaUI : MonoBehaviour
{
    public static CapturaUI Instance;

    public GameObject painelCaptura;
    public Image animalImage;
    public Transform AreaCaptura;

    private ICapturavel alvoCaptura;


    private void Awake()
    {
        Instance = this;
        painelCaptura.SetActive(false);
    }

    public void AbrirCaptura(ICapturavel alvo)
    {
        alvoCaptura = alvo;

        painelCaptura.SetActive(true);
        animalImage.sprite = alvo.GetSprite();


    }
    public void EncerrarCaptura()
    {
        painelCaptura.SetActive(false);
        Destroy(alvoCaptura.GetGameObject());
        alvoCaptura = null;
    }
}
