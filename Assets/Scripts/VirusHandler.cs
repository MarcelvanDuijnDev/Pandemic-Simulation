using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusHandler : MonoBehaviour
{
    [SerializeField] private List<Country> Countries;

    void Update()
    {
        for (int i = 0; i < Countries.Count; i++)
        {
            for (int j = 0; j < Countries[i]._Provinces.Count; j++)
            {
                if(Countries[i]._Provinces[j].Infected < Countries[i]._Provinces[j].Population)
                {
                    //For Testing
                    Countries[i]._Provinces[j].Add_Infected(10);
                    Countries[i].UpdateInfected();
                }
            }
        }
    }
}
