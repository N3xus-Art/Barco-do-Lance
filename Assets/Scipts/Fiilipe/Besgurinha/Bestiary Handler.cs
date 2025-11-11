using System.Collections.Generic;
using TMPro;
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

    [SerializeField]
    [TextArea] private string[] Strings;

    [SerializeField] public TMP_Text Curiosities;


    private void Awake()
    {
        Background = GetComponentInChildren<Image>();
        Curiosities = GetComponentInChildren<TMP_Text>();
        PageMax = Sprites.Length - 1;
    }
    private void ChangePage(int i) {

        if (Page == PageMax && i == 1) {

            enabled = false;
            Curiosities.enabled = false;
            gameObject.GetComponent<BesgurinhaHandler>().enabled = true;
            Background.sprite = DefaultBackground;
            return;
        }

        Page += i;

        if (Page == 0) { PreviousButton.gameObject.SetActive(false); } else { PreviousButton.gameObject.SetActive(true); }

        Background.sprite = Sprites[Page];
        Curiosities.text  = Strings[Page];

    }
    private void OnEnable()
    {
        Background.sprite = Sprites[Page];
        Curiosities.text  = Strings[Page];


        NextButton.onClick.RemoveAllListeners();
        PreviousButton.onClick.RemoveAllListeners();

        NextButton.onClick.AddListener(() => ChangePage(1));
        PreviousButton.onClick.AddListener(() => ChangePage(-1));

    }
}
