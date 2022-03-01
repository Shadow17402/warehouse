using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorRayCast : MonoBehaviour
{
    public GameObject lastHit;
    public Vector3 collision = Vector3.zero;

    public Dataclass.lineType type;
    Dataclass data;
    //Daniel
    public GameObject pickup;

    // Start is called before the first frame update
    void Start()
    {
         data= Dataclass.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        var ray = new Ray(this.transform.position, this.transform.forward);

        RaycastHit box;
        if(Physics.Raycast(ray, out box, 2))
        {
            lastHit = box.transform.gameObject;
            pickup.GetComponent<Pickup>().setPackage(lastHit);
            data.changeLine(type, false);
        }
        else
        {
            data.changeLine(type, true);
        }
    }
}
