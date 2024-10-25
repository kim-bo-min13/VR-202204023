using UnityEngine;
using UnityEngine.UI;

public class ResultDirector : MonoBehaviour
{
    public Text pointText;
    
    void Start()
    {
        // 현재 점수를 가져와서 텍스트로 표시
        int point = PlayerPrefs.GetInt("Point", 0);
        if (point > 200)
        {
            pointText.text ="Your Point: " + point.ToString() + " point ... NotBad";
            if (point > 300)
            {
                pointText.text = "Your Point: " + point.ToString() + " point ...Good";
                if (point > 400)
                {
                    pointText.text = "Your Point: " + point.ToString() + " point ...Nice";
                    if (point >= 500)
                    {
                        pointText.text = "Your Point: " + point.ToString() + " point ...Excellent";
                    }
                }

            }
        }
        else
            pointText.text = "Your Point: " + point.ToString() + " point ... Bad";

    }
}