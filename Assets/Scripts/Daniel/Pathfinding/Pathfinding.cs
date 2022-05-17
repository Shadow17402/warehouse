using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private PathRequestManager requestManager;
    private Grid grid;

    private void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
    }

    public void StartFindPath(Vector3 start, Vector3 target)
    {
        StartCoroutine(findPath(start, target));
    }

    private IEnumerator findPath(Vector3 start, Vector3 target)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;
        
        Node startNode = grid.NodeFromWorldPoint(start);
        Node targetNode = grid.NodeFromWorldPoint(target);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node current = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].f_cost < current.f_cost || openSet[i].f_cost == current.f_cost && openSet[i].h_cost < current.h_cost)
                {
                    current = openSet[i];
                }
            }

            openSet.Remove(current);
            closedSet.Add(current);

            if(current == targetNode)
            {
                pathSuccess = true;
                break;
            }

            foreach(Node neighbor in grid.GetNeighbors(current))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                    continue;

                int newCostToNeighbor = current.g_cost + GetDistance(current, neighbor);
                if(newCostToNeighbor < neighbor.g_cost || !openSet.Contains(neighbor))
                {
                    neighbor.g_cost = newCostToNeighbor;
                    neighbor.h_cost = GetDistance(neighbor, targetNode);
                    neighbor.parent = current;
                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
        yield return null;
        if(pathSuccess)
            waypoints = retracePath(startNode, targetNode);
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    private Vector3[] retracePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node current = end;

        while(current != start)
        {
            path.Add(current);
            current = current.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    private Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].grid_x - path[i].grid_x, path[i - 1].grid_y - path[i].grid_y);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPos);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    private int GetDistance(Node a, Node b)
    {
        int dstX = Mathf.Abs(a.grid_x - b.grid_x);
        int dstY = Mathf.Abs(a.grid_y - b.grid_y);

        if(dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
