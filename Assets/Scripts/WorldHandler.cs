using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldHandler : MonoBehaviour
{
    public List<CountrySO> CountriesProfile = new List<CountrySO>();
    public List<Country> Countries = new List<Country>();

    void Start()
    {
        for (int i = 0; i < CountriesProfile.Count; i++)
        {
            int population = 0;
            for (int j = 0; j < CountriesProfile[i].Provinces.Count; j++)
            {
                population += CountriesProfile[i].Provinces[j].Population;
            }
            CountriesProfile[i].Population = population;
        }
    }
}