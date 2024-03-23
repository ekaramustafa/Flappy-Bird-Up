using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HatDatabaseSO", menuName = "ScriptableObjects/HatDatabase")]

public class HatDatabaseSO : DatabaseSO
{
    public override void UpdateSO(Bird bird, int selectedOption)
    {
        Transform transform = bird.GetHatHolderTransform();
        CosmeticSO cosmeticSO = (CosmeticSO)GetScriptableObject(selectedOption);
        transform.GetComponent<SpriteRenderer>().sprite = cosmeticSO.cosmetic;
        transform.localPosition = new Vector3(cosmeticSO.xPos, cosmeticSO.yPos, 0);
        Quaternion newRotation = Quaternion.Euler(0f, 0f, cosmeticSO.zRotation);
        transform.localRotation = newRotation;
    }
}
