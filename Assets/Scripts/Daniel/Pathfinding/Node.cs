using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public bool walkable;
    public Vector3 worldPos;

    public Node(bool walkable, Vector3 worldPos)
    {
        this.walkable = walkable;
        this.worldPos = worldPos;
    }

    public void changeWalkable()
    {
        walkable = false;
    }

}
