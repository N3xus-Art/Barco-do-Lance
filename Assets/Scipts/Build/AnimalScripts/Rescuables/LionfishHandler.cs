using UnityEngine;

public class LionfishHandler : MarineAnimalHandler, ICapturable {
    protected override void Start() {
        base.Start();
        name = "Peixe-leão";
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
        Debug.Log($"Clicou na peixe-leão {name}");
        CaptureUI.Instance.OpenCapture(this);
    }
    public void startCapture() {
        Debug.Log($"Captura iniciada no animal {name}");
    }
}
