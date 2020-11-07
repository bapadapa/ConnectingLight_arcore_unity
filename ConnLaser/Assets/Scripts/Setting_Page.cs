using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Setting_Page : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //메인 화면으로 전환.
    public void Home_btn()
    {
        SceneManager.LoadScene("Main_Page"); 
    }
    

    public void btn_stage()
    {
        //클릭된 버튼명으로 Scene전환
        //stage Scene의 이름과 button의 이름이 동일해야함.

        string stage_name = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene(stage_name);

    }

}
