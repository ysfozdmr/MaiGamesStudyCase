using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coskunerov.Managers;

namespace Coskunerov.Actors
{
    public class GameSingleActor<T> : GameActor<GameManager> where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType<T>();
                }
                return instance;
            }
        }

    }
}
