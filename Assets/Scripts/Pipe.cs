using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{

    public static int PIPES_PASSED_COUNT = 0;

    private float verticalSpeed = 20f;
    private const float PIPE_DESTROY_X_POSITION = -100f;

    private Transform pipeHeadTransform;
    private bool isBottom;
    private float movingRange;
    private Vector3 initialHeadPosition;
    private float direction = -1f;

    private bool isPipeCounted;

    private PipeSO pipeSO;

    private void Awake()
    {
        movingRange = 10f;
        direction = -1f;
        isPipeCounted = false;
    }

    private void Update()
    {
        if (GameHandler.state == GameHandler.State.Playing)
        {
            Move();
        }
    }

    public void Move()
    {
        if (pipeHeadTransform == null) return;
        bool isToTheRightOfBird = GetXPosition() > GameHandler.BIRD_X_POSITION;
        VerticalMove();
        HorizantalMove();
        if (isToTheRightOfBird && GetXPosition() <= GameHandler.BIRD_X_POSITION && IsBottom() && !isPipeCounted)
        {
            //Pipe passed Bird
            isPipeCounted = true;
            PIPES_PASSED_COUNT++;
        }
        if (GetXPosition() < PIPE_DESTROY_X_POSITION)
        {
            DestroySelf();
        }
    }

    private void HorizantalMove()
    {
        pipeHeadTransform.position += new Vector3(-1, 0, 0) * GameHandler.OBJECTS_MOVING_SPEED * Time.deltaTime;
        transform.position += new Vector3(-1, 0, 0) * GameHandler.OBJECTS_MOVING_SPEED * Time.deltaTime;
    }

    private void VerticalMove()
    {
        if (movingRange == 0) return; //The pipe should not move vertically

        if (Math.Abs(initialHeadPosition.y - pipeHeadTransform.transform.position.y) >= movingRange)
        {
            direction *= -1;
            pipeHeadTransform.position += new Vector3(0, direction, 0) * verticalSpeed * Time.deltaTime;
            transform.position += new Vector3(0, direction, 0) * verticalSpeed * Time.deltaTime;
        }
        pipeHeadTransform.position += new Vector3(0, direction, 0) * verticalSpeed * Time.deltaTime;
        transform.position += new Vector3(0, direction, 0) * verticalSpeed * Time.deltaTime;

    }

    public float GetXPosition()
    {
        return pipeHeadTransform.transform.position.x;
    }

    public void DestroySelf()
    {
        Destroy(pipeHeadTransform.gameObject);
        Destroy(this.gameObject);
    }

    public bool IsBottom()
    {
        return isBottom;
    }


    public void setIsBottom(bool isBottom)
    {
        this.isBottom = isBottom;
    }


    public void SetHeadTransform(Transform transform)
    {
        pipeHeadTransform = transform;
        initialHeadPosition = pipeHeadTransform.position;
    } 

    public void SetPipeSO(PipeSO pipeSO)
    {
        this.pipeSO = pipeSO;

        //unpack the SO
        this.movingRange = pipeSO.movingRange;
        this.verticalSpeed = pipeSO.verticalSpeed;
    }

}
