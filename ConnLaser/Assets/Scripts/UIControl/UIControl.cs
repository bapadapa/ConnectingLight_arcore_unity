using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


//UI 상호작용.
public class UIControl : MonoBehaviour
{
    private string thisScnen;
    

    void Start()
    {

    }


    //------------UI 껏다 켰다하기.-----------------

    public void OnClickStart()
    {
        popSys.Instance.openPopUp();
        //필요하면 사용할것.
        //() =>{Debug.Log("OnClick Oaky!");} , () =>{Debug.Log("OnClick cancel!");}
    }

    //------------UI 껏다 켰다하기.-----------------

    //Exit 선택 어플리케이션 종료
    public void OnClickExit()
    {
        Application.Quit();
    }

    //재시작 선택 어플리케이션 종료
    public void btn_Restart()
    {
        thisScnen = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisScnen);
    }    

    public void OnClickStage()
    {
        //클릭된 버튼명으로 Scene전환
        //stage Scene의 이름과 button의 이름이 동일해야함.
        string stage_name = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene(stage_name);
    }

    public void btn_NextStage()
    {
        string nextStage = SceneManager.GetActiveScene().name;
        int stageNum;
        nextStage= nextStage.Substring(0, nextStage.Length - 1)+(int.Parse(nextStage.Substring(nextStage.Length - 1))+1);
        SceneManager.LoadScene(nextStage);
        
    }
   


}
