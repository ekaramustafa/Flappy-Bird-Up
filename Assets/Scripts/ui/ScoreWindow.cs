using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highestScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI coinText;
    // Start is called before the first frame update

    private void Awake()
    {
    }
    void Start()
    {
        highestScoreText.SetText("HIGHEST: " + ScoreManager.GetHighestScore().ToString());
        coinText.SetText(CoinManager.GetCoin().ToString());
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.SetText(Level.GetInstance().GetPipePassed().ToString());
        coinText.SetText(CoinManager.GetCoin().ToString());
    }
}
