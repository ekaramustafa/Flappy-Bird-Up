using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CosmeticDatabaseSO", menuName = "ScriptableObjects/CosmeticDatabase")]

public class CosmeticDatabaseSO : DatabaseSO
{

    public override void UpdateSO(Bird bird, int selectedOption)
    {
        Transform transform = bird.GetHatHolderTransform();
        CosmeticSO cosmeticSO = (CosmeticSO)GetScriptableObject(selectedOption);
        transform.GetComponent<SpriteRenderer>().sprite = cosmeticSO.cosmetic;
        transform.localPosition = new Vector3(cosmeticSO.xoffset, cosmeticSO.yoffset, 0);
        Quaternion newRotation = Quaternion.Euler(0f, 0f, cosmeticSO.zRotation);
        transform.localRotation = newRotation;
    }
}
