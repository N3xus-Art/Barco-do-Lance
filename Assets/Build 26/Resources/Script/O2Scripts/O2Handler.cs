using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class O2Handler : MonoBehaviour{
    [SerializeField] float rotation;
    [SerializeField] private float o2 = 100;
    [SerializeField] private int maxO2 = 100;

    private void Update(){
        if (o2 > 0){
            o2 -= 1 * Time.deltaTime;
        }
        rotation = -160f + (o2 / maxO2) * (160f - (-160f));
        // tank full oxigen 160, zero oxigen -160
        gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, -rotation);
    }
}
