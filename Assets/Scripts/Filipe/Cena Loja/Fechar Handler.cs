using UnityEngine;
using UnityEngine.SceneManagement;

public class FecharHandler : MonoBehaviour
{
    public void Fechar()
    {
        SceneManager.UnloadSceneAsync(gameObject.scene.name);
        Time.timeScale = 1;
    }
}
