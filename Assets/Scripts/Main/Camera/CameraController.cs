using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour{
    [SerializeField] private float damping;
    public Transform target;
    private Vector3 vel = Vector3.zero;
    private void FixedUpdate(){
        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
    }}
