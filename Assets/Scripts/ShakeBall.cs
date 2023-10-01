using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Fenrir.Actors;
using Fenrir.EventBehaviour.Attributes;
using Fenrir.Managers;

public class ShakeBall : GameActor<GameManager>
{
    [SerializeField] private GameObject ballPrefab;
    public GameObject CenterPoint; // Nesnenin etrafında döneceği merkezi nokta
    public float TurnSpeed = 45f;
    public List<GameObject> CreatedBalls;

    private void Update()
    {
        if (GameManager.Instance.runtime.isGameStarted)
        {
            transform.RotateAround(CenterPoint.transform.position, Vector3.up, TurnSpeed * Time.deltaTime);
        }
    }

    void CreateBalls()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject go;
            Vector3 pos = new Vector3(transform.position.x - (i / 2), transform.position.y, transform.position.z);
            go = Instantiate(ballPrefab, pos, Quaternion.identity);
            CreatedBalls.Add(go);
            go.transform.parent = transform.parent;
        }

        gameObject.transform.localScale = Vector3.zero;
    }

    [GE(Constants.DESTROYBALLEVENT)]
    private void DestroyBalls()
    {
        foreach (GameObject ball in CreatedBalls)
        {
            Debug.Log("calisti");
            ball.SetActive(false);
            CreatedBalls.Remove(ball);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CreateBalls();
        }
    }
}