using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    
    public GameObject prefab; 
    public List<GameObject> shuttles = new List<GameObject>();
    public Vector3 coordinatesOut;
    public Vector3 coordinatesDest;

    void Start(){
        shuttles.Add(prefab);
        coordinatesOut = new Vector3(-3.22f,0f,5.93f);
        coordinatesDest = new Vector3(-4.9f,-0.6955996f,-1.043527f);
        MovePackage(coordinatesOut, coordinatesDest);
    }

    public void OnClick(){
         shuttles.Add(prefab);
         Instantiate(prefab, transform.position, Quaternion.identity);
         
    }

    public void MovePackage(Vector3 outlet, Vector3 dest){
        foreach(GameObject shuttle in shuttles){
            if(shuttle.GetComponent<ForkliftController>().active == false){
                
                MoveToPackage(outlet, shuttle);
                //await new WaitUntil(() => shuttle.GetComponent<ForkliftController>().active == true);
                MovePackageToDest(dest, shuttle);
                //StartCoroutine(waiter(shuttle, dest));
                 /**while(shuttle.GetComponent<ForkliftController>().active == false);
                {
                }**/
                
    
            }
        }
        /**int length = shuttles.Count;
        print(length);
        if(GameObject.Find("Forklift").GetComponent<ForkliftController>().active == false){
                GameObject.Find("Forklift").GetComponent<ForkliftController>().MoveAgent(outlet);
            }**/
        
        
    }
    public void MovePackageToDest(Vector3 dest, GameObject shuttle){
        shuttle.GetComponent<ForkliftController>().MoveAgent(dest);
    }
    public void MoveToPackage(Vector3 outlet, GameObject shuttle){
         shuttle.GetComponent<ForkliftController>().MoveAgent(outlet);
    }
    /**IEnumerator waiter(GameObject shuttle, Vector3 dest){
        
        yield return new WaitUntil(() => shuttle.GetComponent<ForkliftController>().active == true);
        MovePackageToDest(dest, shuttle);
    }**/

    

    
    

   
}
