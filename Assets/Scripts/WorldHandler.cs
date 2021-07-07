using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldHandler : MonoBehaviour
{
    public List<Country> Countries = new List<Country>();

    void Start()
    {
        for (int i = 0; i < Countries.Count; i++)
        {
            int population = 0;
            for (int j = 0; j < Countries[i].Provinces.Count; j++)
            {
                population += Countries[i].Provinces[j].ProvinceInfo.Population;
            }
            Debug.Log(Countries[i].CountryName + " Population: " + population.ToString());
        }
    }

    void Update()
    {
        
    }
}

[System.Serializable]
public class Country
{
    public string CountryName;
    public List<Province> Provinces = new List<Province>();
}

[System.Serializable]
public class Province
{
    public ProvincesSO ProvinceInfo;
}

[System.Serializable]
public class Place
{

}