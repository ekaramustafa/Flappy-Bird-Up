using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HatDatabaseSO", menuName = "ScriptableObjects/HatDatabase")]

public class HatDatabaseSO : DatabaseSO
{
    private List<CosmeticSO> purchasedCosmetics = new List<CosmeticSO>();
    private List<CosmeticSO> unpurchasedCosmetics = new List<CosmeticSO>();

    private bool isSelectedOptionPurchased = true;
    public override int Count
    {
        get
        {
            purchasedCosmetics.Clear();
            unpurchasedCosmetics.Clear();
            foreach (ScriptableObject so in scriptableObjects)
            {
                CosmeticSO cosmeticSO = (CosmeticSO)so;
                if (cosmeticSO.isPurchased)
                {
                    purchasedCosmetics.Add(cosmeticSO);
                }
                else
                {
                    unpurchasedCosmetics.Add(cosmeticSO);
                }
            }
            List<CosmeticSO> rearrangedCosmetics = new List<CosmeticSO>();
            rearrangedCosmetics.AddRange(purchasedCosmetics);
            rearrangedCosmetics.AddRange(unpurchasedCosmetics);
            scriptableObjects = rearrangedCosmetics.ToArray();

            return scriptableObjects.Length;
        }
    }
    public override void UpdateSO(Bird bird, int selectedOption)
    {
        Transform transform = bird.GetHatHolderTransform();
        CosmeticSO cosmeticSO = (CosmeticSO)GetScriptableObject(selectedOption);
        SpriteRenderer spriteRenderer = transform.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cosmeticSO.cosmetic;
        
        isSelectedOptionPurchased = cosmeticSO.isPurchased;
        Color spriteColor = spriteRenderer.color;
        if (!cosmeticSO.isPurchased)
        {
            spriteColor.a = 0.6f;
        }
        else
        {
            spriteColor.a = 1f;
        }
        spriteRenderer.color = spriteColor;

        transform.localPosition = new Vector3(cosmeticSO.xPos, cosmeticSO.yPos, 0);
        Quaternion newRotation = Quaternion.Euler(0f, 0f, cosmeticSO.zRotation);
        transform.localRotation = newRotation;
    }

    public bool IsSelectedOptionPurchased()
    {
        return isSelectedOptionPurchased;
    }


    public void BuySelectedOption(int index)
    {
        CosmeticSO cosmeticSO = (CosmeticSO)GetScriptableObject(index);
        cosmeticSO.isPurchased = true;
    }
}
