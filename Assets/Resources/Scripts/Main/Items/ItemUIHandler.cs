using UnityEngine;

public class ItemUIHandler : MonoBehaviour
{
    [SerializeField] NetController net;
    [SerializeField] CuttingControler cut;
    [SerializeField] TreatController treat;
    [SerializeField] GameObject netGO, cutGO, treatGO;
    [SerializeField] public float cur = 1;
    public void Update() {
        if (cur == 1){
            netGO.SetActive(true);
            cutGO.SetActive(false);
            treatGO.SetActive(false);
        }
        else if (cur == 2) {
            netGO.SetActive(false);
            cutGO.SetActive(true);
            treatGO.SetActive(false);
        }
        else if(cur == 3){
            netGO.SetActive(false);
            cutGO.SetActive(false);
            treatGO.SetActive(true);
        }else{
            netGO.SetActive(false);
            cutGO.SetActive(false);
            treatGO.SetActive(false);
        }
    }
}
