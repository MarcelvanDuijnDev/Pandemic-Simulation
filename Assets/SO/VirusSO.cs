using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Virus", menuName = "SO/Virus", order = 0)]
public class VirusSO : ScriptableObject
{
    public string VirusName;

    [Header("Infection")]
    public float Ro;
    public float InfectionDuration;
    public float DeathRate;

}
