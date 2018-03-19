using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TankGame.Persistence
{
    [Serializable]
    public class UnitData
    {
        public int Id;
        public int Health;
        public Vector3 Position;
        public float YRotation;
    }
}
