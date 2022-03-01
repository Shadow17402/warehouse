using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxCollider : MonoBehaviour
{
    public Transform Spawnpoint;
    public GameObject Prefab;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Forklift"){
           Instantiate(Prefab, Spawnpoint.position, Spawnpoint.rotation);
        }

    }
     
}
