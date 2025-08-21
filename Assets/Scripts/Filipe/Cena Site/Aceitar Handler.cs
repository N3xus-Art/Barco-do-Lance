using System.Data;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class AceitarHandler : MonoBehaviour
{
    [SerializeField]
    bool IsOnMission = false;

    [SerializeField]
    bool IsEndMission = false;
    
    public TMP_Text Texto;
    public SiteHandler Site;

    public Color AcceptColor;
    public Color EndColor;

    public void AcceptMission()
    {
        if (IsOnMission == false) 
        {

            GameObject CurrentMission = new GameObject();
            CurrentMission.name = "CurrentMission";
            CurrentMission.AddComponent<CurrentMission>();
            CurrentMission.GetComponent<CurrentMission>().missionReward = Site.MissoesDisponiveis[Site.ID_Atual].missionReward;
            CurrentMission.GetComponent<CurrentMission>().RescueableAnimals = Site.MissoesDisponiveis[Site.ID_Atual].RescueableAnimals;
            CurrentMission.GetComponent<CurrentMission>().OperableAnimals = Site.MissoesDisponiveis[Site.ID_Atual].OperableAnimals;
          

            IsOnMission = true;
        }
    }

    public void EndMission()
    {

        if (IsEndMission)
        {

            Site.DinDin += GameObject.Find("CurrentMission").GetComponent<CurrentMission>().missionReward;
            Destroy(GameObject.Find("CurrentMission"));
            Site.ProximasMissoes.Add(Site.MissoesDisponiveis[Site.ID_Atual]);
            Site.MissoesDisponiveis[Site.ID_Atual] = Site.ProximasMissoes[0];
            Site.ProximasMissoes.RemoveAt(0);
            IsOnMission = false;
            IsEndMission = false;
        }
    }

    private void Update()
    {
        if (IsOnMission == true)
        {


            gameObject.GetComponent<Image>().color = EndColor;
            Texto.SetText("Cancelar");


        }
        else {

            gameObject.GetComponent<Image>().color = AcceptColor;
            Texto.SetText("Aceitar");

        }

        if (IsEndMission == false)
        {
            if (GameObject.Find("CurrentMission") != null)
            {

                if (GameObject.Find("CurrentMission").GetComponent<CurrentMission>().End)
                {

                    IsEndMission = true;

                }

            }
        }
        else
        {

            Texto.SetText("Terminar Missão");
            GetComponent<Image>().color = AcceptColor;

        }
    }
}
