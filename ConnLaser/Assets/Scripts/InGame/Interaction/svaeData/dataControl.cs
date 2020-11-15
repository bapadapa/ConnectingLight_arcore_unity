using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dataControl : MonoBehaviour
{
    public void saveData(string stage,int stageNum ,float score)
    {
        PlayerPrefs.SetInt("Complete", stageNum);
        PlayerPrefs.SetFloat(stage + "score", score);
        PlayerPrefs.Save();
    }

    public void loadData()
    {
        loadData(GameObject.Find("bestScore").GetComponent<Text>());
    }

    public void loadData(Text bestscore)
    {

        string curStage = SceneManager.GetActiveScene().name;
        if (0 < PlayerPrefs.GetFloat(curStage + "score"))
        {            
            bestscore.text = "최고기록 : " + PlayerPrefs.GetFloat(curStage + "score").ToString("N2") + " 초";
        }
    }

    public void clearData()
    {
        PlayerPrefs.DeleteAll();
    }





}
