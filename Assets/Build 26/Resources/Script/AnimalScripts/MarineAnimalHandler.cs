using UnityEngine;

public class MarineAnimalHandler : MonoBehaviour{
    //Variables
    #region
    [Header("---- Características Básicas ----")]
    public string name;
    public float speed;
    public float size;
    private Vector2 targetPosition;
    #endregion

    //Methdos
    #region
    protected virtual void Start(){
        // Define o primeiro destino aleatório ao iniciar
        targetPosition = GetRandomTargetPosition();
    }

    private void Update(){
        // Move em direção ao destino
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Se o animal chegou perto do destino, define um novo
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f){
            targetPosition = GetRandomTargetPosition();
        }
    }
    private Vector2 GetRandomTargetPosition(){
        //limites da área de movimento
        float minX = -41f, maxX = -23f, minY = -38f, maxY = -29f;
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
    #endregion
}
