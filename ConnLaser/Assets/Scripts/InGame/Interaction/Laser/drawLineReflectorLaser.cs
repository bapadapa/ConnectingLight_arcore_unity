using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawLineReflectorLaser : MonoBehaviour
{
    //public GameObject obj_start;

    float maxLength = 1000.0f;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;
    private LineRenderer lineRenderer;
    GameObject obj_Receiver, obj_sender;
    drawLineReflectorLaser DL;
    float remainingLength;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        remainingLength = 100.0f;
        DL = this.GetComponent<drawLineReflectorLaser>();
    }

    // Update is called once per frame
    void Update()
    {
        DrawLaser(this.gameObject);
    }

    void DrawLaser()
    {
        ray = new Ray(transform.position, transform.forward);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        for (int i = 0; i < 10; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                GameObject reflection;

                //if (hit.collider.name == "BackReflector")
                //{
                //    reflection = hit.transform.parent.gameObject.transform.Find("BottomReflector").gameObject;
                //    lineRenderer.SetPosition(lineRenderer.positionCount - 1, reflection.transform.position);
                //    remainingLength -= Vector3.Distance(ray.origin, hit.point);
                //    //DL.DrawLaser(reflection);
                //}
                //if (hit.collider.name == "BottomReflector")
                //{
                //    reflection = hit.transform.parent.gameObject.transform.Find("BackReflector").gameObject;
                //    lineRenderer.SetPosition(lineRenderer.positionCount - 1, reflection.transform.position);
                //    remainingLength -= Vector3.Distance(ray.origin, hit.point);
                //    //DL.DrawLaser(reflection);
                //}

                if (hit.collider.tag == "goal")
                {
                    //게임 종료.
                    // print("Goal");
                    popSys.Instance.openPopUp();

                    //cheakGame._isEnd();
                    //End_Game_canvas.enabled = true;

                }
                if (hit.collider.tag != "reflectObj")
                {
                    return;
                }
            }

            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }

    }


    public void DrawLaser(GameObject obj_start)
    {

        ray = new Ray(transform.position, transform.forward);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, obj_start.transform.position);
        float remainingLength = maxLength;

        for (int i = 0; i < 10; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                lineRenderer.SetPosition(lineRenderer.positionCount++, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.transform.position, hit.transform.forward);
                GameObject reflection;

                //if (hit.collider.name == "BackReflector")
                //{
                //    reflection = hit.transform.parent.gameObject.transform.Find("BottomReflector").gameObject;
                //    lineRenderer.SetPosition(lineRenderer.positionCount - 1, reflection.transform.position);
                //    remainingLength -= Vector3.Distance(ray.origin, hit.point);
                //    //DL.DrawLaser(reflection);
                //}
                //if (hit.collider.name == "BottomReflector")
                //{
                //    reflection = hit.transform.parent.gameObject.transform.Find("BackReflector").gameObject;
                //    lineRenderer.SetPosition(lineRenderer.positionCount - 1, reflection.transform.position);
                //    remainingLength -= Vector3.Distance(ray.origin, hit.point);
                //    //DL.DrawLaser(reflection);
                //}

                if (hit.collider.tag == "goal")
                {
                    //게임 종료.
                    // print("Goal");
                    popSys.Instance.openPopUp();

                    //cheakGame._isEnd();
                    //End_Game_canvas.enabled = true;

                }
                if (hit.collider.tag != "reflectObj")
                {
                    return;
                }
            }

            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }
    }
}




