﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _Speed = 5;
    [SerializeField] private float _ScrollSpeed = 20;
    [SerializeField] private float _SprintSpeed = 8;
    [SerializeField] private bool _InverseScroll = false;

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
        if (Input.mousePosition.x < 10)
            xas = -1;
        if (Input.mousePosition.x > Screen.width - 10)
            xas = 1;
        if (Input.mousePosition.y < 10)
            zas = -1;
        if (Input.mousePosition.y > Screen.height - 10)
            zas = 1;

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
                "Infected: " + _CountryRef.Infected.ToString("n0");
            SimulationHandler.SIMHANDLER.Set_ClickInfoText(info);
        }

        //Update Province Info
        if (_ProvinceRef != null)
        {
            string info = "Province Name: " + _ProvinceRef.ProvinceProfile.ProvinceName + "\n" +
                "Population: " + _ProvinceRef.ProvinceProfile.Population.ToString("n0") + "\n" +
                "Infected: " + _ProvinceRef.Infected.ToString("n0");
            SimulationHandler.SIMHANDLER.Set_ClickInfoText(info);
        }
    }
}
