using Unity.VisualScripting;
using UnityEngine;

public class topCollisionHandler : MonoBehaviour{
    //Variables
    #region
    [Header("----Variaveis de Escada----")]
    [SerializeField] public Rigidbody2D rbLocal;
    [SerializeField] public LayerMask ladderLayer;
    [SerializeField] public ContactFilter2D contactFilter2D;
    [SerializeField] public bool inLadderLocal => rbLocal.IsTouching(contactFilter2D);
    #endregion

    //Methods
    #region
    private void Start(){
        contactFilter2D.useLayerMask = true;
        contactFilter2D.useTriggers = true;
        contactFilter2D.layerMask = ladderLayer;
    }
    #endregion
}
