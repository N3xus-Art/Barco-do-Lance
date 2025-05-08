using UnityEngine;

public class OceanHandler : MonoBehaviour
{
    Color oceanColor;
    SpriteRenderer oceanSprite;
    [SerializeField] float colorR, colorG, colorB, colorA;

    void Start()
    {   
        oceanSprite = GetComponent<SpriteRenderer>();
        oceanSprite.color = new Color(colorR,colorG,colorB,colorA);
    }




}
