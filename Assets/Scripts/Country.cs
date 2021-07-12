using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour
{
    public CountrySO CountryProfile;
    public double Infected;
    public double Population;

    public List<Province> _Provinces;
    private Material _Mat;

    private void Awake()
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

        float colorcalc = (float)Infected / (float)Population;
        _Mat.color = new Vector4(1, 1 - colorcalc, 1 - colorcalc, 1);
    }
}
