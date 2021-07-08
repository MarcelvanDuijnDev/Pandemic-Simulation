using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeHandler : MonoBehaviour
{
    [Header("Settings")]
    public Vector3 TimeSpeed = new Vector3(0,0,1);
    public Vector3 StartTime = new Vector3(3,16,20);
    public Vector3 StartDate = new Vector3(7,7,2021);

    [Header("TextMeshPro")]
    [SerializeField] private TextMeshProUGUI _DateTimeText = null;

    private Vector3 _CurrentTime;
    private Vector3 _CurrentDate;

    void Start()
    {
        _CurrentTime = StartTime;
        _CurrentDate = StartDate;
    }

    void Update()
    {
        //Update Time
        _CurrentTime.x += TimeSpeed.x * Time.deltaTime;
        _CurrentTime.y += TimeSpeed.y * Time.deltaTime;
        _CurrentTime.z += TimeSpeed.z * Time.deltaTime;


        //Update Time
        if (_CurrentTime.z > 60) //Seconds > Minutes
        {
            _CurrentTime.z = 0;
            _CurrentTime.y += 1;
        }
        if(_CurrentTime.y > 60) //Minutes > Hours
        {
            _CurrentTime.y = 0;
            _CurrentTime.x += 1;
        }
        if(_CurrentTime.x >= 24) //Hours > Days
        {
            _CurrentTime.x = 0;
            _CurrentDate.x += 1;
        }

        //Update Date
        //DAYS > MONTHS
        if(_CurrentDate.y == 1 || _CurrentDate.y == 3 || _CurrentDate.y == 5 || _CurrentDate.y == 7 || _CurrentDate.y == 8 || _CurrentDate.y == 10 || _CurrentDate.y == 12) //31 days
        {
            if(_CurrentDate.x > 31)
            {
                _CurrentDate.x = 1;
                _CurrentDate.y += 1;
            }
        }
        if(_CurrentDate.y == 4 || _CurrentDate.y == 6 || _CurrentDate.y == 9 || _CurrentDate.y == 11) //30 days
        {
            if (_CurrentDate.x > 30)
            {
                _CurrentDate.x = 1;
                _CurrentDate.y += 1;
            }
        }
        if(_CurrentDate.y == 2) // 28 days (leap years not implemented)
        {
            if (_CurrentDate.x > 28)
            {
                _CurrentDate.x = 1;
                _CurrentDate.y += 1;
            }
        }
        //MONTHS > YEARS
        if(_CurrentDate.y > 12)
        {
            _CurrentDate.y = 1;
            _CurrentDate.z += 1;
        }

        //Display time/date
        _DateTimeText.text = "Current Time: " + Mathf.Floor(_CurrentTime.x) + ":" + Mathf.Floor(_CurrentTime.y) + ":" + Mathf.Floor(_CurrentTime.z) + 
            " / " + _CurrentDate.x + "- " + _CurrentDate.y + "- " + _CurrentDate.z;
    }

    public Vector3 Get_Time()
    {
        return _CurrentTime;
    }
    public Vector3 Get_Data()
    {
        return _CurrentDate;
    }
}

