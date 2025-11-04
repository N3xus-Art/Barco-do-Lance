using UnityEngine;

public class MarineAnimalHandler : MonoBehaviour {
    //Variables
    #region
    [Header("Características Básicas")]
    protected string nane;
    public float speed;
    private Vector2 targetPosition;
    private Vector2 velocity = Vector2.zero; // Armazena a velocidade atual para SmoothDamp
    public float smoothness = 0.5f; // Quanto menor, mais rápido responde ao destino

    protected virtual void Start() {
        targetPosition = GetRandomTargetPosition();
    }

    void Update() {
        // Movimento suave em direção ao destino
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothness, speed);

        // Se o animal chegou perto do destino, define um novo
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f) {
            targetPosition = GetRandomTargetPosition();
        }
    }

    private Vector2 GetRandomTargetPosition() {
        if (transform.parent != null) {
            var parentTransform = transform.parent;
            var parentRenderer = parentTransform.GetComponent<Renderer>();

            if (parentRenderer != null) {
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
    #endregion
}
