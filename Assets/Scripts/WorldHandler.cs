using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldHandler : MonoBehaviour
{
    public List<CountrySO> Countries = new List<CountrySO>();

    void Start()
    {
        for (int i = 0; i < Countries.Count; i++)
        {
            int population = 0;
            for (int j = 0; j < Countries[i].Provinces.Count; j++)
            {
                population += Countries[i].Provinces[j].Population;
            }
            Countries[i].Population = population;
        }
    }
}