using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Control_Ray : MonoBehaviour
{
    public  Canvas End_Game_canvas;
    public LineRenderer lineRender;
    RaycastHit hit;

    float MaxDistance = 3f; // 거리.
    public GameObject obj_start;
    public Text Time_text;
    GameObject obj_Receiver, obj_sender;
    float text_timer;
    public bool _isPlaying = true;

    Vector3 start_lr;
    float timer;
    

    // Start is called before the first frame update
    void Start()
    {
        End_Game_canvas.enabled = false;
        timer = 0.0f;
        text_timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        if (timer > 0.5)
        {
            //Action
            timer = 0;
            Rayzer(gameObject);
        }
        if (_isPlaying)
        {
        
            text_timer += Time.deltaTime;
            Time_text.text = "소요시간 : " + Convert.ToString(text_timer.ToString("N2"));
        }
            

    }
    void Rayzer(GameObject start_obj)
    {
     //   lineRender.SetPosition(0, start_obj.transform.position);
        Debug.DrawRay(start_obj.transform.position, start_obj.transform.right * MaxDistance, Color.blue, 0.3f);
        if (Physics.Raycast(start_obj.transform.position, start_obj.transform.right, out hit, MaxDistance))
        {
            if (hit.transform.tag == "game_obj")
            {
                hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                transform.GetComponent<MeshRenderer>().material.color = Color.red;
                //Debug.Log("hit");

                if (hit.transform.name == "Front")
                {
                   
                    //  Debug.Log("Front!!!");
                    obj_Receiver = hit.transform.gameObject;
                    obj_sender = obj_Receiver.transform.parent.gameObject.transform.Find("Top").gameObject;
                    //레이져 맞은 부위에서 raycast 실행

                    //Debug.DrawRay(obj_sender.transform.position, obj_sender.transform.right * MaxDistance, Color.blue, 0.3f);
                    //Physics.Raycast(obj_sender.transform.position, obj_sender.transform.right, out hit, MaxDistance);
                    Rayzer(obj_sender);
                }
                else if (hit.transform.name == "Top")
                {
                 
                    // Debug.Log("Top!!!");
                    obj_Receiver = hit.transform.gameObject;
                    obj_sender = obj_Receiver.transform.parent.gameObject.transform.Find("Front").gameObject;

                    //레이져 맞은 부위에서 raycast 실행
                    //Debug.DrawRay(obj_sender.transform.position, obj_sender.transform.right * MaxDistance, Color.blue, 0.3f);
                    //Physics.Raycast(obj_sender.transform.position, obj_sender.transform.right, out hit, MaxDistance);
                    Rayzer(obj_sender);
                }


            }
            else if (hit.transform.tag == "goal")
            {
                
                //게임 종료.
                print("Goal");
                _isPlaying = false;
                Time_text.text = "소요시간 : " + Convert.ToString(text_timer.ToString("N2"));
                End_Game_canvas.enabled = true;

            }
            else
            {
                transform.GetComponent<MeshRenderer>().material.color = Color.blue;
            }

        }
        else
        {
            transform.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }

}

