 using UnityEngine;
 using UnityEngine.InputSystem;

public class Andar : MonoBehaviour
{

  [SerializeField] float velocity;

    // Update is called once per frame
    void Update()
    {
        
       transform.Translate(Vector2.right * Time.deltaTime * velocity); 

    }
}
