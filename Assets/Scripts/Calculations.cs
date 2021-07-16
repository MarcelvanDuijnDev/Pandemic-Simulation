using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculations : MonoBehaviour
{
    [Header("set")]
    public double totalpopulation = 7879110410;
    public float deathrate = 20;

    public bool startupdate = false;

    [Header("AddDate")]
    public float NewDateTime;

    [Header("Check")]
    public float newdatetimetimer;
    public double totalpopulationcalc;
    public double normalpopulation;
    public double infectedpopulation;
    public double deadpopulation;
    public float deathratecalc;

    public int newdate = 1;

    public CalcHealthy HealthyPop = new CalcHealthy();
    public List<Calc> Date = new List<Calc>();
    

    private void Start()
    {
        totalpopulationcalc = totalpopulation;
        deathratecalc = deathrate * 0.001f;

        AddDate();
        AddDate();
        AddDate();
    }

    void Update()
    {
        if (!startupdate)
            return;


        //Calculate 
        double infected = 0;
        infectedpopulation = 0;
        for (int i = 0; i < Date.Count; i++)
        {
            if(Date[i].Date.y <= TimeHandler.TIME.CurrentDate.y)
            {
                if(Date[i].Date.x < TimeHandler.TIME.CurrentDate.x)
                {
                    double calcdeath = Date[i].Amount * deathratecalc;
                    deadpopulation += Mathf.Floor((float)calcdeath);
                    double calchealthy = Date[i].Amount - calcdeath;
                    HealthyPop.Healthy += Mathf.Floor((float)calchealthy);

                    Date.RemoveAt(i);
                    return;
                }
            }

            infected += Date[i].Amount;
        }
        infectedpopulation += infected;
        normalpopulation = totalpopulation - infected - deadpopulation - HealthyPop.Healthy;


        //Add date
        if (normalpopulation >= 10000)
            newdatetimetimer += 1 * Time.deltaTime;
        if(newdatetimetimer >= NewDateTime)
        {
            AddDate();
            newdatetimetimer = 0;
        }
    }

    void AddDate()
    {
        Calc calc = new Calc();
        calc.Date = TimeHandler.TIME.Get_DateAfterDays(newdate);
        newdate++;
        calc.Amount = 10000;
        Date.Add(calc);
    }
}

[System.Serializable]
public class Calc
{
    public Vector3 Date; //Day/Month/Year
    public double Amount; //Total people
}

[System.Serializable]
public class CalcHealthy
{
    public double Healthy;
}


//This script is for testing large calculations