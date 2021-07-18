using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _Speed = 5;
    [SerializeField] private float _ScrollSpeed = 20;
    [SerializeField] private float _SprintSpeed = 8;
    [SerializeField] private bool _InverseScroll = false;
    [SerializeField] private bool _CheckCorner = true;

    private Country _CountryRef;
    private Province _ProvinceRef;

    private GameObject _CheckPrevObj;
    private float _CurrentSpeed;

    void Update()
    {
        Movement();
        SceneInteraction();
    }

    private void Movement()
    {
        //variables
        float xas = 0;
        float zas = 0;
        float yas = Input.mouseScrollDelta.y;

        //Mouse
        if (_CheckCorner)
        {
            if (Input.mousePosition.x < 10)
                xas = -1;
            if (Input.mousePosition.x > Screen.width - 10)
                xas = 1;
            if (Input.mousePosition.y < 10)
                zas = -1;
            if (Input.mousePosition.y > Screen.height - 10)
                zas = 1;
        }

        //WSAD
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            xas = Input.GetAxis("Horizontal");
            zas = Input.GetAxis("Vertical");
        }

        if (Input.GetKey(KeyCode.LeftShift))
            _CurrentSpeed = _SprintSpeed;
        else
            _CurrentSpeed = _Speed;

        //Zoom/Scroll
        if (_InverseScroll)
            yas *= _ScrollSpeed;
        else
            yas *= -_ScrollSpeed;

        //Apply Movement
        transform.Translate(new Vector3(xas * _CurrentSpeed, yas, zas * _CurrentSpeed) * Time.deltaTime);
    }
    private void SceneInteraction()
    {
        //Mouse pos / hit
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            SimulationHandler.SIMHANDLER.Set_ClickInfoLocation(hit.transform);

            //Get Country Ref
            if (hit.transform.gameObject.layer == 6)
            {
                if (_CountryRef == null || hit.transform.gameObject != _CheckPrevObj)
                {
                    _CountryRef = hit.transform.GetComponent<Country>();
                    _CheckPrevObj = hit.transform.gameObject;
                }
            }
            else
                _CountryRef = null;

            //Get Province Ref
            if (hit.transform.gameObject.layer == 7)
            {
                if (_ProvinceRef == null || hit.transform.gameObject != _CheckPrevObj)
                {
                    _ProvinceRef = hit.transform.GetComponent<Province>();
                    _CheckPrevObj = hit.transform.gameObject;
                }
            }
            else
                _ProvinceRef = null;
        }

        //Update Country Info
        if (_CountryRef != null)
        {
            string info = "Country Name: " + _CountryRef.CountryProfile.CountryName + "\n" +
                "Population: " + _CountryRef.CountryProfile.Population.ToString("n0") + "\n" +
                "Normal: " + _CountryRef.Population_Normal.ToString("n0") + " (" + (_CountryRef.Population_Normal / _CountryRef.Population * 100).ToString("0.00") + "%)" + "\n" +
                "Healthy: " + _CountryRef.Population_Healthy.ToString("n0") + " (" + (_CountryRef.Population_Healthy / _CountryRef.Population * 100).ToString("0.00") + "%)" + "\n" +
                "Infected: " + _CountryRef.Population_Infected.ToString("n0") + " (" + (_CountryRef.Population_Infected / _CountryRef.Population * 100).ToString("0.00") + "%)" + "\n" +
                "Dead: " + _CountryRef.Population_Dead.ToString("n0") + " (" + (_CountryRef.Population_Dead / _CountryRef.Population * 100).ToString("0.00") + "%)";
            SimulationHandler.SIMHANDLER.Set_ClickInfoText(info);
        }

        //Update Province Info
        if (_ProvinceRef != null)
        {
            string info = "Province Name: " + _ProvinceRef.ProvinceProfile.ProvinceName + "\n" +
                "Population: " + _ProvinceRef.ProvinceProfile.Population.ToString("n0") + "\n" +
                "Normal: " + _ProvinceRef.Population_Normal.ToString("n0") + " (" + (_ProvinceRef.Population_Normal / _ProvinceRef.Population * 100).ToString("0.00") + "%)" + "\n" +
                "Healthy: " + _ProvinceRef.Population_Healthy.ToString("n0") + " (" + (_ProvinceRef.Population_Healthy / _ProvinceRef.Population * 100).ToString("0.00") + "%)" + "\n" +
                "Infected: " + _ProvinceRef.Population_Infected.ToString("n0") + " (" + (_ProvinceRef.Population_Infected / _ProvinceRef.Population * 100).ToString("0.00") + "%)" + "\n" +
                "Dead: " + _ProvinceRef.Population_Dead.ToString("n0") + " (" + (_ProvinceRef.Population_Dead / _ProvinceRef.Population * 100).ToString("0.00") + "%)";
            SimulationHandler.SIMHANDLER.Set_ClickInfoText(info);
        }

        //SetGraph
        for (int i = 0; i < SimulationHandler.SIMHANDLER.Graph.Count; i++)
        {
            SimulationHandler.SIMHANDLER.Graph[i]._Province = _ProvinceRef;
            SimulationHandler.SIMHANDLER.Graph[i]._Country = _CountryRef;
        }
        
    }
}
