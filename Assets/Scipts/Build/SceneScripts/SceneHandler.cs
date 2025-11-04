using UnityEngine.SceneManagement;

public class SceneHandler{
    //Methods
    #region 
    public void LoadScene(string sceneName, LoadSceneMode sceneMode = LoadSceneMode.Single){
        SceneManager.LoadSceneAsync(sceneName, sceneMode);
    }
    #endregion
}
