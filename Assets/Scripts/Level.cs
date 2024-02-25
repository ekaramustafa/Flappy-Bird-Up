using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private const float CAMERA_ORTHO_SIZE = 50f;

    private const float PIPE_BODY_WIDTH = 7.8f;
    private const float PIPE_HEAD_HEIGHT = 3.75f;
    private const float PIPE_DESTROY_X_POSITION = -100f;
    private const float PIPE_SPAWN_X_POSITION = 100f;

    [SerializeField]
    public const float pipeMoveSpeed = 30f;

    private List<Pipe> pipeList;


    private float pipeSpawnTimer;
    private float pipeSpawnTimerMax;
    private float gapSize;
    private int pipesSpawned;

    public enum Difficutly
    {
        Easy,
        Medium,
        Hard,
        Impossible
    }

    private void Awake()
    {
        pipeList = new List<Pipe>();
        pipeSpawnTimerMax = 1f;
        SetDifficulty(Difficutly.Easy); //set the gapSize
        pipesSpawned = 0;
    }
    private void Start()
    {

    }

    private void Update()
    {
        HandlePipeMovement();
        HandlePipeSwapning();
    }

    private void HandlePipeSwapning()
    {
        pipeSpawnTimer -= Time.deltaTime;
        if(pipeSpawnTimer < 0)
        {
            pipeSpawnTimer += pipeSpawnTimerMax;
            
            float heightEdgeLimit = 10f;
            float minHeight = gapSize * 0.5f + heightEdgeLimit;
            float maxHeight = CAMERA_ORTHO_SIZE * 2f - gapSize*0.5f - heightEdgeLimit;
            float height = UnityEngine.Random.Range(minHeight, maxHeight);
            CreateGapPipes(height, gapSize, PIPE_SPAWN_X_POSITION);
            pipesSpawned++;
            SetDifficulty(GetDifficutly());
        }
    }

    private void HandlePipeMovement()
    {
        //In java this would blow the code
        for(int i = 0; i < pipeList.Count; i++)
        {
            Pipe pipe = pipeList[i];
            pipe.Move();
            //The pipe is out of the window
            if(pipe.GetXPosition() < PIPE_DESTROY_X_POSITION)
            {
                pipe.DestorySelf();
                pipeList.Remove(pipe);
                i--;
            }
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
        CreatePipe(gapY - gapSize * 0.5f, xPosition, true);
        CreatePipe(CAMERA_ORTHO_SIZE * 2f - gapY - gapSize * 0.5f, xPosition, false);

    }

    private void CreatePipe(float height, float xPosition, bool createBottom)
    {

        //setup for pipe head
        Transform pipeHead = Instantiate(GameAssets.GetInstance().pfPipeHead);
        float pipeHeadYPosition;
        if (createBottom)
        {
            pipeHeadYPosition = -CAMERA_ORTHO_SIZE + height - PIPE_HEAD_HEIGHT * 0.5f;
        }
        else
        {
            pipeHeadYPosition = +CAMERA_ORTHO_SIZE - height + PIPE_HEAD_HEIGHT * 0.5f;
        }

        pipeHead.position = new Vector3(xPosition, pipeHeadYPosition);

        //setup for pipe body
        Transform pipeBody = Instantiate(GameAssets.GetInstance().pfPipeBody);
        float pipeBodyYPosition;
        if (createBottom)
        {
            pipeHeadYPosition = -CAMERA_ORTHO_SIZE;
        }
        else
        {
            pipeHeadYPosition = +CAMERA_ORTHO_SIZE;
            pipeBody.localScale = new Vector3(1, -1, 1);
        }
        pipeBody.position = new Vector3(xPosition, pipeHeadYPosition);

        SpriteRenderer pipeBodySpriteRendered = pipeBody.GetComponent<SpriteRenderer>();
        pipeBodySpriteRendered.size = new Vector2(PIPE_BODY_WIDTH, height);

        BoxCollider2D pipeBodyBoxCollider = pipeBody.GetComponent<BoxCollider2D>();
        pipeBodyBoxCollider.size = new Vector2(PIPE_BODY_WIDTH, height);
        pipeBodyBoxCollider.offset = new Vector2(0f, height * 0.5f);

        Pipe pipe = new Pipe(pipeHead, pipeBody);
        pipeList.Add(pipe);

    }


    /*
     * Represents a single Pipe
     */
    private class Pipe{

        private Transform pipeHeadTransform;
        private Transform pipeBodyTransform;
 

        public Pipe(Transform pipeHeadTransform, Transform pipeBodyTransform)
        {
            this.pipeHeadTransform = pipeHeadTransform;
            this.pipeBodyTransform = pipeBodyTransform;
        }


        public void Move()
        {
            pipeHeadTransform.position += new Vector3(-1, 0, 0) * pipeMoveSpeed * Time.deltaTime;
            pipeBodyTransform.position += new Vector3(-1, 0, 0) * pipeMoveSpeed * Time.deltaTime;

        }

        public float GetXPosition()
        {
            return pipeHeadTransform.transform.position.x;
        }

        public void DestorySelf()
        {
            Destroy(pipeHeadTransform.gameObject);
            Destroy(pipeBodyTransform.gameObject);
        }
    }
}
