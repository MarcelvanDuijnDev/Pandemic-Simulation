using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _TotalInfectedText = null;
    [SerializeField] private TextMeshProUGUI _DateTimeText = null;
    [SerializeField] private TextMeshProUGUI _Virus_Name = null;
    [SerializeField] private TextMeshProUGUI _Virus_DeathRate = null;
    [SerializeField] private TextMeshProUGUI _Virus_Ro = null;
    [SerializeField] private TextMeshProUGUI _Virus_Duration = null;

    private void Start()
    {
        _Virus_Name.text = "Virus: " + SimulationHandler.SIMHANDLER.Virus_Name;
        _Virus_Ro.text = "R0: " + SimulationHandler.SIMHANDLER.Virus_Ro;
        _Virus_DeathRate.text = "Death Rate: " + SimulationHandler.SIMHANDLER.Virus_DeathRate;
        _Virus_Duration.text = "Duration: " + SimulationHandler.SIMHANDLER.Virus_Duration;
    }

    void Update()
    {
        //Population/Infected
        _TotalInfectedText.text = SimulationHandler.SIMHANDLER.VirusHandler.TotalPopulation.ToString("n0") + " / " + SimulationHandler.SIMHANDLER.VirusHandler.TotalPopulationInfected.ToString("n0");

        //Display time/date
        _DateTimeText.text = "Current Time: " + Mathf.Floor(TimeHandler.TIME.CurrentTime.x) + ":" + Mathf.Floor(TimeHandler.TIME.CurrentTime.y) + ":" + Mathf.Floor(TimeHandler.TIME.CurrentTime.z) +
            " / " + TimeHandler.TIME.CurrentDate.x + "- " + TimeHandler.TIME.CurrentDate.y + "- " + TimeHandler.TIME.CurrentDate.z;
    }
}
