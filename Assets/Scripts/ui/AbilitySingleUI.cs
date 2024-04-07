using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySingleUI : MonoBehaviour
{
    [SerializeField] private Transform iconContainter;
    [SerializeField] private Transform iconTemplate;
    private int id;

    private float fadeDuration;


    public void SetAbility(Ability ability)
    {
        
        foreach (Transform child in iconContainter)
        {
            if (child != iconTemplate)
                Destroy(child);
        }

        iconTemplate.gameObject.SetActive(true);
        iconTemplate.GetComponent<Image>().sprite = ability.GetComponent<SpriteRenderer>().sprite;
        Debug.Log("Here");
        iconTemplate.GetComponent<Image>().color = ability.GetComponent<SpriteRenderer>().color;
        iconTemplate.rotation = ability.transform.rotation;
        id = ability.GetID();
        fadeDuration = ability.GetAbilityEffectTime();
        StartCoroutine(Fade(0f));
    }

    IEnumerator Fade(float targetAlpha)
    {
        float timer = 0f;
        float currentAlpha = 1f;
        Color color = iconTemplate.GetComponent<Image>().color;

        // Interpolate the alpha value over time
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            currentAlpha = Mathf.Lerp(1f, targetAlpha, timer / fadeDuration);
            color.a = currentAlpha;
            iconTemplate.GetComponent<Image>().color = color;
            yield return null;
        }

        currentAlpha = targetAlpha;
        color.a = currentAlpha;
        iconTemplate.GetComponent<Image>().color = color; // Ensure final alpha value
    }


    public int GetAbilityID()
    {
        return id;
    } 

    
}
