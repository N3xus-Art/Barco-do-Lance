using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestiaryHandler : MonoBehaviour
{
    [SerializeField] private Image Background;

    [SerializeField] private int Page;
    [SerializeField] private int PageMax;
    
    [SerializeField] private Button NextButton;
    [SerializeField] private Button PreviousButton;

    [SerializeField] private Sprite DefaultBackground;
    [SerializeField] private Sprite[] Sprites;

    private void Awake()
    {
        Background = GetComponentInChildren<Image>();
        PageMax = Sprites.Length - 1;
    }
    private void ChangePage(int i) {

        if (Page == PageMax && i == 1) {

            enabled = false;
            gameObject.GetComponent<BesgurinhaHandler>().enabled = true;
            Background.sprite = DefaultBackground;
            return;
        }

        Page += i;

        if (Page == 0) { PreviousButton.gameObject.SetActive(false); } else { PreviousButton.gameObject.SetActive(true); }

        Background.sprite = Sprites[Page];
    
    }
    private void OnEnable()
    {
        Background.sprite = Sprites[Page];

        NextButton.onClick.RemoveAllListeners();
        PreviousButton.onClick.RemoveAllListeners();

        NextButton.onClick.AddListener(() => ChangePage(1));
        PreviousButton.onClick.AddListener(() => ChangePage(-1));

    }
}
