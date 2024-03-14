using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highestScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    // Start is called before the first frame update

    private void Awake()
    {
    }
    void Start()
    {
        highestScoreText.SetText("HIGHEST: " + Score.GetHighestScore().ToString());
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.SetText(Level.GetInstance().GetPipePassed().ToString());
    }
}
