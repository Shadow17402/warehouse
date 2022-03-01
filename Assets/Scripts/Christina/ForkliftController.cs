
using UnityEngine;
using UnityEngine.AI;

public class ForkliftController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public bool active = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
          Ray ray = cam.ScreenPointToRay(Input.mousePosition);
          RaycastHit hit;

          if(Physics.Raycast(ray, out hit))
          {
              agent.SetDestination(hit.point);
          }
        }
    }

    public void SetActiveTrue(){
        active = true;
    }
    public void SetActiveFalse(){
        active = false;
    }
    public void MoveAgent(Vector3 dest){
        agent.SetDestination(dest);
    }
}
