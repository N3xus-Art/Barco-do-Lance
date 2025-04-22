using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Cria uma instrancia estatica para apenas a classe alterar as propriedades dela
    static GameManager Instance;
    //Importa o SceneController
    SceneController sceneController;

    //Verifica se já existe uma instancia na cena atual, caso tenha destroi e colocar esta no lugar, caso não tenha nada na instancia coloca as propriedades dessa
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
        //cria o sceneController baseado no molde
       sceneController = new SceneController();
        //executa a aplicação de CENA JP em modo aditivo
       sceneController.LoadScene("CENA JP", LoadSceneMode.Additive);
        
    }

    void Update()
    {
        
    }
}
