using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public static DataHandler DATASAVE;

    public VIRUSDATA VirusData = new VIRUSDATA();
    public DATA SaveFile = new DATA();


    void Awake()
    {
        DATASAVE = this;
        LoadVirusData();
    }

    //Data
    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(SaveFile, true);
        File.WriteAllText(Application.persistentDataPath + "/SaveData.json", jsonData);
    }
    public void LoadData()
    {
        try
        {
            string dataAsJson = File.ReadAllText(Application.persistentDataPath + "/SaveData.json");
            SaveFile = JsonUtility.FromJson<DATA>(dataAsJson);
        }
        catch
        {
            SaveData();
        }
    }
    public DATA GetSaveData()
    {
        return SaveFile;
    }

    //VirusData
    public void SaveVirusData()
    {
        string jsonData = JsonUtility.ToJson(VirusData, true);
        File.WriteAllText(Application.persistentDataPath + "/VirusData.json", jsonData);
    }
    public void LoadVirusData()
    {
        try
        {
            string dataAsJson = File.ReadAllText(Application.persistentDataPath + "/VirusData.json");
            VirusData = JsonUtility.FromJson<VIRUSDATA>(dataAsJson);
        }
        catch
        {
            SaveVirusData();
        }
    }
    public VIRUSDATA GetVirusSaveData()
    {
        return VirusData;
    }
}

//Virus
[System.Serializable]
public class VIRUSDATA
{
    public List<DATA_VIRUS> Virus;
}
[System.Serializable]
public class DATA_VIRUS
{
    public string VirusName;

    [Header("Infection")]
    public float Ro;
    public float InfectionDuration;
    public float DeathRate;
}

public class DATA
{
    public DATA_SPREAD VirusSpread;
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