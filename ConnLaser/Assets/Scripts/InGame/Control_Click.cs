using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class Control_Click : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    Vector3 previousMousePosition;
    Vector3 mouseDelta;

    GameObject target,clicked, _clicked_parent, _clicked_sibling;
    float speed = 200f;
    int score = 0;

    private Transform cameraRotate;
    public Text score_txt;
    
    Camera _mainCam = null;

    /// 마우스가 다운된 오브젝트
    
    
    void Start()
    {
        _mainCam = Camera.main;
        score_txt.text = "회전 수 : " + Convert.ToString(score);
    }

    // Update is called once per frame 
    void Update()
    {
        //_clicked();
    
            Swipe();
        if (target != null)
            Drag();
        
    }



    private void _clicked()
    {
        try
        {
            if (Input.GetMouseButtonDown(0))
            {
                //타겟을 받아온다.
                clicked = GetClickedObject();
                _clicked_parent = clicked.transform.parent.gameObject;
                //_target_sibling = _target_parent.transform.Find("Target").gameObject;
                _clicked_sibling = _clicked_parent.transform.parent.gameObject;
                //타겟이 나인가?          
                
                //Debug.Log("target = " + clicked);                
                //Debug.Log("_target_parent = " + _clicked_parent);                
                //Debug.Log("_target_sibling = " + _clicked_sibling);                
                //Debug.Log("_target_sibling Target = " + _clicked_sibling.transform.Find("Target").gameObject);

                target = _clicked_sibling.transform.Find("Target").gameObject;
                
            }
            else if (Input.GetMouseButtonUp(0))
            {
               // target.transform.parent.gameObject.GetComponent<Rotate_Obj>().enabled = true;
                // gameObject.transform.gameObject.GetComponent<Control_Click>().enabled = false;
            }


            //마우스가 내려갔는지?

        }
        catch (NullReferenceException e)
        {
            Debug.Log(e);
        }
        finally
        {

        }
    }

    /// 
    /// 마우스가 내려간 오브젝트를 가지고 옵니다.
    /// 
    /// 선택된 오브젝트
    private GameObject GetClickedObject()
    {
        //충돌이 감지된 영역
        RaycastHit hit;
        //찾은 오브젝트
        GameObject target = null;

        //마우스 포이트 근처 좌표를 만든다.
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);

        //마우스 근처에 오브젝트가 있는지 확인
        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
        {
            //있다!

            //있으면 오브젝트를 저장한다.
            target = hit.collider.gameObject;
        }

        return target;
    }

    void Drag()
    {
        if (Input.GetMouseButton(0) )
        {
            mouseDelta = Input.mousePosition - previousMousePosition;
            mouseDelta *= 0.1f; //rotation speed 줄이기.
            _clicked_parent.transform.rotation = Quaternion.Euler(mouseDelta.y, -mouseDelta.x, 0) * _clicked_parent.transform.rotation;

        }
        else
        {
            if (_clicked_parent.transform.rotation != target.transform.rotation)
            {
                var step = speed * Time.deltaTime;
                _clicked_parent.transform.rotation = Quaternion.RotateTowards(_clicked_parent.transform.rotation, target.transform.rotation, step);

                //gameObject.transform.gameObject.GetComponent<Rotate_Obj>().enabled = false;
            }
        }
        previousMousePosition = Input.mousePosition;

    }

    void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked = GetClickedObject();
            if(clicked != null)
            {
                _clicked_parent = clicked.transform.parent.gameObject;             
                _clicked_sibling = _clicked_parent.transform.parent.gameObject;
                target = _clicked_sibling.transform.Find("Target").gameObject;
                //클릭 시 2D 좌표 얻어오기. 
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);                
            }



        }
        if (Input.GetMouseButtonUp(0) )
        {
            
            //클릭 후 2D 좌표 얻어오기. 
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //클릭 시의 좌표와 클릭 후의 차이를 구함.
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            //일반화.
            currentSwipe.Normalize();
            controlSwipe(currentSwipe);
        }

    }
    void controlSwipe(Vector2 currentSwipe)
    {
        

        if (target != null)
        {
            score++;
            score_txt.text = "회전 수 : " + Convert.ToString(score);
            if ((_mainCam.transform.position.x - target.transform.position.x) <= 0)
            {
                if ((_mainCam.transform.position.z - target.transform.position.z) <= 0)
                {
                    //드래그 방향.
                    if (LeftSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, 90, 0, Space.World);
                    }
                    else if (RightSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, -90, 0, Space.World);
                    }
                    else if (UpLeftSwipe(currentSwipe))
                    {
                        target.transform.Rotate(90, 0, 0, Space.World);
                    }
                    else if (UpRightSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, 0, -90, Space.World);
                    }
                    else if (DownLeftSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, 0, 90, Space.World);
                    }
                    else if (DownRightSwipe(currentSwipe))
                    {
                        target.transform.Rotate(-90, 0, 0, Space.World);
                    }
                    print("--");
                }
                else
                {
                    //나중에 다시 확인해볼것.
                    //드래그 방향.
                    if (LeftSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, 90, 0, Space.World);
                    }
                    else if (RightSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, -90, 0, Space.World);
                    }
                    else if (UpLeftSwipe(currentSwipe))
                    {
                        target.transform.Rotate(90, 0, 0, Space.World);
                    }
                    else if (UpRightSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, 0, -90, Space.World);
                    }
                    else if (DownLeftSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, 0, 90, Space.World);
                    }
                    else if (DownRightSwipe(currentSwipe))
                    {
                        target.transform.Rotate(-90, 0, 0, Space.World);
                    }
                    print("-+");
                }
            }
            else
            {
                if ((_mainCam.transform.position.z - target.transform.position.z) <= 0)
                {
                    //드래그 방향.
                    if (LeftSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, 90, 0, Space.World);
                    }
                    else if (RightSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, -90, 0, Space.World);
                    }
                    else if (UpLeftSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, 0, 90, Space.World);
                        
                        //target.transform.Rotate(90, 0, 0, Space.World);
                    }
                    else if (UpRightSwipe(currentSwipe))
                    {
                        target.transform.Rotate(90, 0, 0, Space.World);
                        //target.transform.Rotate(0, 0, -90, Space.World);
                       
                    }
                    else if (DownLeftSwipe(currentSwipe))
                    {
                        target.transform.Rotate(-90, 0, 0, Space.World);
                        //target.transform.Rotate(0, 0, 90, Space.World);
                    }
                    else if (DownRightSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, 0, -90, Space.World);
                        //target.transform.Rotate(-90, 0, 0, Space.World);

                    }
                    print("+-");
                }
                else
                {
                    //나중에 다시 확인해볼것.
                    //드래그 방향.
                    if (LeftSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, 90, 0, Space.World);
                    }
                    else if (RightSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, -90, 0, Space.World);
                    }
                    else if (UpLeftSwipe(currentSwipe))
                    {
                        target.transform.Rotate(90, 0, 0, Space.World);
                    }
                    else if (UpRightSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, 0, -90, Space.World);
                    }
                    else if (DownLeftSwipe(currentSwipe))
                    {
                        target.transform.Rotate(0, 0, 90, Space.World);
                    }
                    else if (DownRightSwipe(currentSwipe))
                    {
                        target.transform.Rotate(-90, 0, 0, Space.World);
                    }
                    print("++");
                }
            }
            ////드래그 방향.
            //if (LeftSwipe(currentSwipe))
            //{
            //    target.transform.Rotate(0, 90, 0, Space.World);
            //}
            //else if (RightSwipe(currentSwipe))
            //{
            //    target.transform.Rotate(0, -90, 0, Space.World);
            //}
            //else if (UpLeftSwipe(currentSwipe))
            //{
            //    target.transform.Rotate(90, 0, 0, Space.World);
            //}
            //else if (UpRightSwipe(currentSwipe))
            //{
            //    target.transform.Rotate(0, 0, -90, Space.World);
            //}
            //else if (DownLeftSwipe(currentSwipe))
            //{
            //    target.transform.Rotate(0, 0, 90, Space.World);
            //}
            //else if (DownRightSwipe(currentSwipe))
            //{
            //    target.transform.Rotate(-90, 0, 0, Space.World);
            //}
        }
    }

    bool LeftSwipe(Vector2 swipe)
    {
        return currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    bool RightSwipe(Vector2 swipe)
    {
        return currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }
    bool UpLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0 && currentSwipe.x < 0f;
    }

    bool UpRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0 && currentSwipe.x > 0f;
    }
    bool DownLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && currentSwipe.x < 0f;
    }

    bool DownRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && currentSwipe.x > 0f;
    }



}
