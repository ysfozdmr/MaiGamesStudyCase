using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coskunerov.Actors;


namespace Coskunerov.Resources
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "Coskunerov/Level Data")]
    public class LevelData : ScriptableObject
    {
        public LevelActor LevelPrefab;
    }
    
}