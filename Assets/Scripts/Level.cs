using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Level : MonoBehaviour
{

    /* TO-DOs
    Refactor the input system
    Refactor the state system to have one game state
    */
    private static Level instance;

    private const float CAMERA_ORTHO_SIZE = 50f;

    private const float PIPE_BODY_WIDTH = 7.8f;
    private const float PIPE_HEAD_HEIGHT = 3.75f;
    private const float PIPE_DESTROY_X_POSITION = -100f;
    private const float PIPE_SPAWN_X_POSITION = 100f;
    private const float BIRD_X_POSITION = 0f;

    [SerializeField]
    public const float objectsMoveSpeed = 30f;
    [SerializeField]
    private Bird bird;

    private List<Pipe> pipeList;
    private int pipesPassedCount;

    private float pipeSpawnTimer;
    private float pipeSpawnTimerMax;
    private float gapSize;
    private int pipesSpawned;


    private List<SpecialAbility> abililitesList;


    private State state;

    public enum Difficutly
    {
        Easy,
        Medium,
        Hard,
        Impossible
    }

    private enum State
    {
        WaitingToStart,
        Playing,
        BirdDead
    }

    private void Awake()
    {
        pipeList = new List<Pipe>();
        pipeSpawnTimerMax = 1f;
        SetDifficulty(Difficutly.Easy); //set the gapSize
        pipesSpawned = 0;
        pipesPassedCount = 0;
        instance = this;
        state = State.WaitingToStart;
        abililitesList = new List<SpecialAbility>();
    }
    private void Start()
    {
        bird.OnDied += Bird_OnDied;
        bird.OnStartedPlaying += Bird_OnstartedPlaying;

        
        HandleAbilitySpawning();
     
    }


    private void Bird_OnstartedPlaying(object sender, EventArgs e)
    {
        state = State.Playing;
    }

    private void Bird_OnDied(object sender, EventArgs e)
    {
        state = State.BirdDead;
   
    }

    public static Level GetInstance()
    {
        return instance;
    }
    private void Update()
    {
        if(state == State.Playing)
        {
            HandlePipeMovement();
            HandlePipeSwapning();

            HandleAbilityMovement();
        }

    }

    private void HandleAbilityMovement()
    {
        foreach (SpecialAbility specialAbility in abililitesList)
        {
            specialAbility.Move();
        }
    }

    private void HandleAbilitySpawning()
    {
        CreateSpecialAbility();
    }

    private void CreateSpecialAbility()
    {
        Transform abilityTransform = Instantiate(GameAssets.GetInstance().pfPipeHead);
        abilityTransform.position = new Vector3(10,10);
        SpecialAbility specialAbility = new SpecialAbility(abilityTransform);
        abililitesList.Add(specialAbility);
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
            bool isToTheRightOfBird = pipe.GetXPosition() > BIRD_X_POSITION;
            pipe.Move();
            if(isToTheRightOfBird && pipe.GetXPosition() <= BIRD_X_POSITION && pipe.IsBottom())
            {
                //Pipe passed Bird
                pipesPassedCount++;
            }
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

    private void CreatePipe(float height, float xPosition, bool isBottom)
    {

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
        pipeBody.position = new Vector3(xPosition, pipeBodyYPosition);

        SpriteRenderer pipeBodySpriteRendered = pipeBody.GetComponent<SpriteRenderer>();
        pipeBodySpriteRendered.size = new Vector2(PIPE_BODY_WIDTH, height);

        BoxCollider2D pipeBodyBoxCollider = pipeBody.GetComponent<BoxCollider2D>();
        pipeBodyBoxCollider.size = new Vector2(PIPE_BODY_WIDTH, height);
        pipeBodyBoxCollider.offset = new Vector2(0f, height * 0.5f);

        Pipe pipe = new Pipe(pipeHead, pipeBody,isBottom);
        pipeList.Add(pipe);

    }

    public int GetPipePassed()
    {
        return pipesPassedCount;
    }


    private class SpecialAbility
    {
        private Transform transform;

        public SpecialAbility(Transform transform)
        {
            this.transform = transform;
        }

        public void Move()
        {
            transform.position += new Vector3(-1, 0, 0) * objectsMoveSpeed * Time.deltaTime;

        }


    }

    /*
     * Represents a single Pipe
     */
    private class Pipe{

        private Transform pipeHeadTransform;
        private Transform pipeBodyTransform;
        private bool createBottom;
 

        public Pipe(Transform pipeHeadTransform, Transform pipeBodyTransform, bool isBottom)
        {
            this.pipeHeadTransform = pipeHeadTransform;
            this.pipeBodyTransform = pipeBodyTransform;
            this.createBottom = isBottom;
        }


        public void Move()
        {
            pipeHeadTransform.position += new Vector3(-1, 0, 0) * objectsMoveSpeed * Time.deltaTime;
            pipeBodyTransform.position += new Vector3(-1, 0, 0) * objectsMoveSpeed * Time.deltaTime;

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

        public bool IsBottom()
        {
            return createBottom;
        }
    }
}
