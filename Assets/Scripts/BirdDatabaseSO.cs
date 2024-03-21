using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BirdDatabaseSO", menuName = "ScriptableObjects/BirdDatabaseSO")]
public class BirdDatabaseSO : ScriptableObject
{
    public BirdSO[] birds;
    public int BirdCount
    {
        get
        {
            return birds.Length;
        }
    }


    public BirdSO GetBird(int index)
    {
        return birds[index];
    }

}
