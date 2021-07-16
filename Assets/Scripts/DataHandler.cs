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

public class DATA
{
    public DATA_SPREAD VirusSpread;
    public DATA_VIRUS Virus;
}

public class DATA_VIRUS
{
    public string VirusName;

}

public class DATA_SPREAD
{
    public List<DATA_SPREAD_PLACE> Country;
}

public class DATA_SPREAD_PLACE
{
    public string PlaceName;
    public int Population;
    public List<DATA_TIMEAMOUNT> Infections;
}

public class DATA_TIME
{
    public Vector3 Time;
    public Vector3 Date;
}

public class DATA_TIMEAMOUNT
{
    public Vector3 Time;
    public Vector3 Date;
    public double Amount;
}