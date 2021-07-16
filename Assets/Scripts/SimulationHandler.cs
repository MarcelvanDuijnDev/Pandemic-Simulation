using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimulationHandler : MonoBehaviour
{
    [Header("Settings Optimization")]
    public float Data_Coutry_UpdateTime = 30;
    public float Data_Province_UpdateTime = 60;
    private float _TimerCountry = 0;
    private float _TimerProvince = 0;

    [Header("Click/WorldSpace Info")]
    public Transform CameraObj;
    public Transform ClickInfo;
    public TextMeshProUGUI ClickInfoText;

    //Ref
    public static SimulationHandler SIMHANDLER;
    public List<UILineRenderer> Graph = new List<UILineRenderer>();
    [HideInInspector] public WorldHandler WorldHandler;
    [HideInInspector] public DataHandler DataHandler;
    [HideInInspector] public VirusHandler VirusHandler;

    void Start()
    {
        SIMHANDLER = this;

        WorldHandler = GetComponent<WorldHandler>();
        DataHandler = GetComponent<DataHandler>();
        VirusHandler = GetComponent<VirusHandler>();
    }

    private void Update()
    {
        float scale = CameraObj.position.y * 0.1f;
        ClickInfo.localScale = new Vector3(scale, scale, scale);

        UpdateData();
    }

    private void UpdateData()
    {
        //Country
        _TimerCountry += 1 * Time.deltaTime;
        if(_TimerCountry >= Data_Coutry_UpdateTime)
        {
            for (int i = 0; i < WorldHandler.Countries.Count; i++)
            {
                WorldHandler.Countries[i].AddDataInfections();
            }
            _TimerCountry = 0;
        }

        _TimerProvince += 1 * Time.deltaTime;
        if (_TimerProvince >= Data_Province_UpdateTime)
        {
            for (int i = 0; i < WorldHandler.Countries.Count; i++)
            {
                for (int j = 0; j < WorldHandler.Countries[i].Provinces.Count; j++)
                {
                    WorldHandler.Countries[i].Provinces[j].AddDataInfections();
                }
            }
            _TimerProvince = 0;
        }
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
