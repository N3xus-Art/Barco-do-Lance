using UnityEngine;
using UnityEngine.Rendering;
public class CameraHandler : MonoBehaviour{
    //Variables
    #region
    [SerializeField] private float damping;
    public Transform target;
    private Vector3 velocity = Vector3.zero;
    #endregion
    //Methods
    #region
    void Start(){
        PlayerTag playerTag = FindObjectOfType<PlayerTag>(true);
        if (playerTag != null)
        {
            target = playerTag.gameObject.GetComponent<Transform>();
            Debug.Log("Player encontrado e Atribuido");
        }
        else
        {
            Debug.LogWarning("CameraHandler: NÃO FOI POSSÍVEL ENCONTRAR o objeto com a tag 'playerTag' na cena!");
            target = null;
        }
    }
    private void FixedUpdate(){
        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping);
    }
    #endregion
}
