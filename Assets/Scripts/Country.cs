using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour
{
    public CountrySO CountryProfile;
    public float Infected;
    public float Population;

    public List<Province> _Provinces;
    private Material _Mat;

    private void Start()
    {
        Population = CountryProfile.Population;
        _Mat = GetComponent<MeshRenderer>().material;
    }

    public void UpdateInfected()
    {
        float infected = 0;
        for (int i = 0; i < _Provinces.Count; i++)
        {
            infected += _Provinces[i].Infected;
        }
        Infected = infected;
        _Mat.color = new Vector4(1, 1 - Infected / Population, 1 - Infected / Population, 1);
    }
}
