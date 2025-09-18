using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class ScriptFade : MonoBehaviour
{
    [SerializeField]
    GameObject ImagemFade, CanvaObject;
    
    bool teste = false;
    SceneController sceneController;

    public void FadeIntoScene(GameObject FadeImage, string SceneName) {

        if (FadeImage.GetComponent<CanvasGroup>().alpha < 1)
        {

            FadeImage.GetComponent<CanvasGroup>().alpha += Time.deltaTime;

        }
        else if (FadeImage.GetComponent<CanvasGroup>().alpha >= 1) {
            sceneController = new SceneController();
            teste = true;
            DontDestroyOnLoad(CanvaObject);
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(FadeImage);
            sceneController.LoadScene(SceneName);
        
        }
    
    }
    public void FadeOut(GameObject FadeImage) {

        if (FadeImage.GetComponent<CanvasGroup>().alpha > 0) {

            FadeImage.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;

        }

    }

   
    void Update()
    {
        if (!teste)
        {
            FadeIntoScene(ImagemFade, "Scene Transition 2");
        }
        else { FadeOut(ImagemFade); }
    }
}
