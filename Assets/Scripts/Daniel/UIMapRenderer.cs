using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapRenderer : Graphic
{

    public Grid grid;
    public float thickness = 10f;
    public int offset = 0;
    public bool test = true;
    public int updateCycles = 0;

    private Dictionary<Vector2,int> map = new Dictionary<Vector2,int>();
    private Dictionary<Vector2, int> tempMap = new Dictionary<Vector2, int>();

    protected override void Start()
    {
        InvokeRepeating("updateMap", 0, 0.5f);
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        float widthSqr = thickness * thickness;
        float distanceSqr = widthSqr / 2;
        float distance = Mathf.Sqrt(distanceSqr);

        //bottom left 0
        vertex.position = new Vector3(0, 0);
        vh.AddVert(vertex);
        //top left 1
        vertex.position = new Vector3(0, 50);
        vh.AddVert(vertex);
        //top right 2
        vertex.position = new Vector3(50 + distance, 50 + distance);
        vh.AddVert(vertex);
        //bottom right 3
        vertex.position = new Vector3(50 + distance, 0 - distance);
        vh.AddVert(vertex);
        //bottom left 4
        vertex.position = new Vector3(0 - distance,  0 - distance);
        vh.AddVert(vertex);
        //top left 5
        vertex.position = new Vector3(0 - distance, 50 + distance);
        vh.AddVert(vertex);
        //top right 6
        vertex.position = new Vector3(50, 50);
        vh.AddVert(vertex);
        //bottom right 7
        vertex.position = new Vector3(50, 0);
        vh.AddVert(vertex);

        //Left
        vh.AddTriangle(0, 1, 5);
        vh.AddTriangle(5, 4, 0);
        //Top
        vh.AddTriangle(5, 2, 6);
        vh.AddTriangle(6, 1, 5);
        //Right
        vh.AddTriangle(2, 3, 7);
        vh.AddTriangle(7, 6, 2);
        //Bottom
        vh.AddTriangle(0, 7, 3);
        vh.AddTriangle(3, 4, 0);

        offset = 8;
        if (test)
        {
            for (int i = 0; i < 50; i++)
            {
                drawSquare(new Vector2(i, 20), vh, offset, 0);
                offset += 4;
            }

            for (int i = 0; i < 50; i++)
            {
                drawSquare(new Vector2(i, 10), vh, offset, 1);
                offset += 4;
            }

            for (int i = 0; i < 50; i++)
            {
                drawSquare(new Vector2(i, 30), vh, offset, 2);
                offset += 4;
            }
        }


        foreach(Vector2 key in map.Keys)
        {
            drawSquare(key, vh, offset, map[key]);
            offset += 4;
        }

        foreach (Vector2 key in tempMap.Keys)
        {
            drawSquare(key, vh, offset, tempMap[key]);
            offset += 4;
        }

        updateCycles++;
        if(updateCycles == 5)
        {
            updateCycles = 0;
            clearTempMap();
        }
    }

    public void clearTempMap()
    {
        tempMap.Clear();
    }

    public void toggleTest()
    {
        test = !test;
    }

    public void drawSquare(Vector2 pos,VertexHelper vh,int offset,int map)
    {
        UIVertex vertex = UIVertex.simpleVert;
        if (map == 0)
            vertex.color = Color.green;
        else if(map == 1)
            vertex.color = Color.black;
        else
            vertex.color = color;

        vertex.position = new Vector3(pos.x - 0.3f, pos.y - 0.3f);
        vh.AddVert(vertex);

        vertex.position = new Vector3(pos.x - 0.3f, pos.y + 0.3f);
        vh.AddVert(vertex);

        vertex.position = new Vector3(pos.x + 0.3f, pos.y + 0.3f);
        vh.AddVert(vertex);

        vertex.position = new Vector3(pos.x + 0.3f, pos.y - 0.3f);
        vh.AddVert(vertex);

        vh.AddTriangle(0 + offset, 1 + offset, 2 + offset);
        vh.AddTriangle(2 + offset, 3 + offset, 0 + offset);
    }

    public void updateMap()
    {
        SetVerticesDirty();
    }

    public void processScan(RaycastHit[] hits)
    {
        Vector2 temp;
        //x=y z=x y=height
        foreach (RaycastHit hit in hits){
            if (hit.point.y < 0.1)
                continue;

            grid.setUnwalkable(hit.point);

            float rz = (float)Mathf.Round(hit.point.z * 10f) / 10f;
            float rx = (float)Mathf.Round(hit.point.x * 10f) / 10f;
            temp = new Vector2(50 - rz, rx);
            
            if(hit.transform.gameObject.tag == "Human")
            {
                if (tempMap.ContainsKey(temp)) { }
                else
                {
                    tempMap.Add(temp, 0);
                }
            }
            else if (hit.point.y < 2.4)
            {
                if (map.ContainsKey(temp)) { }
                else
                {
                    map.Add(temp, 1);
                }
            }
        }
    }
}
