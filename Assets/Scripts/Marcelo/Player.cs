using UnityEngine;

public class Player : MonoBehaviour
{

    // Verifica se o player tem um tanque de O2 equipado
    public bool temTanque;
    public float moveSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        movement.Normalize();
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
