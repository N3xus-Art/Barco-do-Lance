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
    [SerializeField] private bool isPaused;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject confirmExit;
    [SerializeField] private GameObject buttons;
    [SerializeField] private SceneHandler sceneHandler;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button configButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button backButton;
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
        Button resume = resumeButton.GetComponent<Button>();
        Button config = configButton.GetComponent<Button>();
        Button home = homeButton.GetComponent<Button>();
        Button quit = quitButton.GetComponent<Button>();
        Button confirm = confirmExit.GetComponent<Button>();
        Button back = backButton.GetComponent<Button>();
        resume.onClick.AddListener(ResumeGame);
        config.onClick.AddListener(Config);
        home.onClick.AddListener(Home);
        quit.onClick.AddListener(Confirm);
        //confirm.onClick.AddListener(Quit);
        back.onClick.AddListener(Back);
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
    public void Home(){
        sceneHandler = new SceneHandler();
        sceneHandler.LoadScene("HomeScreen");
    }
    public void Confirm(){
        confirmExit.SetActive(true);
        buttons.SetActive(false);
    }
    public void Back(){
        confirmExit.SetActive(false);
        buttons.SetActive(true);
    }
    public void Quit(){
        Application.Quit();
    }
    #endregion
}
