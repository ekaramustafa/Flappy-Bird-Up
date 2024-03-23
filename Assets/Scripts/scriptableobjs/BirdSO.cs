using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BirdSO", menuName = "ScriptableObjects/Bird", order = 1)]
public class BirdSO : ScriptableObject
{

    public Sprite sprite;
    public RuntimeAnimatorController runtimeAnimatorController;
    public float radius;//for collider size
    public int mass;
    public float size;
    //public CosmeticDatabaseSO hatCosmetics; try to attach each bird to a set of cosmetics

}
