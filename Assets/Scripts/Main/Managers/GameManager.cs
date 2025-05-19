using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Cria uma instrancia estatica para apenas a classe alterar as propriedades dela
    static GameManager Instance;
    //Importa o SceneController
    SceneController sceneController;

    //Verifica se ja existe uma instancia na cena atual, caso tenha destroi e colocar esta no lugar, caso n�o tenha nada na instancia coloca as propriedades dessa
    void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(Instance != this){
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }
}
