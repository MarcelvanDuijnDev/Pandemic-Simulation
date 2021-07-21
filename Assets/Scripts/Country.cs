using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour
{
    public CountrySO CountryProfile;
    public double Population;
    public double Population_Normal;
    public double Population_Infected;
    public double Population_Healthy;
    public double Population_Dead;

    public List<Province> Provinces;

    public List<DATA_TIMEAMOUNT> DATAInfections = new List<DATA_TIMEAMOUNT>();
    public List<DATA_TIMEAMOUNT> DATANormal = new List<DATA_TIMEAMOUNT>();
    public List<DATA_TIMEAMOUNT> DATADead = new List<DATA_TIMEAMOUNT>();
    public List<DATA_TIMEAMOUNT> DATAHealthy = new List<DATA_TIMEAMOUNT>();

    private Material _Mat;

    //Notifications
    private bool _Notif_Effected;

    private void Awake()
    {
        Population = CountryProfile.Population;
        _Mat = GetComponent<MeshRenderer>().material;
    }

    public void UpdateInfected()
    {
        Population_Infected = 0;
        Population_Normal = 0;
        Population_Healthy = 0;
        Population_Dead = 0;

        for (int i = 0; i < Provinces.Count; i++)
        {
            Population_Infected += Provinces[i].Population_Infected;
            Population_Normal += Provinces[i].Population_Normal;
            Population_Healthy += Provinces[i].Population_Healthy;
            Population_Dead += Provinces[i].Population_Dead;
        }

        float colorcalc = (float)Population_Infected / (float)Population;
        _Mat.color = new Vector4(1, 1 - colorcalc, 1 - colorcalc, 1);

        //Notificatios
        if(!_Notif_Effected && Population_Normal == 0)
        {
            NotificationHandler.NOTIF.SetNotification(CountryProfile.CountryName + ":" + " has no uneffected people left",
                "Every person has been in contact with " + SimulationHandler.SIMHANDLER.Virus_Name + ",\n \n" +
                "Current situation: \n" +
                "Infected: " + Population_Infected.ToString() + "\n" +
                "Dead: " + Population_Dead.ToString() + "\n" +
                "Healthy: " + Population_Healthy.ToString());
            _Notif_Effected = true;
        }
    }

    public void AddDataInfections()
    {
        DATA_TIMEAMOUNT newdata = new DATA_TIMEAMOUNT();
        newdata.Time = TimeHandler.TIME.CurrentTime;
        newdata.Date = TimeHandler.TIME.CurrentDate;
        newdata.Amount = Population_Infected;
        DATAInfections.Add(newdata);

        DATA_TIMEAMOUNT newdata2 = new DATA_TIMEAMOUNT();
        newdata2.Time = TimeHandler.TIME.CurrentTime;
        newdata2.Date = TimeHandler.TIME.CurrentDate;
        newdata2.Amount = Population_Normal;
        DATANormal.Add(newdata2);

        DATA_TIMEAMOUNT newdata3 = new DATA_TIMEAMOUNT();
        newdata3.Time = TimeHandler.TIME.CurrentTime;
        newdata3.Date = TimeHandler.TIME.CurrentDate;
        newdata3.Amount = Population_Dead;
        DATADead.Add(newdata3);

        DATA_TIMEAMOUNT newdata4 = new DATA_TIMEAMOUNT();
        newdata4.Time = TimeHandler.TIME.CurrentTime;
        newdata4.Date = TimeHandler.TIME.CurrentDate;
        newdata4.Amount = Population_Healthy;
        DATAHealthy.Add(newdata4);
    }
}
