using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fenrir.Actors;
using Fenrir.Managers;


public class BallController : GameActor<GameManager>
{
    private Rigidbody rb;
    public override void ActorAwake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void BallForceEvent()
    {
        rb.AddForce(new Vector3(0,0,13));
    }
    
}
