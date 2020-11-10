using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_button : MonoBehaviour
{
    Camera Mcam;
    float speed = 500.0f;
    public GameObject target;
    SceneController sc;
    // Start is called before the first frame update
    void Start()

    {
        Mcam = Camera.main;
        sc = GameObject.Find("GameController").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {

        target = sc.target;
    }
       
    public void OnclickLeft()
    {
    target.transform.Rotate(Vector3.up * 90, Space.World);
    // Mcam.transform.Rotate(Vector3.down * Time.deltaTime * speed);
}
    public void OnclickRight()
    {
    target.transform.Rotate(Vector3.down * 90, Space.World);
    //Mcam.transform.Rotate(Vector3.up * Time.deltaTime *speed);
}
    public void OnclickUp()
    {
        target.transform.Rotate(Vector3.back * 90, Space.World);
    }
    public void OnclickDown()
    {
        target.transform.Rotate(Vector3.forward * 90, Space.World);
        //Mcam.transform.Rotate(Vector3.up * Time.deltaTime *speed);
    }
}
