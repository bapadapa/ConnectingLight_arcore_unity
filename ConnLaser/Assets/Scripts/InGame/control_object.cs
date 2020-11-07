using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_object : MonoBehaviour
{
    float speed = 200f;
    Camera _mainCam = null;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main;
        Debug.Log(_mainCam.name);
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
    }
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
    void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = GetClickedObject();
            if (target != null)
            {
                var step = speed * Time.deltaTime;
                Quaternion tmp = target.transform.rotation;
                print("전" + target.transform.rotation);
                tmp.y += 90f;
                print("후" + tmp);

                target.transform.rotation = Quaternion.RotateTowards(target.transform.rotation, tmp, step);
                print("hahaha");
                //Vector3 tmp = target.;
                //target.transform.rotation = Quaternion.RotateTowards(target.transform.rotation, target.transform.rotation, step);

            }
            else
                return;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            
        }
    }
}