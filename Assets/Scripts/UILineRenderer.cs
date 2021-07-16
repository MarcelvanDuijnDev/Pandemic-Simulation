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
            Points = Graph_Country();
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
    }

    private List<Vector2> Graph_Country()
    {
        List<Vector2> newpoints = new List<Vector2>();

        if (_Country.Infections.Count < GridSize.x)
        {
            for (int i = 0; i < _Country.Infections.Count; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Country.Infections[i].Amount / _Country.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }
        else
        {
            for (int i = 0; i < GridSize.x; i++)
            {
                Vector2 newpoint = new Vector2(i, (float)((_Country.Infections[i + (_Country.Infections.Count - GridSize.x)].Amount / _Country.Population) * GridSize.y));
                newpoints.Add(newpoint);
            }
        }

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
        return newpoints;
    }
}
