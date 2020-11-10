using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class Draw_Line : MonoBehaviour
{
    //public Canvas End_Game_canvas;

    public int reflections;
    public float maxLength;
    public bool _isPlaying;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;

    IngameUI cheakGame;

    SceneController _sceneController;

    void Start()
    {
        _isPlaying = true;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetColors(Color.blue, Color.blue);
        lineRenderer.SetWidth(0.1f, 0.1f);

        Transform LaserPos = transform;
        LaserPos.position += new Vector3(0, 0.01f, 0);
        _sceneController = GameObject.Find("GameController").GetComponent<SceneController>();

    }

    // Update is called once per frame
    void Update()
    {

        DrawLaser();
    }
    void DrawLaser()
    {
        ray = new Ray(transform.position, transform.forward);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));


                if (hit.collider.tag == "goal")
                {
                    //게임 종료.
                    // print("Goal");
                    _isPlaying = false;
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
