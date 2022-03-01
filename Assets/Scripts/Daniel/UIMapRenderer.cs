﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapRenderer : Graphic
{

    public Dictionary<int,ArrayList> map = new Dictionary<int, ArrayList>();

    public float thickness = 10f;

    public int offset = 0;

    public bool test = true;

    protected override void Start()
    {
        map.Add(0, new ArrayList());  //Shuttle
        map.Add(1, new ArrayList());  //Passable Obstacle
        map.Add(2, new ArrayList());  //Obstacle
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;
        //bottom left
        vertex.position = new Vector3(0, 0);
        vh.AddVert(vertex);
        //top left
        vertex.position = new Vector3(0, 50);
        vh.AddVert(vertex);
        //top right
        vertex.position = new Vector3(50, 50);
        vh.AddVert(vertex);
        //bottom right
        vertex.position = new Vector3(50, 0);
        vh.AddVert(vertex);

        //vh.AddTriangle(0, 1, 2);
        //vh.AddTriangle(2, 3, 0);

        float widthSqr = thickness * thickness;
        float distanceSqr = widthSqr / 2;
        float distance = Mathf.Sqrt(distanceSqr);

        vertex.position = new Vector3(0 - distance,  0 - distance);
        vh.AddVert(vertex);

        vertex.position = new Vector3(0 - distance, 50 - distance);
        vh.AddVert(vertex);

        vertex.position = new Vector3(50 - distance, 50 - distance);
        vh.AddVert(vertex);

        vertex.position = new Vector3(50 - distance, 0 - distance);
        vh.AddVert(vertex);

        //Left
        vh.AddTriangle(0, 1, 5);
        vh.AddTriangle(5, 4, 0);
        //Top
        vh.AddTriangle(1, 2, 6);
        vh.AddTriangle(6, 5, 1);
        //Right
        vh.AddTriangle(2, 3, 7);
        vh.AddTriangle(7, 6, 2);
        //Bottom
        vh.AddTriangle(3, 0, 4);
        vh.AddTriangle(4, 7, 3);

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

        foreach (Vector2 point in map[1])
        {
            drawSquare(new Vector2(point.x, point.y), vh, offset, 1);
            offset += 4;
            Debug.Log(point.x + " " + point.y);
        }

        foreach (Vector2 point in map[2])
        {
            drawSquare(new Vector2(point.x, point.y), vh, offset, 2);
            offset += 4;
            Debug.Log(point.x + " " + point.y);
        }

    }

    public void toggleTest()
    {
        test = !test;
    }

    public void drawSquare(Vector2 pos,VertexHelper vh,int offset,int map)
    {
        UIVertex vertex = UIVertex.simpleVert;
        if (map == 0)
            vertex.color = Color.yellow;
        else if(map == 1)
            vertex.color = Color.green;
        else if (map == 2)
            vertex.color = Color.black;
        else
            vertex.color = color;

        vertex.position = new Vector3(pos.x - 0.5f,pos.y - 0.5f);
        vh.AddVert(vertex);

        vertex.position = new Vector3(pos.x - 0.5f, pos.y + 0.5f);
        vh.AddVert(vertex);

        vertex.position = new Vector3(pos.x + 0.5f, pos.y + 0.5f);
        vh.AddVert(vertex);

        vertex.position = new Vector3(pos.x + 0.5f, pos.y - 0.5f);
        vh.AddVert(vertex);

        vh.AddTriangle(0 + offset, 1 + offset, 2 + offset);
        vh.AddTriangle(2 + offset, 3 + offset, 0 + offset);
    }

    public void markDirty()
    {
        SetVerticesDirty();
    }

    public void processScan(Vector3[] hits)
    {
        ArrayList shuttle = new ArrayList();
        ArrayList passable = new ArrayList();
        ArrayList obstacle = new ArrayList();


        //x=y z=x y=height
        foreach(Vector3 hit in hits){
            if (hit.y < 0.1)
                continue;
            else if (hit.y < 1.85)
                obstacle.Add(new Vector2(50-hit.z, hit.x));
            else if (hit.y > 1.85)
                passable.Add(new Vector2(50-hit.z, hit.x));
            
        }
        map.Remove(1);
        map.Remove(2);
        map.Add(1, passable);
        map.Add(2, obstacle);

        SetVerticesDirty();
    }
}
