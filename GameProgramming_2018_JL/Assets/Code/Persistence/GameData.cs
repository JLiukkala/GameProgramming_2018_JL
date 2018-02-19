using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TankGame.Persistence
{
    [Serializable]
    public class GameData
    {
        public UnitData PlayerData;
        public List<UnitData> EnemyDatas = new List<UnitData>();
    }
}
