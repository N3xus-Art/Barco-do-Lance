using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController{
    public void LoadScene(string sceneName, LoadSceneMode sceneMode = LoadSceneMode.Single){
        SceneManager.LoadSceneAsync(sceneName, sceneMode);
    }
}
