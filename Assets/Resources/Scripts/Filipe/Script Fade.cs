using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class ScriptFade : MonoBehaviour
{
    [SerializeField]
    Image ImagemFade;
    
    bool teste = false;
    SceneController sceneController;

    public void FadeIntoScene(Image FadeImage, string SceneName) {

        if (FadeImage.GetComponent<CanvasGroup>().alpha < 1)
        {

            FadeImage.GetComponent<CanvasGroup>().alpha += Time.deltaTime;

        }
        else if (FadeImage.GetComponent<CanvasGroup>().alpha >= 1) {
            sceneController = new SceneController();
            bool teste = true;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(FadeImage.gameObject);
            // Fade out 
            sceneController.LoadScene(SceneName);
        
        }
    
    }

   
    void Update()
    {
        if (!teste)
        {
            FadeIntoScene(ImagemFade, "Scene Transition 2");
        }
    }
}
