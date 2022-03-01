using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform onHand;
    public GameObject package;
   
    public void setPackage(GameObject package)
    {
        this.package = package;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if(other.gameObject.tag == "Shuttle"){
            Debug.Log(other);
            if (other.GetComponent<ForkliftController>().active == false){
                Debug.Log("Enter3");
                other.GetComponentInChildren<PacketStore>().setPacket(package);
                package.GetComponent<Rigidbody>().useGravity = false;
                onHand = other.transform.GetChild(0).gameObject.transform;
                package.transform.position = onHand.transform.position;
                package.transform.parent = GameObject.Find("Shuttle").transform;
                var active = GameObject.Find("Shuttle");
                active.GetComponent<ForkliftController>().SetActiveTrue();
                this.package = null;
            }
            
           
        }
        

    }

    

    
}