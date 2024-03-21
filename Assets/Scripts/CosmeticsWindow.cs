using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsWindow : MonoBehaviour
{
    private const string SELECTED_OPTION = "SelectedOption";
    [SerializeField] private Transform readyButton;
    [SerializeField] private Transform leftButton;
    [SerializeField] private Transform rightButton;
    [SerializeField] private BirdDatabaseSO birdDatabase;
    [SerializeField] private Bird bird;
    // Start is called before the first frame update
    private int selectedOption;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(SELECTED_OPTION))
        {
            selectedOption = PlayerPrefs.GetInt(SELECTED_OPTION);
        }
        else
        {
            selectedOption = 0;
        }

        readyButton.GetComponent<Button_UI>().ClickFunc = () => {
            Hide();
            GameHandler.state = GameHandler.State.WaitingToStart;
            PlayerPrefs.SetInt(SELECTED_OPTION, selectedOption);
            PlayerPrefs.Save();
        };

        leftButton.GetComponent<Button_UI>().ClickFunc = () =>
        {
            BackOption();
        };

        rightButton.GetComponent<Button_UI>().ClickFunc = () =>
        {
            NextOption();
        };

        UpdateBird();

    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void NextOption()
    {
        selectedOption++;
        if (selectedOption >= birdDatabase.BirdCount)
        {
            selectedOption = 0;
        }

        UpdateBird();
    }

    public void BackOption()
    {
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption = birdDatabase.BirdCount - 1;
        }

        UpdateBird();
    }

    private void UpdateBird()
    {
        Debug.Log(selectedOption);
        BirdSO birdSO = birdDatabase.GetBird(selectedOption);
        bird.SetBirdSO(birdSO);
    }

}
