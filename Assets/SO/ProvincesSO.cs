using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Provinces", menuName = "SO/Provinces", order = 1)]
public class ProvincesSO : ScriptableObject
{
    public string ProvinceName;
    public int Population;
}
