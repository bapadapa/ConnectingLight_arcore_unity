﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class popSys : MonoBehaviour
{
    public GameObject popUp;
    Animator _animator;

    public Text _time, _rotate;


    public static popSys Instance { get; private set; }

    Action onClickOkay, onClickCancel;


    private void Awake()
    {
        Instance = this;
        _animator = popUp.GetComponent<Animator>();
    }
    private void Update()
    {

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("popUpClose"))
        {
            //에니메이션이 끝났나?
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                popUp.SetActive(false);
            }

        }
    }

    public void openPopUp()
    {
        //필요가 생기면 넣을것.
        //Action onClickOkay, Action onClickCancel
        //this.onClickOkay = onClickOkay;
        //this.onClickCancel = onClickCancel;
        popUp.SetActive(true);

    }
    public void openPopUp(string time, string rotate)
    {
        //필요가 생기면 넣을것.
        //Action onClickOkay, Action onClickCancel
        //this.onClickOkay = onClickOkay;
        //this.onClickCancel = onClickCancel;
        _time.text = "소요 시간 :" +time + " 초";
        _rotate.text = "회전 횟수 : "+rotate + " 회";
        popUp.SetActive(true);
    }

    //구현된 마지막 스테이지.
    public void openPopUp(string message)
    {
        //필요가 생기면 넣을것.
        //Action onClickOkay, Action onClickCancel
        //this.onClickOkay = onClickOkay;
        //this.onClickCancel = onClickCancel;       
        
        popUp.SetActive(true);
        _time.text = message;
    }
    public void OnClickOkay()
    {
        //if(onClickOkay != null){
        //    onClickOkay();
        //} 
        ClosePopUp();
    }
    public void OnClickCancel()
    {
        //if (onClickCancel!= null)
        //{
        //    onClickCancel();
        //}
        ClosePopUp();
    }
    public void ClosePopUp()
    {
        _animator.SetTrigger("close");

    }




}
