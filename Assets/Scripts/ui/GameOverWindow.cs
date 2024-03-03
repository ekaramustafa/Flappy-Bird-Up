using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private Transform retryButton;
    [SerializeField]
    private Transform menuButton;
    [SerializeField]
    private Bird bird;


    private void Awake()
    {
        retryButton.GetComponent<Button_UI>().ClickFunc = () => {
            Loader.Load(Scene.GameScene);

        };

        menuButton.GetComponent<Button_UI>().ClickFunc = () => {
            Loader.Load(Scene.MainMenu);

        };
    }

    private void Start()
    {
        bird.OnDied += Bird_OnDied;
        Hide();
    }

    private void Bird_OnDied(object sender, System.EventArgs e)
    {
        scoreText.SetText(Level.GetInstance().GetPipePassed().ToString());
        Show();
    }


    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    } 
}
