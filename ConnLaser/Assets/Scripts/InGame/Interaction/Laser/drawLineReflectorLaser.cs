using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawLineReflectorLaser : MonoBehaviour
{
    public GameObject obj_start;

    float maxLength = 1000.0f;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;
    private LineRenderer lineRenderer;
    GameObject obj_Receiver, obj_sender;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetColors(Color.red, Color.yellow);
        lineRenderer.SetWidth(0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        DrawLaser(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collition Occur");
        //if(transform.name == "reflector01")
        //{
        //    Transform tr = GetComponent<Transform>().Find("reflector02");
        //    DrawLaser(tr);
        //}
        //else if (transform.name == "reflector02")
        //{
        //    Transform tr = GetComponent<Transform>().Find("reflector01");
        //    DrawLaser(tr);
        //}
    }

    void DrawLaser(GameObject start)
    {

        ray = new Ray(start.transform.position, start.transform.forward);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, start.transform.position);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, maxLength))
        {
            lineRenderer.positionCount += 1;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("gameObj"))
                if (hit.transform.name == "reflector01")
                {

                    Debug.Log("reflector01!!!");
                    obj_Receiver = hit.transform.gameObject;
                    obj_sender = obj_Receiver.transform.parent.gameObject.transform.Find("reflector02").gameObject;
                    //레이져 맞은 부위에서 raycast 실행

                    //Debug.DrawRay(obj_sender.transform.position, obj_sender.transform.right * MaxDistance, Color.blue, 0.3f);
                    //Physics.Raycast(obj_sender.transform.position, obj_sender.transform.right, out hit, MaxDistance);
                    lineRenderer.positionCount += 1;
                    DrawLaser(obj_sender);
                }
                else if (hit.transform.name == "reflector02")
                {

                    Debug.Log("reflector02!!!");
                    obj_Receiver = hit.transform.gameObject;
                    obj_sender = obj_Receiver.transform.parent.gameObject.transform.Find("reflector01").gameObject;

                    //레이져 맞은 부위에서 raycast 실행
                    //Debug.DrawRay(obj_sender.transform.position, obj_sender.transform.right * MaxDistance, Color.blue, 0.3f);
                    //Physics.Raycast(obj_sender.transform.position, obj_sender.transform.right, out hit, MaxDistance);
                    lineRenderer.positionCount += 1;
                    DrawLaser(obj_sender);
                }

            if (hit.collider.tag == "goal")
            {

            }


        }


    }



}
