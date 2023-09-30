using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fenrir.Actors;
using Fenrir.Managers;
using DG.Tweening;
using Fenrir.EventBehaviour;
using TMPro;


public class DoorController : GameActor<GameManager>
{
    private int ballCounter;
    public int DoorPassageCounter;
    public GameObject CounterText;
    public List<GameObject> DoorBalls = new List<GameObject>();
    private bool ballPushing;

    public override void ActorAwake()
    {
        CounterText.GetComponent<TextMeshPro>().text = "0/" + DoorPassageCounter;
    }

    IEnumerator OpenDoor()
    {
        ballCounter++;
        CounterText.GetComponent<TextMeshPro>().text = ballCounter + "/" + DoorPassageCounter;
        yield return new WaitForSeconds(2f);
        if (ballCounter >= DoorPassageCounter)
        {
            foreach (GameObject ball in DoorBalls)
            {
                Destroy(ball);
            }

            CounterText.SetActive(false);
            transform.DOMove(new Vector3(transform.localPosition.x, 0, transform.localPosition.z), 1f)
                .OnComplete(() => CountinuePlayer());
        }
        else
        {
            GameManager.Instance.FinishLevel(false);
        }
    }

    void CountinuePlayer()
    {
        GameManager.Instance.PushEvent(Constants.COUTINUEPLAYEREVENT);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (DataManager.Instance.PlayersBalls.Count <= 0&& !ballPushing)
            {
                GameManager.Instance.FinishLevel(false);
            }
            else
            {
                ballPushing = true;
                GameManager.Instance.PushEvent(Constants.PAUSEPLAYEREVENT);
                foreach (GameObject ball in DataManager.Instance.PlayersBalls)
                {
                    ball.GetComponent<BallController>().BallForceEvent();
                }
            }
        }

        if (other.CompareTag("Ball"))
        {
            DoorBalls.Add(other.gameObject);
            StartCoroutine(OpenDoor());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ballPushing = false;
        }
    }
}