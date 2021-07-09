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

    private Country _CountryRef;
    private Province _ProvinceRef;

    private GameObject _CheckPrevObj;
    private float _CurrentSpeed;

    void Update()
    {
        //CameraMovement
        if (Input.GetKey(KeyCode.LeftShift))
            _CurrentSpeed = _SprintSpeed;
        else
            _CurrentSpeed = _Speed;

        float xas = Input.GetAxis("Horizontal");
        float zas = Input.GetAxis("Vertical");
        float yas = Input.mouseScrollDelta.y;

        if (_InverseScroll)
            yas *= _ScrollSpeed;
        else
            yas *= -_ScrollSpeed;

        //Apply Movement
        transform.Translate(new Vector3(xas * _CurrentSpeed, yas, zas * _CurrentSpeed) * Time.deltaTime);


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
        if(_CountryRef != null)
        {
            string info = "Country Name: " + _CountryRef.CountryProfile.CountryName + "\n" +
                "Population: " + _CountryRef.CountryProfile.Population + "\n" +
                "Infected: " + _CountryRef.Infected;
            SimulationHandler.SIMHANDLER.Set_ClickInfoText(info);
        }

        //Update Province Info
        if(_ProvinceRef != null)
        {
            string info = "Province Name: " + _ProvinceRef.ProvinceProfile.ProvinceName + "\n" +
                "Population: " + _ProvinceRef.ProvinceProfile.Population + "\n" +
                "Infected: " + _ProvinceRef.Infected;
            SimulationHandler.SIMHANDLER.Set_ClickInfoText(info);
        }
    }
}
