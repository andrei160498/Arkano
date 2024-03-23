using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreManager : MonoBehaviour
{
    [SerializeField] Text BestscoreText;
    [SerializeField] Text ScoreText;

    public static float score;
    int bestscore;

    void Update()
    {
        bestscore = (int)score;
        ScoreText.text = "" + bestscore.ToString();

        if (!PlayerPrefs.HasKey("score") || PlayerPrefs.GetInt("score") <= bestscore)
        {
            PlayerPrefs.SetInt("score", bestscore);
        }
        BestscoreText.text = "" + PlayerPrefs.GetInt("score").ToString();
    }
}

