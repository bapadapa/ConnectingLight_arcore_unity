using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPopUp : MonoBehaviour
{

    public void OnClickStart()
    {
        popSys.Instance.openPopUp();
    }
    public void OnClickExit()
    {
        Application.Quit();
    }

}
