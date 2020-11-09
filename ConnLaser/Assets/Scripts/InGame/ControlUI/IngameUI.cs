using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class IngameUI : UIControl 
{
    public Canvas duringGame;
    public Canvas endGame;
    public Text _ToDebug;
    bool _isplay;
    public  Draw_Line DL;
    public SceneController sceneController;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("ControlCanvas Started");
        duringGame.enabled = true;
        endGame.enabled = false;
      //  sceneController = GameObject.Find("GameController").GetComponent<SceneController>();

        
    }   

    // Update is called once per frame
    void Update()
    {
        if(DL != null)
        {

            if (!DL._isPlaying)
                _isEnd();
        }
        //if(sceneController.touchedObj != null)
        //    _ToDebug.text = sceneController.touchedObj.name;



    }

   public void _isEnd()
    {
        endGame.enabled = true;
        duringGame.enabled = false;
        
        //Debug.Log("ControlCanvas Ended");

    }
    public void Home_btn()
    {
        SceneManager.LoadScene("Main_Page");

    }

}
