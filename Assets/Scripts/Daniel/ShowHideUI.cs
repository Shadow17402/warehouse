using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideUI : MonoBehaviour
{
    
    public GameObject _object;
    public bool active = false;


    // Start is called before the first frame update
    void Start()
    {
        _object.SetActive(false);
    }

    public void showHide()
    {
        active = !active;
        _object.SetActive(active);
    }

}
