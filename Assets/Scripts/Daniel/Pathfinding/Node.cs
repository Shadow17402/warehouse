using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public bool walkable;
    public Vector3 worldPos;

    public int grid_x;
    public int grid_y;

    public int g_cost;
    public int h_cost;

    public Node parent;

    public Node(bool walkable, Vector3 worldPos, int grid_x, int grid_y)
    {
        this.walkable = walkable;
        this.worldPos = worldPos;
        this.grid_x = grid_x;
        this.grid_y = grid_y;
    }

    public void changeUnwalkable()
    {
        walkable = false;
    }

    public void changeWalkable()
    {
        walkable = true;
    }

    public int f_cost
    {
        get
        {
            return g_cost + h_cost;
        }
    }

}
