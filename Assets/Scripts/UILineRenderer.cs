using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILineRenderer : Graphic
{
    [Header("Settings")]
    [SerializeField] private float _LineWidth = 1;
    
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
        {
            return;
        }

        float angle = 0;

        for (int i = 0; i < Points.Count; i++)
        {
            Vector2 point = Points[i];

            if(i<Points.Count - 1)
            {
                angle = GetAngle(Points[i],Points[i+1]) + 45f;
            }

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
        List<Vector2> newpoints = new List<Vector2>();
        if (_Country != null)
        {
            _CountryText.text = _Country.CountryProfile.CountryName;

            if (_Country.Infections.Count < GridSize.x)
            {
                for (int i = 0; i < _Country.Infections.Count; i++)
                {
                    Vector2 newpoint = new Vector2(i, (float)((_Country.Infections[i].Infected / _Country.Population) * GridSize.y));
                    newpoints.Add(newpoint);
                }
            }
            else
            {
                for (int i = 0; i < GridSize.x; i++)
                {
                    Vector2 newpoint = new Vector2(i, (float)((_Country.Infections[i + (_Country.Infections.Count - GridSize.x)].Infected / _Country.Population) * GridSize.y));
                    newpoints.Add(newpoint);
                }
            }
        }
        else
        {
            if(_Province != null)
            {
                _CountryText.text = _Province.ProvinceProfile.ProvinceName;

                if (_Province.Infections.Count < GridSize.x)
                {
                    for (int i = 0; i < _Province.Infections.Count; i++)
                    {
                        Vector2 newpoint = new Vector2(i, (float)((_Province.Infections[i].Infected / _Province.Population) * GridSize.y));
                        newpoints.Add(newpoint);
                    }
                }
                else
                {
                    for (int i = 0; i < GridSize.x; i++)
                    {
                        Vector2 newpoint = new Vector2(i, (float)((_Province.Infections[i + (_Province.Infections.Count - GridSize.x)].Infected / _Province.Population) * GridSize.y));
                        newpoints.Add(newpoint);
                    }
                }
            }
        }
        Points = newpoints;
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
}
