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
        if(other.gameObject.tag == "Shuttle"){
            if (other.GetComponent<ForkliftController>().active == true)
            {
                packet = other.GetComponentInChildren<PacketStore>().getPacket();
                other.GetComponentInChildren<PacketStore>().setPacket(null);
                print("it worked");
                //packet.transform.parent = null;
                //GetComponent<Rigidbody>().useGravity = true;
                //packet.GetComponent<Rigidbody>().useGravity = true;
                packet.transform.position = onCube.transform.position;
                packet.transform.parent = onCube.parent;
                var active = GameObject.Find("Shuttle");
                active.GetComponent<ForkliftController>().SetActiveFalse();
            }
        }

    }
}
