using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fenrir.Actors;
using Fenrir.EventBehaviour.Attributes;
using Fenrir.Managers;


public class PlayerController : GameActor<GameManager>
{
    [Header("Movement Settings")] private float mouseTempX;
    private float horizontalTempPos;
    private float mouseCurrentX;
    private float movementSpeed;
    private float dragSpeed;
    private float clampMin;
    private float clampMax;
    Vector3 direction;
    private Vector3? lastMousePos;
    private Vector2 diff;


    private Rigidbody rb;

    public override void ActorAwake()
    {
        rb = GetComponent<Rigidbody>();
        movementSpeed = DataManager.Instance.GameConfiguration.MovementSpeed;
        dragSpeed = DataManager.Instance.GameConfiguration.DragSpeed;
        clampMin = DataManager.Instance.GameConfiguration.MovementClampStart;
        clampMax = DataManager.Instance.GameConfiguration.MovementClampEnd;
    }

    public override void ActorStart()
    {
    }

    [GE(Constants.PAUSEPLAYEREVENT)]
    private void PausePlayer()
    {
        movementSpeed = 0f;
    }

    [GE(Constants.COUTINUEPLAYEREVENT)]
    private void ContinuePlayer()
    {
        movementSpeed = DataManager.Instance.GameConfiguration.MovementSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            DataManager.Instance.PlayersBalls.Add(other.gameObject);
        }

        if (other.CompareTag("Finish"))
        {
            GameManager.Instance.FinishLevel(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            DataManager.Instance.PlayersBalls.Remove(other.gameObject);
        }
    }

    
    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            lastMousePos = null;
            direction = Vector3.zero;
        }

        if (lastMousePos != null)
        {
            diff = (Vector2)Input.mousePosition - (Vector2)lastMousePos;

            lastMousePos = Input.mousePosition;
            direction = Vector3.Lerp(direction, Vector3.right * diff.x, Time.deltaTime * 5);
        }
    }


    private void Clamp()
    {
        Vector3 newPosition = rb.position;
        newPosition.x = Mathf.Clamp(newPosition.x, clampMin, clampMax);
        rb.position = newPosition;
    }

    void Movement()
    {
        rb.velocity = Vector3.ClampMagnitude((direction * dragSpeed), 4) + (Vector3.forward * movementSpeed);
    }


    public override void ActorUpdate()
    {
        if (GameManager.Instance.runtime.isGameStarted)
        {
            Clamp();
            GetInput();
        }
    }

    public override void ActorFixedUpdate()
    {
        if (GameManager.Instance.runtime.isGameStarted)
        {
            Movement();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}