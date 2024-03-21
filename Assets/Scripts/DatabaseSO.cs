using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DatabaseSO", menuName = "ScriptableObjects/Database")]
public abstract class DatabaseSO : ScriptableObject
{
    public ScriptableObject[] scriptableObjects;

    public int Count
    {
        get
        {
            return scriptableObjects.Length;
        }
    }

    public ScriptableObject GetScriptableObject(int index)
    {
        return scriptableObjects[index];
    }

    public abstract void UpdateSO(Bird bird,int selectedOption);

}
