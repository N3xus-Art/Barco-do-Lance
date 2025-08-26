using UnityEngine;
using UnityEngine.Rendering;
public class CameraHandler : MonoBehaviour{
    //Variables
    #region
    [SerializeField] private float damping;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    #endregion
    //Methods
    #region
    private void FixedUpdate(){
        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping);
    }
    #endregion
}
