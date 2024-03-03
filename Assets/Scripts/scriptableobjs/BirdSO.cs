using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BirdSO", menuName = "ScriptableObjects/BirdScriptableObject", order = 1)]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BirdSO : ScriptableObject
{

    public Sprite spriteRenderer;
    public RuntimeAnimatorController runtimeAnimatorController;
    public int radius;//for collider size
    public int mass;

    
}
