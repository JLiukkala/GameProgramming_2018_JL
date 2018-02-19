using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TankGame.Persistence
{
    public class SaveSystem
    {
        private IPersistence _persistence;

        public SaveSystem(IPersistence persistence)
        {
            _persistence = persistence;
        }
    }
}
