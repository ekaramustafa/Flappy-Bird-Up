using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoinManager
{

    private const string COIN = "Coin";


    public static int GetCoin()
    {
        return PlayerPrefs.GetInt(COIN);
    }

    public static void AddCoin(int value)
    {
        int currentCoinBalance = GetCoin();
        PlayerPrefs.SetInt(COIN,currentCoinBalance + value);
    }

    public static bool DeduceCoin(int value)
    {
        int currentCoinBalance = GetCoin();
        if (currentCoinBalance < value) return false;
        PlayerPrefs.SetInt(COIN, currentCoinBalance - value);
        return true;
    }
}
