using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusHandler : MonoBehaviour
{
    public VirusSO Virus = null;

    [SerializeField] private List<Country> Countries;

    public double TotalPopulation;
    public double TotalPopulationInfected;

    //testing
    private Vector3 _CheckDate;

    private void Start()
    {
        double population = 0;
        for (int i = 0; i < Countries.Count; i++)
        {
            population += Countries[i].Population;
        }
        TotalPopulation = population;
    }

    void Update()
    {
        if (_CheckDate != TimeHandler.TIME.CurrentDate)
        {
            double infected = 0;
            for (int i = 0; i < Countries.Count; i++)
            {
                infected += Countries[i].Population_Infected;
                for (int j = 0; j < Countries[i].Provinces.Count; j++)
                {
                    if (Countries[i].Provinces[j].Population_Infected < Countries[i].Provinces[j].Population)
                    {
                        //For Testing
                        if (Countries[i].Provinces[j].DateInfected.Count > 0)
                        {
                            for (int k = 0; k < Countries[i].Provinces[j].DateInfected.Count; k++)
                            {
                                Countries[i].Provinces[j].Add_Infected((int)Countries[i].Provinces[j].DateInfected[k].InfectedPerDay);
                            }
                        }
                        Countries[i].UpdateInfected();
                    }
                }
            }
            TotalPopulationInfected = infected;
            _CheckDate = TimeHandler.TIME.CurrentDate;
        }
    }
}