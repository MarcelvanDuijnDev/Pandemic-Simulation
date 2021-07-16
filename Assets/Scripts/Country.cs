using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour
{
    public CountrySO CountryProfile;
    public double Infected;
    public double Population;

    public List<Province> Provinces;

    public List<DATA_TIMEAMOUNT> Infections = new List<DATA_TIMEAMOUNT>();

    private Material _Mat;

    private void Awake()
    {
        Population = CountryProfile.Population;
        _Mat = GetComponent<MeshRenderer>().material;
    }

    public void UpdateInfected()
    {
        float infected = 0;
        for (int i = 0; i < Provinces.Count; i++)
        {
            infected += Provinces[i].Population_Infected;
        }
        Infected = infected;

        float colorcalc = (float)Infected / (float)Population;
        _Mat.color = new Vector4(1, 1 - colorcalc, 1 - colorcalc, 1);
    }

    public void AddDataInfections()
    {
        DATA_TIMEAMOUNT newdata = new DATA_TIMEAMOUNT();
        newdata.Time = TimeHandler.TIME.CurrentTime;
        newdata.Date = TimeHandler.TIME.CurrentDate;
        newdata.Amount = Infected;
        Infections.Add(newdata);
    }
}
