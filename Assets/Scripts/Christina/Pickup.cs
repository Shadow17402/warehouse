using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform onHand;
    public GameObject package;
   
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Forklift"){
            if(GameObject.Find("Forklift").GetComponent<ForkliftController>().active == false){
                //GetComponent<Rigidbody>().useGravity = false;
                
                package.transform.position = onHand.transform.position;
                package.transform.parent = GameObject.Find("Forklift").transform;
                var active = GameObject.Find("Forklift");
                active.GetComponent<ForkliftController>().SetActiveTrue();
            }
            
           
        }
        

    }

    

    
}