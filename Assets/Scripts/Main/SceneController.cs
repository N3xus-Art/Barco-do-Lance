using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//Cria a classe SceneController
public class SceneController 
{
    //cria o metodo LoadScene rescebendo o nome da cena e o modo como vai carregar a cena
    public void LoadScene(string sceneName, LoadSceneMode sceneMode = LoadSceneMode.Single){

        //Usa o metodo do LoadScene do SceneManager para carregar a cena com os parametros rescebidos
        SceneManager.LoadSceneAsync(sceneName, sceneMode);
    }
}
