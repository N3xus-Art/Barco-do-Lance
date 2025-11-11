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
                _FadeHandler.FadeOut();

            }
        }


        if (Dead)
        {

            Recover();

        }
        
    }

    public void Morrer()
    {

        StartCoroutine(Die());

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


    private IEnumerator Die()
    {

        // desabilita a o movimento
        _MoveTester.enabled = false;

        // move o player pro inicio
        _Transform.position = Vector3.zero;

        Dead = true;

        yield return new WaitForSecondsRealtime(0.5f);

        _Text.enabled = true;

        yield return new WaitForSecondsRealtime(4f);

        _Text.enabled = false;

        yield return new WaitForSecondsRealtime(0.5f);

        _FadeHandler.FadeIn();

    }

}
