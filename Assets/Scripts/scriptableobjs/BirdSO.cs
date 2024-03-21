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
    [Header("Cosmetics")]
    public float hatOffsetX;
    public float hatOffsetY;
    public float hatRotationZ;

    
}
