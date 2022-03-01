using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{

    public GameObject box;
    public bool doSpawning = true;
    public float initialDelay, spawnDelay;
    public bool overflow = false;
    public Dataclass.lineType type;
    public GameObject packets;
    // Start is called before the first frame update
    void Start()
    {
        Dataclass data = Dataclass.Instance;
        switch (type)
        {
            case Dataclass.lineType.A:
                data.spawnerA = this;
                break;
            case Dataclass.lineType.B:
                data.spawnerB = this;
                break;
            case Dataclass.lineType.C:
                data.spawnerC = this;
                break;
        }
        InvokeRepeating("SpawnObject", initialDelay, spawnDelay);
    }

    public void SpawnObject()
	{
        if (!overflow) { 
            GameObject spawnedBox = Instantiate(box, transform.position, transform.rotation);
            spawnedBox.transform.parent = packets.transform;
        }
            
        if (!doSpawning)
		{
            CancelInvoke("SpawnObject");
		}
	}

}
