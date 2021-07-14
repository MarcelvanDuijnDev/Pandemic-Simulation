using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Province : MonoBehaviour
{
    public ProvincesSO ProvinceProfile;
    public float Population;
    public float Infected;

    private Material _Mat;

    public List<DATA_TIMEINFECTIONS> Infections = new List<DATA_TIMEINFECTIONS>();

    private void Start()
    {
        Population = ProvinceProfile.Population;
        _Mat = GetComponent<MeshRenderer>().material;
    }

    public void Add_Infected(int amount)
    {
        Infected += amount;
        _Mat.color = new Vector4(1, 1 - Infected/Population, 1 - Infected / Population, 1);
    }

    public void AddDataInfections()
    {
        DATA_TIMEINFECTIONS newdata = new DATA_TIMEINFECTIONS();
        newdata.Time = TimeHandler.TIME.CurrentTime;
        newdata.Date = TimeHandler.TIME.CurrentDate;
        newdata.Infected = Infected;
        Infections.Add(newdata);
    }
}
