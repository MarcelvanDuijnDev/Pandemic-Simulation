using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculations : MonoBehaviour
{
    private float totalpopulation = 7879110410;
    private List<Vector4> _Person = new List<Vector4>();

    void Update()
    {

    }


}

[System.Serializable]
public class Calc
{
    public Vector3 Date; //Day/Month/Year
    public double Amount; //Total people

}


//This script is for testing large calculations