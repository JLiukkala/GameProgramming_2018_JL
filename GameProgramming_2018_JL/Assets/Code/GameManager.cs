using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TankGame
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject gameManagerObject = 
                        new GameObject(typeof(GameManager).Name);
                    _instance = gameManagerObject.AddComponent<GameManager>();
                }
                return _instance;
            }
        }

        private List<Unit> _enemyUnit = new List<Unit>();
        private Unit _playerUnit = null;

        protected void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Init();
        }

        private void Init()
        {
            Unit[] allUnits = FindObjectsOfType<Unit>();
            foreach (Unit unit in allUnits)
            {
                AddUnit(unit);
            }
        }

        public void AddUnit(Unit unit)
        {
            if (unit is EnemyUnit)
            {
                _enemyUnit.Add(unit);
            }
            else if (unit is PlayerUnit)
            {
                _playerUnit = unit;
            }
        }
    }
}
