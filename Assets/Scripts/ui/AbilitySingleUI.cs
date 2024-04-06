using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI effectDuration;
    [SerializeField] private Transform iconContainter;
    [SerializeField] private Transform iconTemplate;

    public void SetAbility(Ability ability)
    {
        Transform iconTransform = Instantiate(iconTemplate, iconContainter);
        iconTransform.gameObject.SetActive(true);
        iconTransform.GetComponent<Image>().sprite = ability.GetComponent<SpriteRenderer>().sprite;
        effectDuration.text =  $"{ability.GetAbilityEffectTime()}";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
