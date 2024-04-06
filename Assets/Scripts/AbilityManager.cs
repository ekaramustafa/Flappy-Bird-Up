using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    private static AbilityManager instance;

    public event EventHandler OnAbilityInteracted;
    public event EventHandler OnAbilityDestroyed;


    private float abilitiesSpawnTimer;
    [SerializeField] private float abilitiesSpawnTimerMax = 8f;

    public static AbilityManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public void HandleAbilitySpawning()
    {
        abilitiesSpawnTimer -= Time.deltaTime;
        if (abilitiesSpawnTimer < 0)
        {
            abilitiesSpawnTimer += abilitiesSpawnTimerMax;
            float heightEdgeLimit = 10f;
            float minHeight = -GameHandler.CAMERA_ORTHO_SIZE + heightEdgeLimit;
            float maxHeight = GameHandler.CAMERA_ORTHO_SIZE - heightEdgeLimit;
            float height = UnityEngine.Random.Range(minHeight, maxHeight);

            float minX = 50;
            float maxX = 100;
            float xPosition = UnityEngine.Random.Range(minX, maxX);
            CreateAbility(height, xPosition);
        }
    }

    private void CreateAbility(float yPosition, float xPosition)
    {
        if (IsCollidingAnything(yPosition, xPosition)) return;

        Transform abilityTransform = Instantiate(GameAssets.GetInstance().GetRandomAbility());
        abilityTransform.position = new Vector3(xPosition, yPosition);
        Ability ability = abilityTransform.GetComponent<Ability>();

    }

    private bool IsCollidingAnything(float yPosition, float xPosition)
    {
        float spawnRadiusOffset = 4f;
        Collider2D overlapCollider = Physics2D.OverlapCircle(new Vector2(xPosition, yPosition), spawnRadiusOffset);
        if (overlapCollider != null)
        {
            return true;
        }
        return false;
    }

}
