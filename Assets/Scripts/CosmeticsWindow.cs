using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsWindow : MonoBehaviour
{

    [SerializeField] private Transform readyButton;
    [SerializeField] private Transform leftButton;
    [SerializeField] private Transform rightButton;
    // Start is called before the first frame update

    private void Awake()
    {
        readyButton.GetComponent<Button_UI>().ClickFunc = () => {
            Hide();
            GameHandler.state = GameHandler.State.WaitingToStart;
        };

        leftButton.GetComponent<Button_UI>().ClickFunc = () =>
        {

        };

        rightButton.GetComponent<Button_UI>().ClickFunc = () =>
        {

        };

    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
