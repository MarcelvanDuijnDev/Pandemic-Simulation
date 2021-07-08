using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour
{
    public CountrySO CountryProfile;
    public float Infected;
    public float Population;

    public List<Province> _Provinces;

    private void Start()
    {
        Population = CountryProfile.Population;
    }

    public void UpdateInfected()
    {
        float infected = 0;
        for (int i = 0; i < _Provinces.Count; i++)
        {
            infected += _Provinces[i].Infected;
        }
        Infected = infected;
    }
}
