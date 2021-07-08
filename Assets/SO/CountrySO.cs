using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Country", menuName = "SO/Country", order = 1)]
public class CountrySO : ScriptableObject
{
    public string CountryName;
    public int Population;
    public List<ProvincesSO> Provinces;
}
