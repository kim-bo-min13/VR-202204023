using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    GameObject timerText;
    GameObject pointText;
    GameObject goalPointText;
    GameObject goalPointTexta;
    bool timeIncreased = false;
    bool timeIncreased1 = false;

    float time = 50.0f;
    int point = 0;
   
    GameObject generator;

    public void GetApple()
    {
        this.point += 10;
    }
    public void GetBomb()
    {
        this.point -= 5;
    }
  
    // Start is called before the first frame update
    void Start()
    {
        this.generator = GameObject.Find("ItemGenerator");
        this.timerText = GameObject.Find("Time");
        this.pointText = GameObject.Find("Point");
        this.goalPointText = GameObject.Find("GoalScore");
        this.goalPointTexta = GameObject.Find("GoalScorea");


    }

    // Update is called once per frame
    void Update()
    {
        this.time -= Time.deltaTime;
        if (this.time < 0)
        {
            this.time = 0;
            this.generator.GetComponent<ItemGenerator>().SetParameter(10000.0f, 0, 0);
            EndGame();
        }
        else if (0 <= this.time && this.time < 10)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.07f, -0.03f, 0);
        }
        else if (10 <= this.time && this.time < 20)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.1f, -0.06f, 2);//6
        }
        else if (20 <= this.time && this.time < 25)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.15f, -0.04f, 4);//4
        }
        else if (25 <= this.time && this.time < 30)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.2f, -0.03f, 3);//3
        }
        else if (30 <= this.time && this.time < 40)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.4f, -0.02f, 3);
        }
        else if (40 <= this.time && this.time < 50)
        {
            this.generator.GetComponent<ItemGenerator>().SetParameter(0.5f, -0.015f, 1);
        }
        this.timerText.GetComponent<Text>().text = this.time.ToString("F1");
        this.pointText.GetComponent<Text>().text = this.point.ToString() + " point";

        if (this.point > 200 && !timeIncreased)
        {
            time += 30.0f;
            timeIncreased = true;
            this.goalPointText.GetComponent<Text>().text = "GoalScore: 350point";
        }

        if (this.point > 350 && !timeIncreased1)
        {
            time += 10.0f;
            timeIncreased1 = true;
            this.goalPointText.GetComponent<Text>().text = "GoalScore: 500point";
        }
    }
    void EndGame()
    {
        SceneManager.LoadScene("ResultScene");
        PlayerPrefs.SetInt("Point", point);
    }
  
}
