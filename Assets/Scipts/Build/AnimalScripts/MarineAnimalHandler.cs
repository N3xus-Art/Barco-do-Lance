using UnityEngine;

public class MarineAnimalHandler : MonoBehaviour {
    public string name;
    public float speed;
    public float smoothness = 0.5f;

    protected Vector2 targetPosition;
    protected Vector2 velocity = Vector2.zero;
    protected bool canMove = true;

    protected virtual void Start()
    {
        targetPosition = GetRandomTargetPosition();
    }

    protected virtual void Update()
    {
        if (canMove)
        {
            transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothness, speed);

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetPosition = GetRandomTargetPosition();
            }
        }
    }

    // Método para ser usado externamente para pausar/retomar o movimento
    public void SetCanMove(bool mover)
    {
        canMove = mover;
        if (!mover)
        {
            velocity = Vector2.zero; // Para o deslize
        }
    }

    private Vector2 GetRandomTargetPosition()
    {
        // ... (código do método sem alterações)
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

        float minX = -10f, maxX = 10f, minY = -5f, maxY = 5f;
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
}
