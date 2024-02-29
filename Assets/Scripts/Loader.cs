using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class Loader
{

    private static Scene targetScene;
    public static void Load(Scene scene)
    {
       targetScene = scene;
       SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoadTargetScene()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
