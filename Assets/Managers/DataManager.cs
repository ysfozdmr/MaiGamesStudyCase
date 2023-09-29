using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Coskunerov.EventBehaviour;
using Coskunerov.Resources;

namespace Coskunerov.Managers
{
    public class DataManager : EventBehaviour<DataManager>
    {
        public FXData fXData;
        public LevelDataCapsule levelCapsule;
    }
}
