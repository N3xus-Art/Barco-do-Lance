using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StickerHandler : MonoBehaviour
{
    [SerializeField] private int Index = 0;

    [SerializeField] private Image Image;
    [SerializeField] private TMP_Text Texto;

    [SerializeField] private BesgurinhaHandler Besgurinha;

    private void Awake()
    {
        Image = GetComponent<Image>();
        Texto = GetComponentInChildren<TMP_Text>();
        Besgurinha.ChangePageEvent += ChangeImage;
        Besgurinha.Stickers.Add(gameObject);
    }

    private void ChangeImage(int i)
    {
        Index += i * 4;

        if (Index <= Besgurinha.SpritesAnimals.Length - 1)
        {
            if (Besgurinha.SpritesAnimals[Index] != null)
            {
                Image.enabled = true;
                Texto.enabled = false;
                Image.sprite = Besgurinha.SpritesAnimals[Index];
            }
            else
            {
                Image.enabled = true;
                Texto.enabled = true;
                Texto.text = $"{Index + 1}";
                Image.sprite = Besgurinha.DefaultSprite;

            }
        }
        else
        {

            Image.enabled = false;

        }
    }

}


