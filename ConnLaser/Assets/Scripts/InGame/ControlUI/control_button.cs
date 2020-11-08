using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_button : MonoBehaviour
{
    Camera Mcam;
    float speed = 500.0f;

    // Start is called before the first frame update
    void Start()
    {
        Mcam = Camera.main;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnclickLeft()
    {
        Mcam.transform.Rotate(Vector3.down * Time.deltaTime * speed);
    }
    public void OnclickRight()
    {
        Mcam.transform.Rotate(Vector3.up * Time.deltaTime *speed);
    }

}
