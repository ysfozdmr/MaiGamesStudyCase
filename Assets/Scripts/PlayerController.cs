using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fenrir.Actors;
using Fenrir.EventBehaviour.Attributes;
using Fenrir.Managers;


public class PlayerController : GameActor<GameManager>
{
    [Header("Movement Settings")] private float _threshold;
    private int screenWidth;
    private float thresholdScreenDivider;
    private float mouseTempX;
    private float horizontalTempPos;
    private float mouseCurrentX;
    private float movementSpeed;
    private float clampMin;
    private float clampMax;
    


    public override void ActorAwake()
    {
        thresholdScreenDivider = DataManager.Instance.GameConfiguration.ThresholdScreenDivider;
         movementSpeed = DataManager.Instance.GameConfiguration.MovementSpeed;
        clampMin = DataManager.Instance.GameConfiguration.MovementClampStart;
        clampMax = DataManager.Instance.GameConfiguration.MovementClampEnd;
    }

    public override void ActorStart()
    {
        screenWidth = Screen.width;
        _threshold = screenWidth / thresholdScreenDivider;
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

    void Movement()
    {
        if (Input.GetMouseButtonDown(0)) //Get initial values
        {
            mouseTempX = (Input.mousePosition.x / Screen.width) * _threshold;

            horizontalTempPos = transform.localPosition.x;
        }

        if (Input.GetMouseButton(0)) //Compare current values to initial values and determine the position
        {
            mouseCurrentX = (Input.mousePosition.x / screenWidth) * _threshold;

            Vector3 pos = transform.localPosition;
            pos.x = horizontalTempPos + (mouseCurrentX - mouseTempX);
            transform.localPosition = pos;

            pos = transform.localPosition;
            pos.x = Mathf.Clamp(pos.x, clampMin, clampMax);

            transform.localPosition = pos;
        }


        transform.position += Vector3.forward * movementSpeed * Time.fixedDeltaTime;
    }

    public override void ActorFixedUpdate()
    {
        if (GameManager.Instance.runtime.isGameStarted)
        {
            Movement();
        }
    }
}