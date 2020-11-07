using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Control_setting : MonoBehaviour
{
    public GameObject Map;
    public GameObject Reflector;
    public GameObject clickControl;
    private bool mode;

    // Start is called before the first frame update
    void Start()
    {
        mode = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Control_Map_btn()
    {
        if (!mode)
        {
            Map.GetComponent<Rotate_Map>().enabled = true;
            Reflector.GetComponent<Rotate_Obj>().enabled = false;
            clickControl.GetComponent<Control_Click>().enabled = false;
            mode = true;
        }
        
    }

    public void Control_Reflector_btn()
    {
        if (mode)
        {
            Map.GetComponent<Rotate_Map>().enabled = false;
            clickControl.GetComponent<Control_Click>().enabled = true;
            //Reflector.GetComponent<Rotate_Obj>().enabled = true;
            mode = false;
        }
    }
}
