using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class CreditsMenuHandler : MonoBehaviour{
    //Variables
    #region
    [SerializeField] private List<GameObject> screens = new List<GameObject>();
    [SerializeField] private GameObject currentScreen;
    [SerializeField] private int counter = 0, absCounter = Mathf.Abs(0);
    [SerializeField] private Button rightButton, leftButton;
    #endregion

    //Methods
    #region
    public void Awake(){
        currentScreen = screens[absCounter];
        leftButton.onClick.AddListener(LeftClick);
        rightButton.onClick.AddListener(RightClick);
    }

    public void Update(){
        if (currentScreen != screens[absCounter]) { 
            currentScreen.SetActive(false);
            screens[absCounter].SetActive(true);
            currentScreen = screens[absCounter];
        }
    }
    public void RightClick(){
        counter++;
        if(counter > 2){
            counter = 0;
            absCounter = 0;
        }
        absCounter = Mathf.Abs(counter);
    }
    public void LeftClick(){
        counter--;
        if (counter < -2){
            counter = 0;
            absCounter = 0;
        }
        absCounter = Mathf.Abs(counter);
    }    
    #endregion
}
