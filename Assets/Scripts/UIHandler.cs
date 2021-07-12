using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _TotalInfectedText = null;
    [SerializeField] private TextMeshProUGUI _DateTimeText = null;

    void Update()
    {
        //Population/Infected
        _TotalInfectedText.text = SimulationHandler.SIMHANDLER.VirusHandler.TotalPopulation.ToString("n0") + " / " + SimulationHandler.SIMHANDLER.VirusHandler.TotalPopulationInfected.ToString("n0");

        //Display time/date
        _DateTimeText.text = "Current Time: " + Mathf.Floor(TimeHandler.TIME.CurrentTime.x) + ":" + Mathf.Floor(TimeHandler.TIME.CurrentTime.y) + ":" + Mathf.Floor(TimeHandler.TIME.CurrentTime.z) +
            " / " + TimeHandler.TIME.CurrentDate.x + "- " + TimeHandler.TIME.CurrentDate.y + "- " + TimeHandler.TIME.CurrentDate.z;
    }
}
