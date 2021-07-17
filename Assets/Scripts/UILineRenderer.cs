using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILineRenderer : Graphic
{
    public enum Types {Normal, Infected, Dead, Healthy };

    [Header("Settings")]
    [SerializeField] private float _LineWidth = 1;
    [SerializeField] private Types Type = Types.Infected;
    
    [Header("Ref")]
    public UIGraph Grid;
    public TextMeshProUGUI _CountryText;

    [HideInInspector] public List<Vector2> Points;
    [HideInInspector] public Country _Country;
    [HideInInspector] public Province _Province;

    [Header("Text")]
    [SerializeField] private Transform TextDataObj;
    [SerializeField] private TextMeshProUGUI TextData;

    private float _Width;
    private float _Height;
    private float _UnitWidth;
    private float _UnitHeight;
    private Vector2Int GridSize = new Vector2Int(1, 1);

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        _Width = rectTransform.rect.width;
        _Height = rectTransform.rect.height;

        _UnitWidth = _Width / (float)GridSize.x;
        _UnitHeight = _Height / (float)GridSize.y;

        if(Points.Count < 2)
            return;

        float angle = 0;

        for (int i = 0; i < Points.Count; i++)
        {
            Vector2 point = Points[i];
            if(i<Points.Count - 1)
                angle = GetAngle(Points[i],Points[i+1]) + 45f;
            DrawVerticesForPoint(point,vh,angle);
        }

        for (int i = 0; i < Points.Count-1; i++)
        {
            int index = i * 2;
            vh.AddTriangle(index + 0, index + 1, index + 3);
            vh.AddTriangle(index + 3, index + 2, index + 0);
        }
    }

    public float GetAngle(Vector2 me, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - me.y, target.x - me.x) * (180 / Mathf.PI));
    }

    void DrawVerticesForPoint(Vector2 point, VertexHelper vh, float angle)
    {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = Quaternion.Euler(0,0,angle) * new Vector3(-_LineWidth / 2, 0);
        vertex.position += new Vector3(_UnitWidth * point.x, _UnitHeight * point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(_LineWidth / 2, 0);
        vertex.position += new Vector3(_UnitWidth * point.x, _UnitHeight * point.y);
        vh.AddVert(vertex);
    }

    private void Update()
    {
        //Display Info
        if (_Country != null)
        {
            _CountryText.text = _Country.CountryProfile.CountryName;
            switch (Type)
            {
                case Types.Infected:
                    Points = Graph_Country_Infections();
                    break;
                case Types.Normal:
                    Points = Graph_Country_Normal();
                    break;
                case Types.Dead:
                    Points = Graph_Country_Dead();
                    break;
                case Types.Healthy:
                    Points = Graph_Country_Healthy();
                    break;
            }
        }
        if (_Province != null)
        {
            _CountryText.text = _Province.ProvinceProfile.ProvinceName;
            
            switch(Type)
            {
                case Types.Infected:
                    Points = Graph_Province_Infections();
                    break;
                case Types.Normal:
                    Points = Graph_Province_Normal();
                    break;
                case Types.Dead:
                    Points = Graph_Province_Dead();
                    break;
                case Types.Healthy:
                    Points = Graph_Province_Healthy();
                    break;
            }
        }
        SetVerticesDirty();

        //Text
        if (_Country == null && _Province == null)
            _CountryText.text = "---";

        //Check grid Size
        if (Grid != null)
        {
            if(GridSize != Grid.gridSize)
            {
                GridSize = Grid.gridSize;
                SetVerticesDirty();
            }
        }


        //Graph Text
        if (TextDataObj != null && Points != null)
            if (Points.Count > 0)
                TextDataObj.transform.position = new Vector3(TextDataObj.transform.position.x, 50 + _UnitHeight * 3 * Points[Points.Count -1].y, 0);
    }

    private List<Vector2> Graph_Country_Infections()
    {
        List<Vector2> newpoints = new List<Vector2>();

        if (_Country.DATAInfections.Count < GridSize.x)
        {
            for (int i = 0; i < _Country.DATAInfections.Count; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Country.DATAInfections[i].Amount / _Country.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        else
        {
            for (int i = 0; i < GridSize.x; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Country.DATAInfections[i + (_Country.DATAInfections.Count - GridSize.x)].Amount / _Country.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        TextData.text = "infections (" + (_Country.Population_Infected / _Country.Population * 100).ToString("0.00") + "%)";
        return newpoints;
    }
    private List<Vector2> Graph_Country_Normal()
    {
        List<Vector2> newpoints = new List<Vector2>();

        if (_Country.DATANormal.Count < GridSize.x)
        {
            for (int i = 0; i < _Country.DATANormal.Count; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Country.DATANormal[i].Amount / _Country.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        else
        {
            for (int i = 0; i < GridSize.x; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Country.DATANormal[i + (_Country.DATANormal.Count - GridSize.x)].Amount / _Country.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        TextData.text = "normal (" + (_Country.Population_Normal / _Country.Population * 100).ToString("0.00") + "%)";
        return newpoints;
    }
    private List<Vector2> Graph_Country_Dead()
    {
        List<Vector2> newpoints = new List<Vector2>();

        if (_Country.DATADead.Count < GridSize.x)
        {
            for (int i = 0; i < _Country.DATADead.Count; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Country.DATADead[i].Amount / _Country.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        else
        {
            for (int i = 0; i < GridSize.x; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Country.DATADead[i + (_Country.DATADead.Count - GridSize.x)].Amount / _Country.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        TextData.text = "dead (" + (_Country.Population_Dead / _Country.Population * 100).ToString("0.00") + "%)";
        return newpoints;
    }
    private List<Vector2> Graph_Country_Healthy()
    {
        List<Vector2> newpoints = new List<Vector2>();

        if (_Country.DATAHealthy.Count < GridSize.x)
        {
            for (int i = 0; i < _Country.DATAHealthy.Count; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Country.DATAHealthy[i].Amount / _Country.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        else
        {
            for (int i = 0; i < GridSize.x; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Country.DATAHealthy[i + (_Country.DATAHealthy.Count - GridSize.x)].Amount / _Country.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        TextData.text = "healthy (" + (_Country.Population_Healthy / _Country.Population * 100).ToString("0.00") + "%)";
        return newpoints;
    }

    private List<Vector2> Graph_Province_Infections()
    {
        List<Vector2> newpoints = new List<Vector2>();

        if (_Province.DATAInfections.Count < GridSize.x)
        {
            for (int i = 0; i < _Province.DATAInfections.Count; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Province.DATAInfections[i].Amount / _Province.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        else
        {
            for (int i = 0; i < GridSize.x; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Province.DATAInfections[i + (_Province.DATAInfections.Count - GridSize.x)].Amount / _Province.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        TextData.text = "infections (" + (_Province.Population_Infected / _Province.Population * 100).ToString("0.00") + "%)";
        return newpoints;
    }
    private List<Vector2> Graph_Province_Normal()
    {
        List<Vector2> newpoints = new List<Vector2>();

        if (_Province.DATANormal.Count < GridSize.x)
        {
            for (int i = 0; i < _Province.DATANormal.Count; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Province.DATANormal[i].Amount / _Province.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        else
        {
            for (int i = 0; i < GridSize.x; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Province.DATANormal[i + (_Province.DATANormal.Count - GridSize.x)].Amount / _Province.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        TextData.text = "normal (" + (_Province.Population_Normal / _Province.Population * 100).ToString("0.00") + "%)";
        return newpoints;
    }
    private List<Vector2> Graph_Province_Dead()
    {
        List<Vector2> newpoints = new List<Vector2>();

        if (_Province.DATADead.Count < GridSize.x)
        {
            for (int i = 0; i < _Province.DATADead.Count; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Province.DATADead[i].Amount / _Province.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        else
        {
            for (int i = 0; i < GridSize.x; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Province.DATADead[i + (_Province.DATADead.Count - GridSize.x)].Amount / _Province.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        TextData.text = "dead (" + (_Province.Population_Dead / _Province.Population * 100).ToString("0.00") + "%)";
        return newpoints;
    }
    private List<Vector2> Graph_Province_Healthy()
    {
        List<Vector2> newpoints = new List<Vector2>();

        if (_Province.DATAHealthy.Count < GridSize.x)
        {
            for (int i = 0; i < _Province.DATAHealthy.Count; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Province.DATAHealthy[i].Amount / _Province.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        else
        {
            for (int i = 0; i < GridSize.x; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Province.DATAHealthy[i + (_Province.DATAHealthy.Count - GridSize.x)].Amount / _Province.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        TextData.text = "healthy (" + (_Province.Population_Healthy / _Province.Population * 100).ToString("0.00") + "%)";
        return newpoints;
    }
}
