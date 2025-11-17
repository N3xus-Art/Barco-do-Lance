using UnityEngine;

public class MarineAnimalHandler : MonoBehaviour
{
    public string name;
    public float speed;
    public float smoothness = 0.5f;

    protected Vector2 targetPosition;
    protected Vector2 velocity = Vector2.zero;
    protected bool canMove = true;
    protected bool canInteract = false;

    // Variável para rastrear se o animal está indo para a comida
    protected bool isAttractedToFood = false;

    protected virtual void Start()
    {
        targetPosition = GetRandomTargetPosition();
    }

    protected virtual void Update()
    {
        if (canMove)
        {
            // Move o peixe em direção ao destino alvo
            transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothness, speed);
            if (Vector2.Distance(transform.position, targetPosition) < 0.5f)
            {
                // Verifica se o peixe estava indo para a comida, se sim, reseta o estado
                if (isAttractedToFood)
                {
                    isAttractedToFood = false;
                    Debug.Log(name + " chegou na ração, voltando a nadar.");
                }

                // Pega um novo destino aleatório
                targetPosition = GetRandomTargetPosition();
            }
        }
    }

    // Método público para pausar ou retomar o movimento do peixe
    public void SetCanMove(bool mover)
    {
        canMove = mover;
        if (!mover)
        {
            velocity = Vector2.zero;

            // Se o peixe for parado manualmente, ele para de ir para a comida
            isAttractedToFood = false;

        }
    }

    // Método público para atrair o peixe para a comida
    public void GoToFood(Vector3 foodPosition)
    {
        // O peixe só vai para a comida se estiver nadando normalmente
        // (e não estiver já indo para outra comida)
        if (canMove)
        {
            Debug.Log(name + " sentiu o cheiro da ração!");
            isAttractedToFood = true;
            targetPosition = foodPosition;
        }
    }

    // Obtém uma posição aleatória dentro dos limites do pai, ou em uma área padrão
    private Vector2 GetRandomTargetPosition()
    {
        // Tenta obter os limites do objeto pai, se existir
        if (transform.parent != null)
        {
            var parentTransform = transform.parent;
            var parentRenderer = parentTransform.GetComponent<Renderer>();

            if (parentRenderer != null)
            {
                Bounds bounds = parentRenderer.bounds;
                float parentMinX = bounds.min.x;
                float parentMaxX = bounds.max.x;
                float parentMinY = bounds.min.y;
                float parentMaxY = bounds.max.y;

                return new Vector2(Random.Range(parentMinX, parentMaxX), Random.Range(parentMinY, parentMaxY));
            }
        }
        // Se não houver pai ou Renderer, usa uma área padrão
        float minX = -10f, maxX = 10f, minY = -5f, maxY = 5f;
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
}
