using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckClear : MonoBehaviour
{
    public Button[] stages = new Button[9];
    public Sprite clearImg, defaultImg;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main_Page")
        {
            if (true == PlayerPrefs.HasKey("Complete"))
            {
                for (int i = 0; i < stages.Length; i++)
                    stages[i].image.overrideSprite = defaultImg;
                for (int i = 0; i < PlayerPrefs.GetInt("Complete"); i++)
                    stages[i].image.overrideSprite = clearImg;
            }
        }
        else
        {
            dataControl dataControl = GetComponent<dataControl>();
            dataControl.loadData(GameObject.Find("bestScore").GetComponent<Text>());
        }
                

    }

}
