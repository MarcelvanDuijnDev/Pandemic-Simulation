using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldView : MonoBehaviour
{
    [SerializeField] private List<GameObject> _Countries = new List<GameObject>();
    [SerializeField] private List<GameObject> _CountriesText = new List<GameObject>();

    [SerializeField] private float _HightSheck;

    private bool _CheckHight;

    void Update()
    {
        if(transform.position.y >= _HightSheck)
        {
            if(!_CheckHight)
            {
                for (int i = 0; i < _Countries.Count; i++)
                {
                    _Countries[i].GetComponent<MeshRenderer>().enabled = true;
                    _Countries[i].GetComponent<MeshCollider>().enabled = true;
                    _CountriesText[i].SetActive(true);
                }
                _CheckHight = true;
            }
        }
        else
        {
            if(_CheckHight)
            {
                for (int i = 0; i < _Countries.Count; i++)
                {
                    _Countries[i].GetComponent<MeshRenderer>().enabled = false;
                    _Countries[i].GetComponent<MeshCollider>().enabled = false;
                    _CountriesText[i].SetActive(false);
                }
                _CheckHight = false;
            }
        }
    }
}
