using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    public float CurrentDate = 0;
    public Vector3 StartTime = new Vector3(3,16);
    public Vector3 StartDate = new Vector3(7,7,2021);


    void Start()
    {
        CurrentDate = 0;

        //Year/Month/Day
        CurrentDate += StartDate.z * 31449600;
        CurrentDate += StartDate.y * 2620800;
        CurrentDate += StartDate.x * 86400;

        //Hour/Minute/Seconds
        CurrentDate += StartTime.x * 3600;
        CurrentDate += StartTime.y * 60;
        CurrentDate += StartTime.z;


        float year = Mathf.FloorToInt(CurrentDate / 31449600);
        float month = Mathf.FloorToInt((CurrentDate - (year * 31449600)) / 2620800);
        float day = Mathf.FloorToInt((CurrentDate - (year * 31449600) - (month * 2620800)));



        Debug.Log(year);
        Debug.Log(month);
        Debug.Log(day);
    }

    void Update()
    {
    }
}
