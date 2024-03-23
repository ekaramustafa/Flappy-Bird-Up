using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BirdDatabaseSO", menuName = "ScriptableObjects/BirdDatabase")]
public class BirdDatabaseSO : DatabaseSO
{
    public override void UpdateSO(Bird bird, int selectedOption)
    {

        BirdSO birdSO = (BirdSO)GetScriptableObject(selectedOption);
        bird.SetBirdSO(birdSO);
    }
}
