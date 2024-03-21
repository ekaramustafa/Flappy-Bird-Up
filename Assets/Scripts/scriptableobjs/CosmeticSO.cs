using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BirdCosmeticSO", menuName = "ScriptableObjects/BirdCosmetic")]
public class CosmeticSO : ScriptableObject
{
    public Sprite cosmetic;
    public float xoffset;
    public float yoffset;
    public float zRotation;

   
}
