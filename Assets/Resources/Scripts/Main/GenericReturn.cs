using UnityEngine;

public class GenericReturn : MonoBehaviour
{
    SceneController sceneController;
    public void Return()
    {
        sceneController = new SceneController();
        sceneController.LoadScene("Home Screen");
    }
}
