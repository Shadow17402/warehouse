﻿using UnityEngine;
using UnityEngine.AI;

public class Pathfinder : MonoBehaviour
{

    public Camera cam;

    public NavMeshAgent agent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                //Move
                agent.SetDestination(hit.point);
            }
        }
    }
}