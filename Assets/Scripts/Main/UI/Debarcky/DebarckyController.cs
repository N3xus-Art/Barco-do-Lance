using UnityEngine;
using UnityEngine.SceneManagement;

public class DebarckyController : MonoBehaviour
{
    [SerializeField] GameObject betaDebarcky;
    SceneController sceneController;
    public void Medi()
    {
        sceneController = new SceneController();
        sceneController.LoadScene("Caribbean Sea");
    }

    public void Cari()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        sceneController = new SceneController();
        sceneController.LoadScene("Mediterranean Sea");
    }

    public void Red()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        sceneController = new SceneController();
        sceneController.LoadScene("Red Sea");
    }

    public void OnCollisionEnter2D(Collision2D collision){
            betaDebarcky.SetActive(true);
    }

    public void OnCollisionExit2D(Collision2D collision){
            betaDebarcky.SetActive(false);
    }

}
