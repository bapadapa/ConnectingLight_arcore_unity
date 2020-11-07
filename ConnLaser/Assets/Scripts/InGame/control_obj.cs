using UnityEngine;
using System.Collections;
using System;
using System.Net;


public class control_obj : MonoBehaviour
{
    [SerializeField] private Vector3 FirstTouch;
    [SerializeField] private Vector3 LastTouch;

    public bool check = true;
    private Transform cameraRotate;
    Camera _mainCam = null;    

    /// 마우스가 다운된 오브젝트
    private GameObject target;
    
    void Start()
    {
        _mainCam = Camera.main;
        
    }

    // Update is called once per frame 
    void Update()
    {
        try{
      
           
            //마우스가 내려갔는지?
            if (Input.GetMouseButtonDown(0))
            {
                //클릭 시작지점 저장.
                FirstTouch = Input.mousePosition;

                //내려갔다.
                //타겟을 받아온다.
                target = GetClickedObject();

                //타겟이 나인가?
                
            }
            else if (Input.GetMouseButtonUp(0))
            {
                LastTouch = Input.mousePosition;

                //_mouseState = false;                
                Debug.Log("ft x,y,z = " + FirstTouch);
                Debug.Log("lt x,y,z = " + LastTouch);


                chAngle(FirstTouch, LastTouch);

                //Debug.Log("ft x = " + (FirstTouch.x - transform.position.x));
                //Debug.Log("lt x = " + (LastTouch.x- transform.position.x));




            }

            //마우스가 눌렸나?
            //스케일을 조절하여 클릭됬음을 확인한다.
            //if (_mouseState &&check)
            //{
            //    check = false;
            //    //눌렸다!
          
            //    StartCoroutine("delayTime");
            //}
        }
        catch(NullReferenceException e)
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

    private void chAngle(Vector3 ft, Vector3 lt)
    {
        float direction = 0;
        float x, y, z;
        char chAngle = ' ';

        x = Math.Abs(Math.Abs(FirstTouch.x) - Math.Abs(LastTouch.x));
        y = Math.Abs(Math.Abs(FirstTouch.y) - Math.Abs(LastTouch.y));
        z = Math.Abs(Math.Abs(FirstTouch.z) - Math.Abs(LastTouch.z));


        if (x < y)
        {
            if (y < z)
                chAngle = 'z';
            else
                chAngle = 'y';
        }
        else
        {
            if (x < z)
                chAngle = 'z';
            else
                chAngle = 'x';
        }
        
        switch (chAngle)
        {
            case 'x':
                direction = chdirection(Math.Abs(FirstTouch.x), Math.Abs(LastTouch.x));
                if (target.Equals(gameObject) && target.CompareTag("game_obj"))
                {
                    //있으면 마우스 정보를 바꾼다.
                    //_mouseState = true;
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 90 * direction, transform.eulerAngles.z);
                    //transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.forward.y + 90 * direction, transform.eulerAngles.z);
                    //transform.rotation = Quaternion.Euler(transform.forward.x, transform.forward.y + 90 * direction-45, transform.forward.z);
                    

                }
                break;

            case 'y':
                direction = chdirection(Math.Abs(FirstTouch.y), Math.Abs(LastTouch.y));
                if (target.Equals(gameObject) && target.CompareTag("game_obj"))
                {
                    //있으면 마우스 정보를 바꾼다.
                    //_mouseState = true;
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y , transform.eulerAngles.z - 90 * direction);
                }
                break;

            case 'z':
                direction = chdirection(Math.Abs(FirstTouch.z), Math.Abs(LastTouch.z));
                if (target.Equals(gameObject) && target.CompareTag("game_obj"))
                {
                    //있으면 마우스 정보를 바꾼다.
                    //_mouseState = true;
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x + 90 * direction, transform.eulerAngles.y, transform.eulerAngles.z);
                }
                break;
        }

    }
    // mouseDown 좌표와 mouseUp 좌표 비교를 하여 회전 방향 지정.
    private float chdirection(float ft , float lt)
    {
        if (Math.Abs(ft) - Math.Abs(lt) < 0)
            return -1;
        else if (Math.Abs(ft) - Math.Abs(lt) > 0)
            return  1;
        else
            return  0;
    }
    IEnumerator delayTime()
    {
        yield return new WaitForSeconds(0.1f);
        check = true;
    }
}