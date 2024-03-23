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
    
    [Header("Hat")]
    [SerializeField] private Transform hatLeftButton;
    [SerializeField] private Transform hatRightButton;
    [SerializeField] private Transform hatBuyButton;
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
        hatSelectedOption = 0;

        //Button 

        readyButton.GetComponent<Button_UI>().ClickFunc = () =>
        {
            Hide();
            GameHandler.state = GameHandler.State.WaitingToStart;
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
            BackOption(ref hatSelectedOption, bird.GetBirdSO().hatDatabase);
        };

        hatRightButton.GetComponent<Button_UI>().ClickFunc = () =>
        {
            NextOption(ref hatSelectedOption, bird.GetBirdSO().hatDatabase);
        };

        hatBuyButton.GetComponent<Button_UI>().ClickFunc = () =>
        {
            BuySelectedHat();
        };
        hatBuyButton.gameObject.SetActive(false);

        UpdateAll();

    }

    private void UpdateAll()
    {
        UpdateBird();
        UpdateCosmetics();
    }

    private void UpdateCosmetics()
    {
        UpdateHat();
    }

    private void UpdateHat()
    {
        HatDatabaseSO hatDatabase = bird.GetBirdSO().hatDatabase;
        hatDatabase.UpdateSO(bird, hatSelectedOption);
        if (!hatDatabase.IsSelectedOptionPurchased())
        {
            ShowHatBuy();
            readyButton.gameObject.SetActive(false);
        }
    }

    private void ShowHatBuy()
    {
        hatBuyButton.gameObject.SetActive(true);
    }

    private void BuySelectedHat()
    {
        HatDatabaseSO hatDatabase = bird.GetBirdSO().hatDatabase;
        int price = ((CosmeticSO)hatDatabase.GetScriptableObject(hatSelectedOption)).price;
        if (!CoinManager.DeduceCoin(price)) return;
        hatDatabase.BuySelectedOption(hatSelectedOption);
        hatBuyButton.gameObject.SetActive(false);
        readyButton.gameObject.SetActive(true);
        UpdateAll();
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
