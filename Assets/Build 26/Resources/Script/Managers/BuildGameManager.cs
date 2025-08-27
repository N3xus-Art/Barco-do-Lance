using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuildGameManager : MonoBehaviour {
    //Variables
    #region
    static BuildGameManager Instance;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject confirmExit;
    [SerializeField] private GameObject buttons;
    [SerializeField] private bool isPaused;
    [SerializeField] private SceneHandler sceneHandler;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button configButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button quitButton;
    #endregion

    //Methods
    #region
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (Instance != this) {
            Destroy(gameObject);
        }
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }
    public void PauseGame() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Config (){
        sceneHandler = new SceneHandler();
        sceneHandler.LoadScene("ConfigurationsScreen", LoadSceneMode.Additive);
    }
    public void Back(){
        sceneHandler = new SceneHandler();
        sceneHandler.LoadScene("HomeScreen");
    }
    public void Show(){
        confirmExit.SetActive(true);
        buttons.SetActive(false);
    }
    public void Hide(){
        confirmExit.SetActive(false);
        buttons.SetActive(true);
    }
    public void Quit(){
        Application.Quit();
    }

    #endregion

}
