using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AbilitySO", menuName = "ScriptableObjects/AbilityScriptableObject", order = 1)]
[RequireComponent(typeof(SpriteRenderer))]
public class AbilitySO : ScriptableObject
{

    public Transform transform;


}
