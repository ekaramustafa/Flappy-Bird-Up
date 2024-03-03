using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuWindow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform startButton;
    [SerializeField]
    private Transform settingsButton;
    [SerializeField]
    private Transform exitButton;
    private void Awake()
    {
        startButton.GetComponent<Button_UI>().ClickFunc = () => {
            Loader.Load(Scene.GameScene);
        };
        exitButton.GetComponent<Button_UI>().ClickFunc = () => {
            Application.Quit();
        };
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
