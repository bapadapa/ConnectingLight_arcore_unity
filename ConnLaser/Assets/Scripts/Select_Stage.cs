using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Select_Stage : MonoBehaviour
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

}
