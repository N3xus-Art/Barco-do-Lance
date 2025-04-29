using UnityEngine;

public class TargetMovement : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float velocity;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, velocity * Time.deltaTime);
    }
}
