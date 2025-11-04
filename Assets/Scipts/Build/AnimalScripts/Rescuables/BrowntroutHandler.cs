using System.Xml.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BrowntroutHandler : MarineAnimalHandler, ICapturable {
    protected override void Start() {
        base.Start();
        name = "Truta";
        speed = 1.0f;
    }
    public GameObject GetGameObject() {
        return this.gameObject;
    }
    public Sprite GetSprite() {
        return gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    public void OnMouseDown() {
        // Abre a UI de captura
        Debug.Log($"Clicou na truta {name}");
        CaptureUI.Instance.OpenCapture(this);
    }
    public void startCapture() {
        Debug.Log($"Captura iniciada no animal {name}");
    }
}
