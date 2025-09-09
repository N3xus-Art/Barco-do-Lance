using UnityEngine;

public enum TipoFerramenta { Faca, Alicate }

[System.Serializable]
public class Ferramenta
{
    public TipoFerramenta tipo;
    public int durabilidade = 10;

    public bool Usar()
    {
        if (durabilidade > 0)
        {
            durabilidade--;
            return true;
        }
        Debug.Log("Ferramenta sem durabilidade!");
        return false;
    }
}
