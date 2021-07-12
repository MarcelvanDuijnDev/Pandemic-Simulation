using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    [Header("Settings")]
    public Vector3 TimeSpeed = new Vector3(0,0,1);
    public Vector3 StartTime = new Vector3(3,16,20);
    public Vector3 StartDate = new Vector3(7,7,2021);

    public Vector3 CurrentTime;
    public Vector3 CurrentDate;

    public static TimeHandler TIME = null;

    void Start()
    {
        TIME = this;

        CurrentTime = StartTime;
        CurrentDate = StartDate;
    }

    void Update()
    {
        //Update Time
        CurrentTime.x += TimeSpeed.x * Time.deltaTime;
        CurrentTime.y += TimeSpeed.y * Time.deltaTime;
        CurrentTime.z += TimeSpeed.z * Time.deltaTime;


        //Update Time
        if (CurrentTime.z > 60) //Seconds > Minutes
        {
            CurrentTime.z = 0;
            CurrentTime.y += 1;
        }
        if(CurrentTime.y > 60) //Minutes > Hours
        {
            CurrentTime.y = 0;
            CurrentTime.x += 1;
        }
        if(CurrentTime.x >= 24) //Hours > Days
        {
            CurrentTime.x = 0;
            CurrentDate.x += 1;
        }

        //Update Date
        //DAYS > MONTHS
        if(CurrentDate.y == 1 || CurrentDate.y == 3 || CurrentDate.y == 5 || CurrentDate.y == 7 || CurrentDate.y == 8 || CurrentDate.y == 10 || CurrentDate.y == 12) //31 days
        {
            if(CurrentDate.x > 31)
            {
                CurrentDate.x = 1;
                CurrentDate.y += 1;
            }
        }
        if(CurrentDate.y == 4 || CurrentDate.y == 6 || CurrentDate.y == 9 || CurrentDate.y == 11) //30 days
        {
            if (CurrentDate.x > 30)
            {
                CurrentDate.x = 1;
                CurrentDate.y += 1;
            }
        }
        if(CurrentDate.y == 2) // 28 days (leap years not implemented)
        {
            if (CurrentDate.x > 28)
            {
                CurrentDate.x = 1;
                CurrentDate.y += 1;
            }
        }
        //MONTHS > YEARS
        if(CurrentDate.y > 12)
        {
            CurrentDate.y = 1;
            CurrentDate.z += 1;
        }
    }
}

