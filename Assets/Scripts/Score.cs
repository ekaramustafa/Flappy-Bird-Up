using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    private const string HIGHEST_SCORE = "HighestScore";


    public static void Start()
    {
        //ResetHeighestScore();
    }
    public static int GetHighestScore()
    {
        return PlayerPrefs.GetInt(HIGHEST_SCORE);
    }

    public static bool SaveHighestScore(int score)
    {
        int currentHighScore = GetHighestScore();
        if(currentHighScore < score)
        {
            PlayerPrefs.SetInt(HIGHEST_SCORE, score);
            PlayerPrefs.Save();
            return true;
        }
        return false;

    }


    public static void ResetHeighestScore()
    {
        PlayerPrefs.SetInt(HIGHEST_SCORE, 0);
        PlayerPrefs.Save();
    }

}
