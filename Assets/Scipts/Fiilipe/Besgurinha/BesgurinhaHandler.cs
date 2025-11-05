using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BesgurinhaHandler : MonoBehaviour
{

    [SerializeField] private int Page = 0;
    [SerializeField] private int PageMax;

    [SerializeField] private Button NextButton;
    [SerializeField] private Button PreviousButton;

    [SerializeField] public Sprite DefaultSprite;
    [SerializeField] public Sprite[] SpritesAnimals;

    [SerializeField] public List<GameObject> Stickers;

    public delegate void ChangePageDelegate(int i);
    public event ChangePageDelegate ChangePageEvent;


    public void ChangePageMethod(int i)
    {
        if (Page == 0 && i == -1)
        {
            enabled = false;
            gameObject.GetComponent<BestiaryHandler>().enabled = true;
            gameObject.GetComponent<BestiaryHandler>().Curiosities.enabled = true;

            foreach (GameObject S in Stickers)
            {

                S.SetActive(false);

            }

            return;
        }

        Page += i;

        if (Page == PageMax) { NextButton.gameObject.SetActive(false); } else { NextButton.gameObject.SetActive(true); }

        ChangePageEvent?.Invoke(i);
        
    }

    private void Awake()
    {

        PageMax = Mathf.CeilToInt((SpritesAnimals.Length - 1) / 4);

    }

    private void OnEnable()
    {
        foreach (GameObject S in Stickers)
        {

            S.SetActive(true);

        }

        ChangePageMethod(0);

        NextButton.onClick.RemoveAllListeners();
        PreviousButton.onClick.RemoveAllListeners();

        NextButton.onClick.AddListener(() => ChangePageMethod(1));
        PreviousButton.onClick.AddListener(() => ChangePageMethod(-1));
    }

}


