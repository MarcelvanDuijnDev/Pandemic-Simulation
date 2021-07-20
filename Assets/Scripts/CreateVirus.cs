using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateVirus : MonoBehaviour
{

    [Header("Ref")]
    [SerializeField] private TextMeshProUGUI _Text_VirusName;
    [SerializeField] private TMP_InputField _Input_VirusName;
    [SerializeField] private TMP_InputField _Input_Ro;
    [SerializeField] private TMP_InputField _Input_DeathRate;
    [SerializeField] private TMP_InputField _Input_Duration;


    [SerializeField] private Transform _VirusLibraryContent = null;
    [SerializeField] private GameObject _ButtonTemplate = null;

    [Header("Premade Virus")]
    [SerializeField] private List<VirusSO> _Virus = new List<VirusSO>();

    public List<GameObject> VirusButtons = new List<GameObject>();

    private int _Selected = -1;
    private int _CheckSelected = -1;

    void Start()
    {
        Refresh();
    }

    void Update()
    {
        //EditButton
        if(_Selected >= 0)
        {
            if(_CheckSelected != _Selected)
            {
                DataHandler.DATASAVE.LoadVirusData();
                _Text_VirusName.text = DataHandler.DATASAVE.VirusData.Virus[_Selected].VirusName;
                _Input_VirusName.text = DataHandler.DATASAVE.VirusData.Virus[_Selected].VirusName;
                _Input_DeathRate.text = DataHandler.DATASAVE.VirusData.Virus[_Selected].DeathRate.ToString();
                _Input_Duration.text = DataHandler.DATASAVE.VirusData.Virus[_Selected].InfectionDuration.ToString();
                _Input_Ro.text = DataHandler.DATASAVE.VirusData.Virus[_Selected].InfectionDuration.ToString();
                _CheckSelected = _Selected;
            }


            DataHandler.DATASAVE.VirusData.Virus[_Selected].VirusName = _Input_VirusName.text;
            DataHandler.DATASAVE.VirusData.Virus[_Selected].DeathRate = float.Parse(_Input_DeathRate.text);
            DataHandler.DATASAVE.VirusData.Virus[_Selected].InfectionDuration = float.Parse(_Input_Duration.text);
            DataHandler.DATASAVE.VirusData.Virus[_Selected].Ro = float.Parse(_Input_Ro.text);
            
            //MinMax
            if (DataHandler.DATASAVE.VirusData.Virus[_Selected].DeathRate > 100)
            {
                DataHandler.DATASAVE.VirusData.Virus[_Selected].DeathRate = 100;
                _Input_DeathRate.text = "100";
            }
            if (DataHandler.DATASAVE.VirusData.Virus[_Selected].DeathRate < 0)
            {
                DataHandler.DATASAVE.VirusData.Virus[_Selected].DeathRate = 0;
                _Input_DeathRate.text = "0";
            }
            if (DataHandler.DATASAVE.VirusData.Virus[_Selected].InfectionDuration < 1)
            {
                DataHandler.DATASAVE.VirusData.Virus[_Selected].InfectionDuration = 1;
                _Input_Duration.text = "1";
            }
            if (DataHandler.DATASAVE.VirusData.Virus[_Selected].Ro < 0)
            {
                DataHandler.DATASAVE.VirusData.Virus[_Selected].Ro = 0;
                _Input_Ro.text = "0";
            }

            _Text_VirusName.text = DataHandler.DATASAVE.VirusData.Virus[_Selected].VirusName;
        }
        else
        {
            if (DataHandler.DATASAVE.VirusData.Virus.Count > 0)
                _Selected = 0;
            else
                _Selected = -1;
        }    
    }

    private void Refresh()
    {
        for (int i = 0; i < VirusButtons.Count; i++)
            Destroy(VirusButtons[i]);

        if (DataHandler.DATASAVE.VirusData.Virus.Count == 0)
        {
            for (int i = 0; i < _Virus.Count; i++)
            {
                DATA_VIRUS newvirus = new DATA_VIRUS();
                newvirus.VirusName = _Virus[i].name;
                newvirus.DeathRate = _Virus[i].DeathRate;
                newvirus.Ro = _Virus[i].Ro;
                newvirus.InfectionDuration = _Virus[i].InfectionDuration;
                DataHandler.DATASAVE.VirusData.Virus.Add(newvirus);
            }
            DataHandler.DATASAVE.SaveVirusData();
        }

        VirusButtons.Clear();

        for (int i = 0; i < DataHandler.DATASAVE.VirusData.Virus.Count; i++)
        {
            GameObject newobj = GameObject.Instantiate(_ButtonTemplate, _VirusLibraryContent);
            VirusButtons.Add(newobj);
        }

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

    public void CreateNewVirus()
    {
        DATA_VIRUS newvirus = new DATA_VIRUS();
        newvirus.VirusName = "NewVirus" + Random.Range(0,1000).ToString();
        newvirus.DeathRate = 10;
        newvirus.InfectionDuration = 7;
        newvirus.Ro = 1;
        DataHandler.DATASAVE.VirusData.Virus.Add(newvirus);
        DataHandler.DATASAVE.SaveVirusData();
        Refresh();
    }

    public void DeleteVirus()
    {
        DataHandler.DATASAVE.VirusData.Virus.RemoveAt(_Selected);
        DataHandler.DATASAVE.SaveVirusData();
        _Selected = -1;
        _CheckSelected = -2;
        Refresh();
    }

    public void SaveNewVirus()
    {
        DataHandler.DATASAVE.SaveVirusData();
        Refresh();
    }
}
