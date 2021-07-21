using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SimulationSettingsHandler : MonoBehaviour
{
    [Header("Library")]
    [SerializeField] private Transform _VirusLibraryContent = null;
    [SerializeField] private GameObject _ButtonTemplate = null;
    public List<GameObject> VirusButtons = new List<GameObject>();

    [Header("Virus Info")]
    [SerializeField] private TextMeshProUGUI _Text_VirusName = null;
    [SerializeField] private TextMeshProUGUI _Text_RO = null;
    [SerializeField] private TextMeshProUGUI _Text_DeathRate = null;
    [SerializeField] private TextMeshProUGUI _Text_Duration = null;

    [Header("Other")]
    [SerializeField] private SimulationSettings _SimSettings = null;
    [SerializeField] private Button _Button_Continue = null;

    //Select
    private int _Selected = -1;
    private int _CheckSelected = -1;

    void Start()
    {
        CreateButtons();
        _Button_Continue.interactable = false;
    }

    void Update()
    {
        //EditButton
        if (_Selected >= 0)
        {
            if (_CheckSelected != _Selected)
            {
                DataHandler.DATASAVE.LoadVirusData();
                _Text_VirusName.text = DataHandler.DATASAVE.VirusData.Virus[_Selected].VirusName;
                _Text_DeathRate.text = DataHandler.DATASAVE.VirusData.Virus[_Selected].DeathRate.ToString();
                _Text_Duration.text = DataHandler.DATASAVE.VirusData.Virus[_Selected].InfectionDuration.ToString();
                _Text_RO.text = DataHandler.DATASAVE.VirusData.Virus[_Selected].Ro.ToString();
                _SimSettings.SetVirus(_Selected);
                _Button_Continue.interactable = true;
                _CheckSelected = _Selected;
            }
        }
    }


    private void CreateButtons()
    {
        // Add new buttons
        for (int i = 0; i < DataHandler.DATASAVE.VirusData.Virus.Count; i++)
        {
            GameObject newobj = GameObject.Instantiate(_ButtonTemplate, _VirusLibraryContent);
            VirusButtons.Add(newobj);
        }

        // Set Info
        for (int i = 0; i < VirusButtons.Count; i++)
        {
            VirusButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " " + (i + 1).ToString() + "   " + DataHandler.DATASAVE.VirusData.Virus[i].VirusName;
            int x = new int();
            x = i;
            VirusButtons[i].GetComponent<Button>().onClick.AddListener(delegate { SelectButton(x); });
        }

    }

    public void SelectButton(int id)
    {
        _Selected = id;
    }

}
