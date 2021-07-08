using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimulationHandler : MonoBehaviour
{
    public static SimulationHandler SIMHANDLER;

    [HideInInspector] public WorldHandler WorldHandler;
    [HideInInspector] public DataHandler DataHandler;
    [HideInInspector] public TimeHandler TimeHandler;
    [HideInInspector] public VirusHandler VirusHandler;



    [Header("Click/WorldSpace Info")]
    public Transform ClickInfo;
    public TextMeshProUGUI ClickInfoText; 

    void Start()
    {
        SIMHANDLER = this;

        WorldHandler = GetComponent<WorldHandler>();
        DataHandler = GetComponent<DataHandler>();
        TimeHandler = GetComponent<TimeHandler>();
        VirusHandler = GetComponent<VirusHandler>();
    }

    public void Set_ClickInfoLocation(Transform obj)
    {
        ClickInfo.transform.position = obj.position;
    }
    public void Set_ClickInfoText(string info)
    {
        ClickInfoText.text = info;
    }
}
