using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MappingSensor : MonoBehaviour
{
    [SerializeField]
    public RaycastHit[] hits;
    public UIMapRenderer renderer;

    private void Start()
    {
        hits = new RaycastHit[MappingHelper.numViewDirections];
        InvokeRepeating("rayCast", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {

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
                    /*hit3.y = hit3.x;
                    hit3.x = hit3.z;
                    hit3.z = 0;*/
                    if(hit.point.y<2.5)
                        hits[i] = hit;
                }
            }
            /*if (!Physics.SphereCast(ray, settings.boundsRadius, settings.collisionAvoidDst, settings.obstacleMask))
            {
                return dir;
            }*/
        }
        renderer.processScan(hits);
    }

    public void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            foreach(RaycastHit hit in hits)
            {
                Gizmos.DrawSphere(hit.point - new Vector3(-100,0,0), 0.05f);
            }

        }
    }

}
