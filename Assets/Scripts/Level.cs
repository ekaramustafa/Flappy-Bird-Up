using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.VisualScripting;

public class Level : MonoBehaviour
{

    /* TO-DOs
    Refactor the input system
    */
    private static Level instance;

    private const float CAMERA_ORTHO_SIZE = 50f;

    private const float PIPE_BODY_WIDTH = 7.8f;
    private const float PIPE_HEAD_HEIGHT = 3.75f;
    private const float PIPE_DESTROY_X_POSITION = -100f;
    private const float PIPE_SPAWN_X_POSITION = 100f;


    [SerializeField]
    private Bird bird;

    private float pipeSpawnTimer;
    [SerializeField] private float pipeSpawnTimerMax = 1f;
    private float gapSize;
    private int pipesSpawned;


    private float abilitiesSpawnTimer;
    [SerializeField] private float abilitiesSpawnTimerMax = 8f;


    private float coinSpawnTimer;
    [SerializeField] private float coinSpawnTimerMax = 8f;



    public enum Difficutly
    {
        Easy,
        Medium,
        Hard,
        Impossible
    }


    private void Awake()
    {
        SetDifficulty(Difficutly.Easy); //set the gapSize
        pipesSpawned = 0;
        instance = this;
        GameHandler.state = GameHandler.State.WaitingToStart;

        abilitiesSpawnTimer = abilitiesSpawnTimerMax;
        coinSpawnTimer = coinSpawnTimerMax;

    }
    private void Start()
    {
        bird.OnDied += Bird_OnDied;

    }

    private void Bird_OnDied(object sender, EventArgs e)
    {
        ScoreManager.SaveHighestScore(Pipe.PIPES_PASSED_COUNT);
    }

    public static Level GetInstance()
    {
        return instance;
    }
    private void Update()
    {
        if (GameHandler.state == GameHandler.State.Playing)
        {
            HandlePipeSwapning();
            HandleAbilitySpawning();
            HandleCoinSpawning();
        }

    }

    private void HandleCoinSpawning()
    {
        coinSpawnTimer -= Time.deltaTime;
        if (coinSpawnTimer < 0)
        {
            coinSpawnTimer += coinSpawnTimerMax;
            float heightEdgeLimit = 10f;
            float minHeight = -CAMERA_ORTHO_SIZE + heightEdgeLimit;
            float maxHeight = CAMERA_ORTHO_SIZE - heightEdgeLimit;
            float height = UnityEngine.Random.Range(minHeight, maxHeight);

            float minX = 50;
            float maxX = 100;
            float xPosition = UnityEngine.Random.Range(minX, maxX);
            CreateCoin(height, xPosition);
        }
    }

    private void CreateCoin(float yPosition, float xPosition)
    {
        if (IsCollidingAnything(yPosition, xPosition)) return;
        Transform coinTransform = Instantiate(GameAssets.GetInstance().GetRandomCoin());
        coinTransform.position = new Vector3(xPosition, yPosition);
        Coin coin = coinTransform.GetComponent<Coin>();
    }

    private void HandleAbilitySpawning()
    {
        abilitiesSpawnTimer -= Time.deltaTime;
        if (abilitiesSpawnTimer < 0)
        {
            abilitiesSpawnTimer += abilitiesSpawnTimerMax;
            float heightEdgeLimit = 10f;
            float minHeight = -CAMERA_ORTHO_SIZE + heightEdgeLimit;
            float maxHeight = CAMERA_ORTHO_SIZE - heightEdgeLimit;
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

    private void HandlePipeSwapning()
    {
        pipeSpawnTimer -= Time.deltaTime;
        if (pipeSpawnTimer < 0)
        {
            pipeSpawnTimer += pipeSpawnTimerMax;

            float heightEdgeLimit = 10f;
            float minHeight = gapSize * 0.5f + heightEdgeLimit;
            float maxHeight = CAMERA_ORTHO_SIZE * 2f - gapSize * 0.5f - heightEdgeLimit;
            float height = UnityEngine.Random.Range(minHeight, maxHeight);
            CreateGapPipes(height, gapSize, PIPE_SPAWN_X_POSITION);
            pipesSpawned++;
            SetDifficulty(GetDifficutly());
        }
    }


    private void SetDifficulty(Difficutly difficutly)
    {
        switch (difficutly)
        {
            case Difficutly.Easy:
                gapSize = 50f;
                break;
            case Difficutly.Medium:
                gapSize = 40f;
                break;
            case Difficutly.Hard:
                gapSize = 30f;
                break;
            case Difficutly.Impossible:
                gapSize = 20f;
                break;
        }
    }
    private Difficutly GetDifficutly()
    {
        if (pipesSpawned >= 60) return Difficutly.Impossible;
        if (pipesSpawned >= 40) return Difficutly.Hard;
        if (pipesSpawned >= 20) return Difficutly.Medium;
        return Difficutly.Easy;
    }

    private void CreateGapPipes(float gapY, float gapSize, float xPosition)
    {
        PipeSO pipeSO = GameAssets.GetInstance().GetRandomPipeSO();
        CreatePipe(gapY - gapSize * 0.5f, xPosition, true, pipeSO);
        CreatePipe(CAMERA_ORTHO_SIZE * 2f - gapY - gapSize * 0.5f, xPosition, false, pipeSO);

    }

    private void CreatePipe(float height, float xPosition, bool isBottom,PipeSO pipeSO)
    {

        //setup for pipe body
        Transform pipeBody = Instantiate(GameAssets.GetInstance().pfPipeBody);
        float pipeBodyYPosition;
        if (isBottom)
        {
            pipeBodyYPosition = -CAMERA_ORTHO_SIZE;
        }
        else
        {
            pipeBodyYPosition = +CAMERA_ORTHO_SIZE;
            pipeBody.localScale = new Vector3(1, -1, 1);
        }
        if (isBottom)
        {
            pipeBody.position = new Vector3(xPosition, pipeBodyYPosition - pipeSO.movingRange);
        }
        else
        {
            pipeBody.position = new Vector3(xPosition, pipeBodyYPosition + pipeSO.movingRange);
        }

        SpriteRenderer pipeBodySpriteRendered = pipeBody.GetComponent<SpriteRenderer>();
        pipeBodySpriteRendered.size = new Vector2(PIPE_BODY_WIDTH, height + pipeSO.movingRange);

        BoxCollider2D pipeBodyBoxCollider = pipeBody.GetComponent<BoxCollider2D>();
        pipeBodyBoxCollider.size = new Vector2(PIPE_BODY_WIDTH, height + pipeSO.movingRange);
        pipeBodyBoxCollider.offset = new Vector2(0f, height * 0.5f);

        //setup for pipe head
        Transform pipeHead = Instantiate(GameAssets.GetInstance().pfPipeHead);
        float pipeHeadYPosition;
        if (isBottom)
        {
            pipeHeadYPosition = -CAMERA_ORTHO_SIZE + height - PIPE_HEAD_HEIGHT * 0.5f;
        }
        else
        {
            pipeHeadYPosition = +CAMERA_ORTHO_SIZE - height + PIPE_HEAD_HEIGHT * 0.5f;
        }

        pipeHead.position = new Vector3(xPosition, pipeHeadYPosition);
        //The order matters
        pipeBody.GetComponent<Pipe>().SetPipeSO(pipeSO);
        pipeBody.GetComponent<Pipe>().SetHeadTransform(pipeHead);
        pipeBody.GetComponent<Pipe>().setIsBottom(isBottom);

        Pipe pipe = pipeBody.GetComponent<Pipe>();

    }

    //make the spawning faster/slower
    public void ScaleSpawnerTimers(float scale)
    {
        abilitiesSpawnTimerMax = abilitiesSpawnTimerMax / scale;
        pipeSpawnTimerMax = pipeSpawnTimerMax / scale;
        Debug.Log("The value of pipeSpawnTimerMax : " + pipeSpawnTimerMax);
    }
}

