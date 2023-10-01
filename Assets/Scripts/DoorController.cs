using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fenrir.Actors;
using Fenrir.Managers;
using DG.Tweening;
using TMPro;


public class DoorController : GameActor<GameManager>
{
    private int ballCounter;
    public int DoorPassageCounter;
    public GameObject CounterText;
    public List<GameObject> DoorBalls = new List<GameObject>();
    private bool ballPushing;
    [SerializeField] private GameObject wall;

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
            wall.SetActive(false);
            foreach (GameObject ball in DoorBalls)
            {
                DataManager.Instance.PlayersBalls.Remove(ball);
                ball.transform.localScale = Vector3.zero;
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
        GameManager.Instance.PushEvent(Constants.DESTROYBALLEVENT);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (DataManager.Instance.PlayersBalls.Count <= 0 && !ballPushing)
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