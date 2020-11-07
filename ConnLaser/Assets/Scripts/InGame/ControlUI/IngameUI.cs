using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IngameUI : UIControl
{
    public Canvas duringGame;
    public Canvas endGame;

    bool _isplay;
    Draw_Line DL;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ControlCanvas Started");
        duringGame.enabled = true;
        endGame.enabled = false;

        DL = GameObject.Find("StartPoint").GetComponent<Draw_Line>();
    }   

    // Update is called once per frame
    void Update()
    {
        if (!DL._isPlaying)
            _isEnd();
    }

   public void _isEnd()
    {
        endGame.enabled = true;
        duringGame.enabled = false;
        
        Debug.Log("ControlCanvas Ended");

    }
    public void Home_btn()
    {
        SceneManager.LoadScene("Main_Page");

    }

}
