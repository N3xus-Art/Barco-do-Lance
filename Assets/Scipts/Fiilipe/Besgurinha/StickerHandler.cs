using UnityEngine;
using UnityEngine.UI;

public class StickerHandler : MonoBehaviour
{
    [SerializeField] private int Index = 0;
    [SerializeField] private Image Image;
    [SerializeField] private BesgurinhaHandler Besgurinha;

    private void Awake()
    {
        Image = GetComponent<Image>();
        Besgurinha.ChangePageEvent += ChangeImage;
    }

    private void ChangeImage(int i)
    {
        Index += i * 4;

        if (Index <= Besgurinha.Sprites.Length - 1)
        {
            if (Besgurinha.Sprites[Index] != null)
            {
                Image.enabled = true;
                Image.sprite = Besgurinha.Sprites[Index];
            }
            else
            {
                Image.sprite = Besgurinha.DefaultSprite;

            }
        }
        else
        {

            Image.enabled = false;

        }
    }

}


