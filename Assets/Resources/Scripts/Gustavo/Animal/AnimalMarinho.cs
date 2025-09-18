using UnityEngine;

public abstract class AnimalMarinho : MonoBehaviour
{
    [Header("Características Básicas")]
    protected string nome;
    public float velocidade;
    public float tamanho;
    private Vector2 targetPosition;

    protected virtual void Start()
    {
        // Define o primeiro destino aleatório ao iniciar
        targetPosition = GetRandomTargetPosition();
    }

    void Update()
    {
        // Move em direção ao destino
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, velocidade * Time.deltaTime);

        // Se o animal chegou perto do destino, define um novo
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = GetRandomTargetPosition();
        }
    }

    private Vector2 GetRandomTargetPosition()
    {
        //limites da área de movimento
        float minX = -10f, maxX = 10f, minY = -5f, maxY = 5f;
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
}
