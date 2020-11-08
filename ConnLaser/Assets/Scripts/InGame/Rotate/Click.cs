
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using GoogleARCore;
using GoogleARCore.Examples.Common;

#if unity_editor
using input = googlearcore.instantpreviewinput;
#endif

public class Click : MonoBehaviour
{
    Vector2 startPos, endPos, directionPos;

    GameObject currentTouch;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _InstantiateOnTouch();        
    }


    GameObject CastRay()
    {
        GameObject target = null;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //클릭 위치 근처에 오브젝트가 있나 ?
        if (Physics.Raycast(ray.origin, ray.direction * 30, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }



    public void _InstantiateOnTouch()
    {
        //Touch touch;
        //touch = Input.GetTouch(0);
        ////입력이 있다면.

        //if(Input.touchCount != 0)
        //{
        //    //터치한게 GameObj가 아니면 return.
        //    if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        //    {
        //        return;
        //    }
        //    Rotate();
        //}
        

        //// If the player has not touched the screen, we are done with this update.
        //if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        //{
        //    return;
        //}

        ////// Should not handle input if the player is pointing on UI.
        //if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        //{
        //    return;
        //}

        Touch touch;
        //터치가 들어오면.
        if (Input.touchCount != 0)
        {
           
            touch = Input.GetTouch(0);
            currentTouch = CastRay();
            
            if (touch.phase == TouchPhase.Began && currentTouch != null)
            {
                if (currentTouch.tag == "Mirror" || currentTouch.layer == LayerMask.NameToLayer("gameObj"))
                {
                    target = currentTouch.transform.parent.gameObject;
                    startPos = touch.position;
                    Debug.Log("IN");
                    Debug.Log(target.name);
                }
                Debug.Log(target.name);
            }

            if (touch.phase == TouchPhase.Ended && target != null)
            {
                endPos = touch.position;
                directionPos = startPos - endPos;
                Rotate();
            }
        }
    }
    ///-------------------------------------------------------------------------------------------이 부분부터 시작하자 !!! -----------------
    public void targetAcquisition()
    {

    }




    void Rotate()
    {
        Vector3 heading = target.transform.position - Camera.main.transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        Debug.Log("touched");

        if (Mathf.Abs(directionPos.x) > Mathf.Abs(directionPos.y))
        {
            
            if (directionPos.x > 0)
            {


                target.transform.Rotate(Vector3.up * 90, Space.World);

            }
            else
            {
                //Quaternion startRot = Quaternion.AngleAxis(90, Vector3.down);
                ////target.transform.rotation *= startRot;
                //target.transform.rotation = Quaternion.Euler(Vector3.up * 90);

                target.transform.Rotate(Vector3.down * 90, Space.World);
            }
        }
       
        else
        {
            if (directionPos.y > 0)
            {
                target.transform.Rotate(Vector3.left * 90, Space.World);
            }
            else
            {
                target.transform.Rotate(Vector3.right * 90, Space.World);
            }
        }
        target = currentTouch = null;
        return;

        //------------------------------------------------------
        if (Math.Abs(direction.x) < Math.Abs(direction.z))
        {
            Debug.Log("direction.x");
            if (direction.x < 0)
            {
                Debug.Log(direction);

                if (Mathf.Abs(directionPos.x) > Mathf.Abs(directionPos.y))
                {
                    if (directionPos.x > 0)
                    {
                        currentTouch.transform.parent.Rotate(Vector3.up * 90, Space.World);
                    }
                    else
                    {
                        currentTouch.transform.parent.Rotate(Vector3.down * 90, Space.World);
                    }
                }
                else
                {
                    if (directionPos.y > 0)
                    {
                        currentTouch.transform.parent.Rotate(Vector3.left * 90, Space.World);
                    }
                    else
                    {
                        currentTouch.transform.parent.Rotate(Vector3.right * 90, Space.World);
                    }
                }
            }

            else
            {
                Debug.Log(direction);
                if (Mathf.Abs(directionPos.x) > Mathf.Abs(directionPos.y))
                {

                    if (directionPos.x > 0)
                    {
                        currentTouch.transform.parent.Rotate(Vector3.down * 90, Space.World);

                    }

                    else
                    {
                        currentTouch.transform.parent.Rotate(Vector3.up * 90, Space.World);
                    }
                }
                else
                {
                    if (directionPos.y > 0)
                    {
                        currentTouch.transform.parent.Rotate(Vector3.right * 90, Time.deltaTime, Space.World);
                    }
                    else
                    {
                        currentTouch.transform.parent.Rotate(Vector3.left * 90, Time.deltaTime, Space.World);
                    }

                }
            }
        }
        //------------------------------------------------------
        else
        {
            Debug.Log("direction.z");
            if (direction.z < 0)
            {
                Debug.Log(direction);
                if (Mathf.Abs(directionPos.x) > Mathf.Abs(directionPos.y))
                {
                    if (directionPos.x > 0)
                        currentTouch.transform.parent.Rotate(Vector3.up * 90, Space.World);
                    else
                        currentTouch.transform.parent.Rotate(Vector3.down * 90, Space.World);
                }
                else
                {
                    if (directionPos.y > 0)
                        currentTouch.transform.parent.Rotate(Vector3.forward * 90, Space.World);
                    else
                        currentTouch.transform.parent.Rotate(Vector3.back * 90, Space.World);
                }
            }
            else
            {
                Debug.Log(direction);
                if (Mathf.Abs(directionPos.x) > Mathf.Abs(directionPos.y))
                {
                    if (directionPos.x > 0)
                        currentTouch.transform.parent.Rotate(Vector3.up * 90, Space.World);
                    else
                        currentTouch.transform.parent.Rotate(Vector3.down * 90, Space.World);
                }
                else
                {
                    if (directionPos.y > 0)
                        currentTouch.transform.parent.Rotate(Vector3.back * 90, Space.World);
                    else
                        currentTouch.transform.parent.Rotate(Vector3.forward * 90, Space.World);
                }
            }
        }
    }
}

