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
        // Verifica se o objeto tem um pai  
        if (transform.parent != null)
        {
            // Obtém o tamanho do objeto pai  
            var parentTransform = transform.parent;
            var parentRenderer = parentTransform.GetComponent<Renderer>();

            if (parentRenderer != null)
            {
                // Calcula os limites com base no tamanho do Renderer do pai  
                Bounds bounds = parentRenderer.bounds;
                float parentMinX = bounds.min.x;
                float parentMaxX = bounds.max.x;
                float parentMinY = bounds.min.y;
                float parentMaxY = bounds.max.y;

                return new Vector2(Random.Range(parentMinX, parentMaxX), Random.Range(parentMinY, parentMaxY));
            }
        }

        // Caso não tenha um pai ou o pai não tenha um Renderer, retorna um valor padrão  
        float minX = -10f, maxX = 10f, minY = -5f, maxY = 5f;
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
}
