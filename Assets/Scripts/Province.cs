using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Province : MonoBehaviour
{
    public ProvincesSO ProvinceProfile;
    public float Population;

    public int Population_Normal;
    public int Population_Infected;
    public int Population_Healthy;
    public int Population_Dead;

    public int StartInfected;

    private Material _Mat;

    public List<DATA_TIMEAMOUNT> DATAInfections = new List<DATA_TIMEAMOUNT>();
    public List<DATA_TIMEAMOUNT> DATANormal = new List<DATA_TIMEAMOUNT>();
    public List<DATA_TIMEAMOUNT> DATADead = new List<DATA_TIMEAMOUNT>();
    public List<DATA_TIMEAMOUNT> DATAHealthy = new List<DATA_TIMEAMOUNT>();
    public List<DateInfected> DateInfected = new List<DateInfected>();

    private Vector3 _CheckDate = Vector3.zero;
    private int _InfectedAdded;
    private float _DeathRate;

    private bool _NoNormalLeft;

    private void Start()
    {
        Population = ProvinceProfile.Population;
        Population_Normal = (int)Population;
        _Mat = GetComponent<MeshRenderer>().material;
        _DeathRate = SimulationHandler.SIMHANDLER.Virus_DeathRate * 0.01f;

        Add_Infected(StartInfected);
    }

    private void Update()
    {
        if (_CheckDate != TimeHandler.TIME.CurrentDate)
        {
            if (_InfectedAdded != 0)
                AddDate(_InfectedAdded);
            CalculatePopulationStatus();
            _CheckDate = TimeHandler.TIME.CurrentDate;
            _InfectedAdded = 0;
        }
    }

    private void CalculatePopulationStatus()
    {
        //Calculate 
        Population_Infected = 0;
        for (int i = 0; i < DateInfected.Count; i++)
        {
            if (DateInfected[i].Date.y <= TimeHandler.TIME.CurrentDate.y)
            {
                if (DateInfected[i].Date.x <= TimeHandler.TIME.CurrentDate.x)
                {
                    double calcdeath = DateInfected[i].Amount * _DeathRate;
                    Population_Dead += Mathf.RoundToInt((float)calcdeath);
                    double calchealthy = DateInfected[i].Amount - calcdeath;
                    Population_Healthy += Mathf.RoundToInt((float)calchealthy);
                    DateInfected.RemoveAt(i);
                    CalculatePopulationStatus();
                    return;
                }
            }

            Population_Infected += (int)DateInfected[i].Amount;
        }
        Population_Normal = (int)Population - Population_Infected - Population_Dead - Population_Healthy;

        //Notification test
        if(Population_Normal == 0 && !_NoNormalLeft)
        {
            NotificationHandler.NOTIF.SetNotification("Privince: " + ProvinceProfile.ProvinceName + " has no uneffected people left",
                "Every person has been in contact with " + SimulationHandler.SIMHANDLER.Virus_Name + ",\n \n" +
                "Current situation: \n" +
                "Infected: " + Population_Infected.ToString() + "\n" +
                "Dead: " + Population_Dead.ToString() + "\n" +
                "Healthy: " + Population_Healthy.ToString());
            _NoNormalLeft = true;
        }

        //Set Color
        _Mat.color = new Vector4(1, 1 - (Population_Infected + Population_Dead) / Population, 1 - Population_Infected / Population, 1);
    }

    public void Add_Infected(int amount)
    {
        if (Population_Normal != 0)
        {
            if (Population_Normal > amount)
            {
                Population_Normal -= amount;
                Population_Infected += amount;
                _InfectedAdded += amount;
            }
            else
            {
                Population_Infected += Population_Normal;
                _InfectedAdded += Population_Normal;
                Population_Normal = 0;
            }
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

    void AddDate(int amount)
    {
        DateInfected newdateinfected = new DateInfected();
        newdateinfected.Date = TimeHandler.TIME.Get_DateAfterDays(Mathf.RoundToInt(SimulationHandler.SIMHANDLER.Virus_Duration));
        newdateinfected.Amount = amount;
        newdateinfected.InfectedPerDay = ((float)amount / SimulationHandler.SIMHANDLER.Virus_Duration) * SimulationHandler.SIMHANDLER.Virus_Ro;
        DateInfected.Add(newdateinfected);
    }
}

public class DateInfected
{
    public Vector3 Date; //Day/Month/Year
    public double Amount; //Total people
    public float InfectedPerDay;
}