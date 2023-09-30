using System;
using System.Collections;
using System.Collections.Generic;
using Fenrir.Managers;
using UnityEngine;

public class BallDelay : MonoBehaviour
{
    [SerializeField] private bool isBallsActive;
    [SerializeField] private bool isDelayTriggerActive;
    [SerializeField] private float delayTime;
    [SerializeField] private List<GameObject> balls = new List<GameObject>();

    void Update()
    {
        StartCoroutine(ballDelayEnumerator());
        if (isBallsActive)
        {
            StopCoroutine(ballDelayEnumerator());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDelayTriggerActive = true;
        }
    }

    IEnumerator ballDelayEnumerator()
    {
        if (GameManager.Instance.runtime.isGameStarted && isDelayTriggerActive)
        {
            for (int i = 0; i < balls.Count; i++)
            {
                yield return new WaitForSeconds(delayTime);
                balls[i].gameObject.SetActive(true);
            }

            isBallsActive = true;
        }
    }
}