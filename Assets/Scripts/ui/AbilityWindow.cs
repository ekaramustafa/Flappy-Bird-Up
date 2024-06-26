using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AbilityWindow : MonoBehaviour
{

    [SerializeField] private Transform template;
    [SerializeField] private Transform container;


    private void Awake()
    {
        template.gameObject.SetActive(false);
    }

    private void Start()
    {
        AbilityManager.GetInstance().OnAbilityDestroyed += OnAbilityDestroyed;
        AbilityManager.GetInstance().OnAbilityInteracted += OnAbilityInteracted;

        
    }
    private void OnAbilityInteracted(object sender, AbilityManager.OnAbilityArgs e)
    {
        Transform abilityTransform = Instantiate(template, container);
        abilityTransform.gameObject.SetActive(true);
        abilityTransform.GetComponent<AbilitySingleUI>().SetAbility(e.ability);
    }

    private void OnAbilityDestroyed(object sender, AbilityManager.OnAbilityArgs e)
    {
        foreach (Transform child in container)
        {
            
            if (child.GetComponent<AbilitySingleUI>().GetAbilityID() == e.ability.GetID() && child != template)
            {
                Destroy(child.gameObject);
                return;
            }
            
        }
    }
}
