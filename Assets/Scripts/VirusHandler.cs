using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusHandler : MonoBehaviour
{
    [SerializeField] private VirusSO _Virus = null;

    [SerializeField] private List<Country> Countries;

    public double TotalPopulation;
    public double TotalPopulationInfected;

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
        double infected = 0;
        for (int i = 0; i < Countries.Count; i++)
        {
            infected += Countries[i].Infected;
            for (int j = 0; j < Countries[i]._Provinces.Count; j++)
            {
                if(Countries[i]._Provinces[j].Infected < Countries[i]._Provinces[j].Population)
                {
                    //For Testing
                    Countries[i]._Provinces[j].Add_Infected(10);
                    Countries[i].UpdateInfected();
                }
            }
        }
        TotalPopulationInfected = infected;
    }
}
