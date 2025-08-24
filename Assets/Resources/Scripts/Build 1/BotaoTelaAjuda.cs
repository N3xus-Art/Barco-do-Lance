using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class ScriptBotaoTelaDeAjuda : MonoBehaviour
{

    public ScriptTelaAjuda TelaAjudaController;

    public void DeletarSiProprio(){

        TelaAjudaController.numBotoes --;     
        gameObject.SetActive(false);
    }
    
}
