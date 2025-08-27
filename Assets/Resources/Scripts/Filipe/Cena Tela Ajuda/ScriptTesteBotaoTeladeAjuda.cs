using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class ScriptTesteBotaoTeladeAjuda : MonoBehaviour
{

    public ScriptPlayerTelaAjuda TelaAjudaController;

    public void DeletarSiProprio(){

        TelaAjudaController.numBotoes --;     
        gameObject.SetActive(false);
    }
    
}
