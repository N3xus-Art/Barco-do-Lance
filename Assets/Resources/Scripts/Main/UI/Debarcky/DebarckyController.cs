using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DebarckyController : MonoBehaviour
{
    SceneController sceneController;
    public void Medi(){
        sceneController = new SceneController();
        sceneController.LoadScene("Mediterranean Sea");
    }

    public void Cari(){
        sceneController = new SceneController();
        sceneController.LoadScene("Caribbean Sea");
    }
    public void Red(){
        sceneController = new SceneController();
        sceneController.LoadScene("Red Sea");
    }

    public void Boat(){
        sceneController = new SceneController();
        sceneController.LoadScene("Scene Navigation");
    }
}
