using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform Spawnpoint;
    public GameObject Prefab;
    
    void Start () {

        Instantiate(Prefab, Spawnpoint.position, Spawnpoint.rotation);
    }
}
