using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fenrir.Actors;
using Fenrir.Managers;

public class ShakeBall : GameActor<GameManager>
{
    [SerializeField] private GameObject ballPrefab;
    public GameObject CenterPoint; 
    [SerializeField]private float TurnSpeed = 45f;
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



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CreateBalls();
        }
    }
}