using UnityEngine;

public class MuteHandler : MonoBehaviour
{
    public OptionsHandler OptionsHandler;

    public void Click()
    {

        OptionsHandler.Ligado ^= true;

    }
}