using System.Collections;
using System.Collections.Generic;
using Fenrir.Actors;
using Fenrir.EventBehaviour.Attributes;
using Fenrir.Managers;
using Fenrir.Resources;
using UnityEngine;

public class CustomLevelActor : LevelActor
{
    public override void SetupLevel()
    {

    }

    [GE(Constants.LEVELENDEVENT)]
    private void LevelEnd()
    {
    }

}