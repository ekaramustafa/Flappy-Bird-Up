using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DatabaseSO", menuName = "ScriptableObjects/Database")]
public abstract class DatabaseSO : ScriptableObject
{
    public ScriptableObject[] scriptableObjects;

    public virtual int Count
    {
        get
        {
            return scriptableObjects.Length;
        }
    }

    public ScriptableObject GetScriptableObject(int index)
    {
        if(index > scriptableObjects.Length - 1)
        {
            return scriptableObjects[0];
        }
        else
        {
            return scriptableObjects[index];
        }
        
    }

    public abstract void UpdateSO(Bird bird,int selectedOption);

}
