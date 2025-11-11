using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private int MaxO2;
    [SerializeField] private float CurrentO2;
    [SerializeField] private FadeHandler _FadeHandler;
    [SerializeField] private MoveTester _MoveTester;
    [SerializeField] private Transform _Transform;
    [SerializeField] private bool Dead;
    [SerializeField] private bool _Recover;
    [SerializeField] private float Speed;
    [SerializeField] private TMP_Text _Text;

    private void Awake()
    {
        _MoveTester = gameObject.GetComponent<MoveTester>();
        _Transform =  gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        if (CurrentO2 > 0)
        {
            // diminui o o2
            CurrentO2 -= Speed * Time.deltaTime;

            // checa se o O2 acabou
            if (CurrentO2 <= 0)
            {
                //toca a animação de fade
                _FadeHandler.FadeOut(4,Morrer);

            }
        }


        if (_Recover)
        {

            Recover();

        }
        
    }

    public void Morrer()
    {

        // move o player pro inicio
        _Transform.position = Vector3.zero;

        Dead = true;
        
        StartCoroutine(CutsceneMorte());

    }

    public void Spawn() { 
    
        //reabalita as funções
        _MoveTester.enabled = true;
        CurrentO2 = MaxO2;
        Dead = false;
    
    }

    public void Recover()
    {
        CurrentO2 = MaxO2;
        _FadeHandler.StopFade();

    }


    private IEnumerator CutsceneMorte()
    {

        // desabilita a o movimento
        _MoveTester.enabled = false;

        // espera meio segundo
        yield return new WaitForSecondsRealtime(0.5f);

        // habilita o texto de "Desmaiou"
        _Text.enabled = true;

        // espera 4s
        yield return new WaitForSecondsRealtime(4f);

        // desabilita o texto de "Desmaiou"
        _Text.enabled = false;


        yield return new WaitForSecondsRealtime(0.2f);

        _FadeHandler.FadeIn(1.5f,Spawn);

    }

}
