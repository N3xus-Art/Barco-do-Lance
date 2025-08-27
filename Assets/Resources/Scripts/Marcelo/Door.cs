using JetBrains.Annotations;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Player target;

    private void OnTriggerEnter2D(Collider2D other)
    {

        Player player = other.GetComponent<Player>();

        if (player.temTanque && player != null)
        {
            GetComponent<Renderer>().enabled = false;
            GameObject DoorOut = GameObject.Find("DoorOut");
            DoorOut?.SetActive(false);
        }   

    }
}