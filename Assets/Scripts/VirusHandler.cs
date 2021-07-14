using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusHandler : MonoBehaviour
{
    [SerializeField] private VirusSO _Virus = null;

    [SerializeField] private List<Country> Countries;

    public double TotalPopulation;
    public double TotalPopulationInfected;

    //testing
    float _Timer = 0;
    public float _UpdateTime = .1f;

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
        _Timer += 1 * Time.deltaTime;
        if (_Timer >= _UpdateTime)
        {

            double infected = 0;
            for (int i = 0; i < Countries.Count; i++)
            {
                infected += Countries[i].Infected;
                for (int j = 0; j < Countries[i].Provinces.Count; j++)
                {
                    if (Countries[i].Provinces[j].Infected < Countries[i].Provinces[j].Population)
                    {
                        //For Testing
                        Countries[i].Provinces[j].Add_Infected(Random.Range(0, 1000));
                        Countries[i].UpdateInfected();
                    }
                }
            }
            TotalPopulationInfected = infected;
            _Timer = 0;
        }
    }
}
