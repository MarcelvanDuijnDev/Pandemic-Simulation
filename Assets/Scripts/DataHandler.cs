using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    [SerializeField] private DATA _Data = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

[System.Serializable]
public class DATA
{
    public DATA_SPREAD VirusSpread;
    public DATA_VIRUS Virus;
}

[System.Serializable]
public class DATA_VIRUS
{
    public string VirusName;

}

[System.Serializable]
public class DATA_SPREAD
{
    public List<DATA_SPREAD_PLACE> Country;
}

[System.Serializable]
public class DATA_SPREAD_PLACE
{
    public string PlaceName;
    public int Population;
    public List<DATA_TIMEINFECTIONS> Infections;
}

[System.Serializable]
public class DATA_TIME
{
    public Vector3 Time;
    public Vector3 Date;
}

[System.Serializable]
public class DATA_TIMEINFECTIONS
{
    public Vector3 Time;
    public Vector3 Date;
    public double Infected;
}