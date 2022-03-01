using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Conveyor : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt;
    public bool overflow = false;


    public Dataclass.lineType type;



    // Start is called before the first frame update
    void Start()
    {
        Dataclass data = Dataclass.Instance;
        switch (type)
        {
            case Dataclass.lineType.A:
                data.lineA.Add(this);
                break;
            case Dataclass.lineType.B:
                data.lineB.Add(this);
                break;
            case Dataclass.lineType.C:
                data.lineC.Add(this);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!overflow)
        {
            for (int i = 0; i <= onBelt.Count - 1; i++)
            {
                onBelt[i].GetComponent<Rigidbody>().velocity = speed * direction * Time.deltaTime;
            }
        }
    }

    //Box lands on the ConveyorBelt
    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    //Box leaves the Conveyorbelt
    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }



   
}
