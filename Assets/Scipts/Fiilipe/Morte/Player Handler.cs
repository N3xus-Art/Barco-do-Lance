using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private int MaxO2;
    [SerializeField] private float CurrentO2;
    [SerializeField] private FadeHandler _FadeHandler;
    [SerializeField] private MoveTester _MoveTester;
    [SerializeField] private bool Dead;
    [SerializeField] private float Speed;

    private void Awake()
    {
        _MoveTester = gameObject.GetComponent<MoveTester>();
    }

    void Update()
    {
        if (CurrentO2 > 0)
        {

            CurrentO2 -= Speed * Time.deltaTime;

        }
        else
        {

            _FadeHandler.Fade(true);

        }
        
    }

    public void Morrer()
    {
        _MoveTester.enabled = false;
        Dead = true;

    }
}
