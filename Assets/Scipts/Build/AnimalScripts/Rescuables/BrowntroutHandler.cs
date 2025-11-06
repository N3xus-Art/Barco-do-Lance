using System.Xml.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System.Collections;

public class BrowntroutHandler : MarineAnimalHandler, ICapturable {
    protected override void Start()
    {
        base.Start();
        name = "Truta";
        speed = 1.0f;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public Sprite GetSprite()
    {
        return gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void StartCapture()
    {
        Debug.Log("Medusa iniciando captura. Parando movimento.");
        // Usa o método público da classe base para pausar/retomar o movimento
        SetCanMove(false);
        CaptureUI.Instance.OpenCapture(this);
    }

    public void ReturnBehaviour()
    {
        Debug.Log("Medusa retomando comportamento normal.");
        // Usa o método público da classe base para voltar a nadar.
        SetCanMove(true);
    }

    public void Run(Vector3 playerPos)
    {
        StartCoroutine(EscapeRoutine(playerPos));
    }

    private IEnumerator EscapeRoutine(Vector3 playerPos)
    {
        Debug.Log("Medusa está fugindo!");
        SetCanMove(false); // Garante que o movimento normal não interfira

        Vector2 escapeDirection = ((Vector2)transform.position - (Vector2)playerPos).normalized;
        Vector2 escapeDestination = (Vector2)transform.position + escapeDirection * 10f;
        float escapeSpeed = speed * 20f;
        float escapeDuration = 60.0f;
        float elapsedTime = 0f;

        while (elapsedTime < escapeDuration)
        {
            transform.position = Vector2.MoveTowards(transform.position, escapeDestination, escapeSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Medusa terminou de fugir.");
        ReturnBehaviour(); // Volta a nadar normalmente
    }

    public void Interact()
    {
        StartCapture();
    }
}
