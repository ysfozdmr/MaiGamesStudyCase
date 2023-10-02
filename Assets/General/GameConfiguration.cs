using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fenrir.Resources
{

    [CreateAssetMenu(fileName = "Game Configuration", menuName = "Fenrir/Game Confugiration")]
    public class GameConfiguration : ScriptableObject
    {
        public float MovementSpeed;
        public float MovementClampStart;
        public float MovementClampEnd;
        public float DragSpeed;
    }
}