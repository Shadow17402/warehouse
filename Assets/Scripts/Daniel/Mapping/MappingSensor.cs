using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MappingSensor : MonoBehaviour
{
    private Vector3[] hits;
    public UIMapRenderer renderer;

    private void Start()
    {
        hits = new Vector3[MappingHelper.numViewDirections];
    }

    // Update is called once per frame
    void Update()
    {
        rayCast();
        renderer.processScan(hits);
    }


    public void rayCast()
    {
        Vector3[] rayDirections = MappingHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Vector3 position = transform.position;
            Vector3 dir = transform.TransformDirection(rayDirections[i]);
            if (dir.y < 0.5f && dir.y > -0.5) { 
                Ray ray = new Ray(position, dir);
                RaycastHit hit;
                if (Physics.SphereCast(ray, 0.25f, out hit, 200))
                {
                    Debug.DrawLine(ray.origin, hit.point);
                    Vector3 hit3 = hit.point;
                    /*hit3.y = hit3.x;
                    hit3.x = hit3.z;
                    hit3.z = 0;*/
                    hits[i] = hit3;
                }
            }
            /*if (!Physics.SphereCast(ray, settings.boundsRadius, settings.collisionAvoidDst, settings.obstacleMask))
            {
                return dir;
            }*/
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            foreach(Vector3 hit in hits)
            {
                Gizmos.DrawSphere(hit - new Vector3(-100,0,0), 0.05f);
            }

        }
    }

}
