using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class Draw_Line : MonoBehaviour
{
    //public Canvas End_Game_canvas;

    public int reflections;
    public float maxLength;

    public GameObject particle;
    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;


    SceneController _sceneController;
    Draw_Line DL;

    float _Time;
    int _rotateTime;
    bool _isPlaying;

    dataControl dataControl;

    void Start()
    {
        dataControl = GameObject.Find("saveManager").GetComponent<dataControl>();
        dataControl.loadData(GameObject.Find("bestScore").GetComponent<Text>());


        lineRenderer = GetComponent<LineRenderer>();
        //lineRenderer.SetColors(Color.blue, Color.blue);
        //lineRenderer.SetWidth(0.1f, 0.1f);

        Transform LaserPos = transform;
        LaserPos.position += new Vector3(0, 0.01f, 0);
        _sceneController = GameObject.Find("GameController").GetComponent<SceneController>();        


        //시간 및 회전수.
        _Time =  0.0f;
        _isPlaying = true;
        _rotateTime = 0;



    }

    // Update is called once per frame
    void Update()
    {
        DrawLaser();

        if(_isPlaying)
            _Time += Time.deltaTime;
        
       

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
                Debug.Log(hit.normal);
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

                if (hit.collider.tag == "goal")
                {

                    _isPlaying = false;
                    Debug.Log("END");

                    //클리어 저장.
                    string []stageTok = SceneManager.GetActiveScene().name.Split('_');
                    int stage = int.Parse(stageTok[stageTok.Length - 1]);
                    float saveTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "score");
                    
                    if (_Time <  saveTime || saveTime == 0)
                        dataControl.saveData(SceneManager.GetActiveScene().name,stage, _Time);
                    if(int.Parse(stageTok[stageTok.Length - 1]) < 4)
                        popSys.Instance.openPopUp(_Time.ToString("N2"), _sceneController.rotateTime.ToString());
                    else
                    {
                        popSys.Instance.openPopUp("다음 스테이지 준비중이에요");
                    }

                }
                if (hit.collider.tag != "reflectObj")
                {
                    particle.transform.position = hit.transform.position;
                    particle.transform.rotation = Quaternion.LookRotation(hit.normal);
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
