using UnityEngine;

public class PlayerMapHandler : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject);    
    }

}
