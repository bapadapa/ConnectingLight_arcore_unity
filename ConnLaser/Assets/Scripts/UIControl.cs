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
     public  Canvas start, setting, stage;
    void Start()
    {
        start.enabled = true;
        setting.enabled = false;
        stage.enabled = false;
        //thisScnen = SceneManager.GetActiveScene().name;
    }


    //------------UI 껏다 켰다하기.-----------------

    //Setting창으로 화면 전환
    public void btn_Setting()
    {
        //SceneManager.LoadScene("Setting_Page");
        start.enabled = false;
        stage.enabled = false;
        setting.enabled = true;
        
    }   
    //Stage 선택 화면 전환
    public void btn_Stage()
    {
        // SceneManager.LoadScene("Select_Stage");
        start.enabled = false;
        setting.enabled = false;
        stage.enabled = true;
        
    }
    //메인 화면으로 전환.
    public void Home_btn()
    {
        // SceneManager.LoadScene("Main_Page");

        start.enabled = true;
        setting.enabled = false;
        stage.enabled = false;
    }

    //------------UI 껏다 켰다하기.-----------------

    //Exit 선택 어플리케이션 종료
    public void btn_Exit()
    {
        Application.Quit();
    }

    //재시작 선택 어플리케이션 종료
    public void btn_Restart()
    {
        thisScnen = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisScnen);
    }    

    public void btn_stageSelect()
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
        
        Debug.Log(nextStage);
    }


}
