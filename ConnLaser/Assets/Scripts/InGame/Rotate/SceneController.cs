

using System.Collections.Generic;
using System.Collections;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using System;
using UnityEngine.UI;

#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class SceneController : MonoBehaviour
{
    public Camera FirstPersonCamera;
    public GameObject DetectedPlanePrefab;
    public GameObject ARAndroidPrefab;
    public GameObject SearchingForPlaneUI;
    private GameObject ARObject;
    private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();
    private bool m_IsQuitting = false;
    public int CurrentNumberOfGameObjects = 0;
    public int numberOfGameObjectsAllowed = 1;
    //For Pinch to Zoom
    float prevTouchDistance;
    float zoomSpeed = 0.2f;

    public IngameUI ingameUI;
    public GameObject touchedObj;
    public GameObject target;
    Vector2 startPos, endPos, directionPos;
    string strTarget;
    public Text textDebug;
    public bool _isRotatingMap = false;
    public void Update()
    {

        _UpdateApplicationLifecycle();
        _PlaneDetection();
        _InstantiateOnTouch();

       // textDebug.text = "ARobj = " + ARObject.name;
        //Debug.Log(ARObject.name);

    }

    private void _UpdateApplicationLifecycle()
    {
        // Exit the app when the 'back' button is pressed.
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Only allow the screen to sleep when not tracking.
        if (Session.Status != SessionStatus.Tracking)
        {
            const int lostTrackingSleepTimeout = 15;
            Screen.sleepTimeout = lostTrackingSleepTimeout;
        }
        else
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        if (m_IsQuitting)
        {
            return;
        }

        // Quit if ARCore was unable to connect and give Unity some time for the toast to appear.
        if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            _ShowAndroidToastMessage("Camera permission is needed to run this application.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
        else if (Session.Status.IsError())
        {
            _ShowAndroidToastMessage("ARCore encountered a problem connecting.  Please start the app again.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
    }
    private void _DoQuit()
    {
        Application.Quit();
    }
    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                    message, 0);
                toastObject.Call("show");
            }));
        }
    }
    public void _PlaneDetection()
    {
        // Hide snackbar when currently tracking at least one plane.
        Session.GetTrackables<DetectedPlane>(m_AllPlanes);
        bool showSearchingUI = true;
        for (int i = 0; i < m_AllPlanes.Count; i++)
        {
            if (m_AllPlanes[i].TrackingState == TrackingState.Tracking)
            {
                showSearchingUI = false;
                break;
            }
        }

        SearchingForPlaneUI.SetActive(showSearchingUI);

    }
    public void _InstantiateOnTouch()
    {
        Touch touch;
        touch = Input.GetTouch(0);
        _isRotatingMap = false;

        if (Input.touchCount != 0)
        {
            _SpawnARObject();
            SelecObj();
            //_PinchtoZoom();
            _RotateMap();           
            //_RotateObj();
        }
    }


    public void _PinchtoZoom()
    {
        if (Input.touchCount == 2)
        {


            //Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            //Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            //Find the magnitude of the vector(the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            //Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;


            float pinchAmount = deltaMagnitudeDiff * 0.02f * Time.deltaTime;
            ARObject.transform.localScale += new Vector3(pinchAmount, pinchAmount, pinchAmount);

        }
    }
    public void _RotateMap()
    {
        Touch touch;
        touch = Input.GetTouch(0);
        if (Input.touchCount == 2 && touch.phase == TouchPhase.Moved)
        {            
            ARObject.transform.Rotate(Vector3.up * 5f * Time.deltaTime * touch.deltaPosition.x, Space.Self);
            _isRotatingMap = true;
        }
    }

    //인식된 PLANE에 Prefeb 생성하기.
    public void _SpawnARObject()
    {
        Touch touch;
        touch = Input.GetTouch(0);
        //Debug.Log("touch count is " + Input.touchCount);
        TrackableHit hit;      // Raycast against the location the player touched to search for planes.
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
        TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (touch.phase == TouchPhase.Began)
        {
            // Debug.Log("Touch Began");
            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
            {
                if (CurrentNumberOfGameObjects < numberOfGameObjectsAllowed)
                {
                    //Debug.Log("Screen Touched");
                    Destroy(ARObject);
                    // Use hit pose and camera pose to check if hittest is from the
                    // back of the plane, if it is, no need to create the anchor.

                    if ((hit.Trackable is DetectedPlane) &&
                        Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                            hit.Pose.rotation * Vector3.up) < 0)
                    {
                        Debug.Log("Hit at back of the current DetectedPlane");
                    }
                    else
                    {
                        ARObject = Instantiate(ARAndroidPrefab, hit.Pose.position, hit.Pose.rotation);// Instantiate Andy model at the hit pose.
                        ARObject.transform.Rotate(0, 180.0f, 0, Space.Self);// Compensate for the hitPose rotation facing away from the raycast (i.e. camera).
                        GameObject StartPoint = ARObject.transform.Find("StartPoint").gameObject;
                        Vector3 startPosition = Vector3.zero;
                        startPosition.x += 2.5f; startPosition.z = -5.0f;
                        StartPoint.transform.localPosition =  startPosition;
                        Debug.Log("ARObject = " + ARObject);
                        Debug.Log("StartPoint = "+ StartPoint);
                        var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                        ARObject.transform.parent = anchor.transform;
                        CurrentNumberOfGameObjects = CurrentNumberOfGameObjects + 1;

                        //ARObj 생성 완료.
                        ingameUI = GameObject.Find("canvasGroup").GetComponent<IngameUI>();
                        ingameUI.DL = GameObject.Find("StartPoint").GetComponent<Draw_Line>();

                        
                    }

                }

            }

        }
        if(CurrentNumberOfGameObjects == numberOfGameObjectsAllowed)
            //지면 인식한 것 지우기.
            foreach (GameObject Temp in DetectedPlaneGenerator.instance.PLANES)
            {
                Temp.SetActive(false);
            }


    }

    //target 선택.
    GameObject CastRay()
    {
        GameObject target = null;
        RaycastHit hit;
        Ray ray = FirstPersonCamera.ScreenPointToRay(Input.GetTouch(0).position);
        //클릭 위치 근처에 오브젝트가 있나 ?
        if (Physics.Raycast(ray.origin, ray.direction * 30, out hit))
        {
            target = hit.collider.gameObject;

        };
        return target;
    }


    // 추가함..
    void SelecObj()
    {
        Touch touch;
        //터치가 들어오면.
        if (Input.touchCount != 0)
        {
            touch = Input.GetTouch(0);
            touchedObj = CastRay();

            if (touch.phase == TouchPhase.Began && touchedObj != null)
            {
                if (touchedObj.layer == LayerMask.NameToLayer("gameObj"))
                {
                    target = touchedObj.transform.parent.gameObject;
                    startPos = touch.position;
                    Debug.Log("IN");
                    Debug.Log(target.name);
                }
                Debug.Log(target.name);
            }

            if (touch.phase == TouchPhase.Ended && target.tag == "Mirror")
            {
                endPos = touch.position;
                directionPos = startPos - endPos;
                Rotate();
            }
        }
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
                //target.transform.Rotate(Vector3.back * 45  + Vector3.up*45, Space.World);
                target.transform.Rotate(Vector3.back * 90, Space.World);

            }
            else
            {
                //target.transform.Rotate(Vector3.forward * 45 + Vector3.down *45, Space.World);
                target.transform.Rotate(Vector3.forward * 90, Space.World);
            }
        }

        textDebug.text += "Position = " + target.transform.position.ToString() + "ratation = " + target.transform.rotation.ToString();
        target = touchedObj = null;
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
                        target.transform.parent.Rotate(Vector3.up * 90, Space.World);
                    }
                    else
                    {
                        target.transform.parent.Rotate(Vector3.down * 90, Space.World);
                    }
                }
                else
                {
                    if (directionPos.y > 0)
                    {
                        target.transform.parent.Rotate(Vector3.left * 90, Space.World);
                    }
                    else
                    {
                        target.transform.parent.Rotate(Vector3.right * 90, Space.World);
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
                        target.transform.parent.Rotate(Vector3.down * 90, Space.World);

                    }

                    else
                    {
                        target.transform.parent.Rotate(Vector3.up * 90, Space.World);
                    }
                }
                else
                {
                    if (directionPos.y > 0)
                    {
                        target.transform.parent.Rotate(Vector3.right * 90, Time.deltaTime, Space.World);
                    }
                    else
                    {
                        target.transform.parent.Rotate(Vector3.left * 90, Time.deltaTime, Space.World);
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
                        target.transform.parent.Rotate(Vector3.up * 90, Space.World);
                    else
                        target.transform.parent.Rotate(Vector3.down * 90, Space.World);
                }
                else
                {
                    if (directionPos.y > 0)
                        target.transform.parent.Rotate(Vector3.forward * 90, Space.World);
                    else
                        target.transform.parent.Rotate(Vector3.back * 90, Space.World);
                }
            }
            else
            {
                Debug.Log(direction);
                if (Mathf.Abs(directionPos.x) > Mathf.Abs(directionPos.y))
                {
                    if (directionPos.x > 0)
                        target.transform.parent.Rotate(Vector3.up * 90, Space.World);
                    else
                        target.transform.parent.Rotate(Vector3.down * 90, Space.World);
                }
                else
                {
                    if (directionPos.y > 0)
                        target.transform.parent.Rotate(Vector3.back * 90, Space.World);
                    else
                        target.transform.parent.Rotate(Vector3.forward * 90, Space.World);
                }
            }
        }
        target = touchedObj = null;
    }
}

