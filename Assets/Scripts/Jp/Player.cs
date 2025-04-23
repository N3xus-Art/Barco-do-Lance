using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float vida;
    void Update()
    {
        vida--;
        if(0 > vida){
            Destroy(gameObject);
        }
    }

}
