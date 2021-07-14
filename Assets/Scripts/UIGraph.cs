using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGraph : Graphic
{
    public Vector2Int gridSize = new Vector2Int(1,1);
    public float OutlineWidth = 10;

    private float _Width;
    private float _Height;
    private float _CellWidth;
    private float _CellHeight;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        _Width = rectTransform.rect.width;
        _Height = rectTransform.rect.height;

        _CellWidth = _Width / gridSize.x;
        _CellHeight = _Height / gridSize.y;

        int count = 0;
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                DrawCell(x, y, count, vh);
                count++;
            }
        }
    }

    private void DrawCell(int x, int y, int index, VertexHelper vh)
    {
        float xPos = _CellWidth * x;
        float yPos = _CellHeight * y;

        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = new Vector3(xPos, yPos);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos, yPos + _CellHeight);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + _CellWidth, yPos + _CellHeight);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + _CellWidth, yPos);
        vh.AddVert(vertex);

        float widthSqr = OutlineWidth * OutlineWidth;
        float distanceSqr = widthSqr / 2f;
        float distance = Mathf.Sqrt(distanceSqr);

        vertex.position = new Vector3(xPos + distance, yPos + distance);
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + distance, yPos + (_CellHeight - distance));
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + (_CellWidth - distance), yPos + (_CellHeight - distance));
        vh.AddVert(vertex);

        vertex.position = new Vector3(xPos + (_CellWidth - distance), yPos + distance);
        vh.AddVert(vertex);

        int offset = index * 8;

        //Left
        vh.AddTriangle(offset + 0, offset + 1, offset + 5);
        vh.AddTriangle(offset + 5, offset + 4, offset + 0);

        //Top
        vh.AddTriangle(offset + 1, offset + 2, offset + 6);
        vh.AddTriangle(offset + 6, offset + 5, offset + 1);

        //Right
        vh.AddTriangle(offset + 2, offset + 3, offset + 7);
        vh.AddTriangle(offset + 7, offset + 6, offset + 2);

        //Bottom
        vh.AddTriangle(offset + 3, offset + 0, offset + 4);
        vh.AddTriangle(offset + 4, offset + 7, offset + 3);
    }
}