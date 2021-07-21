using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationSettings : MonoBehaviour
{
    public int VirusID = 0;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetVirus(int id)
    {
        VirusID = id;
    }
}
