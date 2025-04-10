using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Vector2 pos = new Vector2(3, 1);
        

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.right * Time.deltaTime * 3);

    }
}
