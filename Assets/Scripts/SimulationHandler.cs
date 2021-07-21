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

    [Header("Virus")]
    public int VirusID;
    public string Virus_Name;
    public float Virus_Ro;
    public float Virus_DeathRate;
    public float Virus_Duration;

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

    void Awake()
    {
        SIMHANDLER = this;

        WorldHandler = GetComponent<WorldHandler>();
        DataHandler = GetComponent<DataHandler>();
        VirusHandler = GetComponent<VirusHandler>();
    }

    private void Start()
    {
        if (GameObject.Find("SimulationSettings") != null)
            VirusID = GameObject.Find("SimulationSettings").GetComponent<SimulationSettings>().VirusID;
        else
            VirusID = 0;
        Virus_Name = DataHandler.DATASAVE.VirusData.Virus[VirusID].VirusName;
        Virus_DeathRate = DataHandler.DATASAVE.VirusData.Virus[VirusID].DeathRate;
        Virus_Ro = DataHandler.DATASAVE.VirusData.Virus[VirusID].Ro;
        Virus_Duration = DataHandler.DATASAVE.VirusData.Virus[VirusID].InfectionDuration;
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
