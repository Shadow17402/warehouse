using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dataclass : MonoBehaviour
{
    //Erstellt die instance
    private static Dataclass instance;

    //Methode /Singleton, welche die instance returned.
    public static Dataclass Instance { get { return instance; } }

    public List<Conveyor> lineA = new List<Conveyor>();
    public List<Conveyor> lineB = new List<Conveyor>();
    public List<Conveyor> lineC = new List<Conveyor>();

    public BoxSpawner spawnerA;
    public BoxSpawner spawnerB;
    public BoxSpawner spawnerC;
    public enum lineType
    {
        A,
        B,
        C
    }


    //Wird am Anfang des Games initialisiert.
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void changeLine(lineType type, bool active)
    {
        List<Conveyor> belts = null ;
        switch (type)
        {
            case lineType.A:
                belts = lineA;
                spawnerA.overflow = !active;
                break;
            case lineType.B:
                spawnerB.overflow = !active;
                belts = lineB;
                break;
            case lineType.C:
                belts = lineC;
                spawnerC.overflow = !active;
                
                break;

        }
        foreach(Conveyor belt in belts)
        {
            belt.overflow = !active;
            
        }


       
    }

}
