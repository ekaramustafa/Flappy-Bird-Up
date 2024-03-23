using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsWindow : MonoBehaviour
{
    private const string BIRD_SELECTED_OPTION = "BirdSelectedOption";
    private const string HAT_SELECTED_OPTION = "HatSelectedOption";
    [SerializeField] private Bird bird;
    [SerializeField] private Transform readyButton;
    [Header("Bird Selecting")]
    [SerializeField] private Transform birdLeftButton;
    [SerializeField] private Transform birdRightButton;
    [SerializeField] private BirdDatabaseSO birdDatabase;
    [Header("Cosmetics")]
    [SerializeField] private CosmeticDatabaseSO hatDatabase;
    [SerializeField] private Transform hatLeftButton;
    [SerializeField] private Transform hatRightButton;
    [SerializeField] private Transform hatHolder;
    // Start is called before the first frame update
    private int birdSelectedOption;
    private int hatSelectedOption;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(BIRD_SELECTED_OPTION))
        {
            birdSelectedOption = PlayerPrefs.GetInt(BIRD_SELECTED_OPTION);
        }
        else
        {
            birdSelectedOption = 0;
        }

        if (PlayerPrefs.HasKey(HAT_SELECTED_OPTION))
        {
            hatSelectedOption = PlayerPrefs.GetInt(HAT_SELECTED_OPTION);
        }
        else
        {
            hatSelectedOption = 0;
        }


        //Button 

        readyButton.GetComponent<Button_UI>().ClickFunc = () =>
        {
            Hide();
            GameHandler.state = GameHandler.State.WaitingToStart;
            PlayerPrefs.SetInt(BIRD_SELECTED_OPTION, birdSelectedOption);
            PlayerPrefs.Save();
        };

        //Bird Selecting
        birdLeftButton.GetComponent<Button_UI>().ClickFunc = () =>
        {
            BackOption(ref birdSelectedOption, birdDatabase);
        };

        birdRightButton.GetComponent<Button_UI>().ClickFunc = () =>
        {
            NextOption(ref birdSelectedOption, birdDatabase);
        };

        //Hat Selecting
        hatLeftButton.GetComponent<Button_UI>().ClickFunc = () =>
        {
            BackOption(ref hatSelectedOption, hatDatabase);
        };

        hatRightButton.GetComponent<Button_UI>().ClickFunc = () =>
        {
            NextOption(ref hatSelectedOption, hatDatabase);
        };

        UpdateAll();

    }

    private void UpdateAll()
    {
        UpdateCosmetics();
        UpdateBird();
    }

    private void UpdateCosmetics()
    {
        UpdateHat();
    }

    private void UpdateHat()
    {
        hatDatabase.UpdateSO(bird, hatSelectedOption);
    }
    private void UpdateBird()
    {
        birdDatabase.UpdateSO(bird, birdSelectedOption);

    }

    public void NextOption(ref int selectedOption, DatabaseSO databaseSO)
    {
        selectedOption++;
        if (selectedOption >= databaseSO.Count)
        {
            selectedOption = 0;
        }

        UpdateAll();
    }

    public void BackOption(ref int selectedOption, DatabaseSO databaseSO)
    {
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption = databaseSO.Count - 1;
        }

        UpdateAll();
    }


    private void Hide()
    {
        gameObject.SetActive(false);
    }


}
