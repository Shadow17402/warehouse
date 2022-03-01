using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPacket : MonoBehaviour
{
    public GameObject packet;
    public GameObject hitboxShuttle;
    public Transform onCube;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Forklift"){
            
            
            //if(packet.transform.position == hitboxShuttle.transform.position){
                if(GameObject.Find("Forklift").GetComponent<ForkliftController>().active == true){
                    print("it worked");
                    packet.transform.parent = null;
                    //GetComponent<Rigidbody>().useGravity = true;
                    //packet.GetComponent<Rigidbody>().useGravity = true;
                    packet.transform.position = onCube.transform.position;
                    packet.transform.parent = GameObject.Find("TestCube").transform;
                    var active = GameObject.Find("Forklift");
                    active.GetComponent<ForkliftController>().SetActiveFalse();
                }
                
               
            
            
        }

    }
}
