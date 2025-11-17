using UnityEngine;
using System.Collections;

public class LionfishHandler : MarineAnimalHandler, ICapturable {
    protected override void Start()
    {
        base.Start();
        name = "Peixe-Leao";
        speed = 1.0f;
        SetCanInteract(true);   
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
        Debug.Log("Peixe-leao iniciando captura. Parando movimento.");
        // Usa o m�todo p�blico da classe base para pausar/retomar o movimento
        SetCanMove(false);
        CaptureUI.Instance.OpenCapture(this);
    }

    public void ReturnBehaviour()
    {
        Debug.Log("Peixe-leao retomando comportamento normal.");
        // Usa o m�todo p�blico da classe base para voltar a nadar.
        SetCanMove(true);
    }

    public void Run(Vector3 playerPos)
    {
        StartCoroutine(EscapeRoutine(playerPos));
    }

    private IEnumerator EscapeRoutine(Vector3 playerPos)
    {
        Debug.Log("Peixe-leao esta fugindo!");
        SetCanMove(false); // Garante que o movimento normal n�o interfira

        Vector2 escapeDirection = ((Vector2)transform.position - (Vector2)playerPos).normalized;
        Vector2 escapeDestination = (Vector2)transform.position + escapeDirection * 10f;
        float escapeSpeed = speed * 2f;
        float escapeDuration = 2.0f;
        float elapsedTime = 0f;

        while (elapsedTime < escapeDuration)
        {
            transform.position = Vector2.MoveTowards(transform.position, escapeDestination, escapeSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Peixe-leao terminou de fugir.");
        ReturnBehaviour(); // Volta a nadar normalmente
    }

    public void Interact(){
        if (canInteract) { StartCapture(); }
    }

    public void SetCanInteract(bool Can)
    {

        canInteract = Can;
    }
}
