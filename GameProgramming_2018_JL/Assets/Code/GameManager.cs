﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TankGame.Persistence;
using System.Linq;
using TankGame.Messaging;
using TankGame.Localization;

namespace TankGame
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null && !IsClosing)
                {
                    GameObject gameManagerObject = 
                        new GameObject(typeof(GameManager).Name);
                    _instance = gameManagerObject.AddComponent<GameManager>();
                }
                return _instance;
            }
        }

        public static bool IsClosing { get; private set; }

        private List<Unit> _enemyUnit = new List<Unit>();
        private Unit _playerUnit = null;
        private SaveSystem _saveSystem;

        public string SavePath
        {
            get { return Path.Combine(Application.persistentDataPath, "save"); }
        }

        public MessageBus MessageBus { get; private set; }

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

        private void OnApplicationQuit()
        {
            IsClosing = true;
        }

        private void Init()
        {
            Localization.Localization.LoadLanguage(LangCode.EN);

            IsClosing = false;

            MessageBus = new MessageBus();

            var UI = FindObjectOfType<UI.UI>();
            UI.Init();

            Unit[] allUnits = FindObjectsOfType<Unit>();
            foreach (Unit unit in allUnits)
            {
                AddUnit(unit);
            }

            _saveSystem = new SaveSystem(new BinaryPersistence(SavePath));
        }

        protected void Update()
        {
            bool save = Input.GetKeyDown(KeyCode.F2);
            bool load = Input.GetKeyDown(KeyCode.F3);

            if (save)
            {
                Save();
            }
            else if (load)
            {
                Load();
            }
        }

        public void AddUnit(Unit unit)
        {
            unit.Init();

            if (unit is EnemyUnit)
            {
                _enemyUnit.Add(unit);
            }
            else if (unit is PlayerUnit)
            {
                _playerUnit = unit;
            }

            UI.UI.Current.HealthUI.AddUnit(unit);
        }

        public void Save()
        {
            GameData data = new GameData();
            foreach (Unit unit in _enemyUnit)
            {
                data.EnemyDatas.Add(unit.GetUnitData());
            }
            data.PlayerData = _playerUnit.GetUnitData();

            _saveSystem.Save(data);
        }

        public void Load()
        {
            GameData data = _saveSystem.Load();
            foreach (UnitData enemyData in data.EnemyDatas)
            {
                Unit enemy = _enemyUnit.FirstOrDefault(unit => unit.Id == enemyData.Id);
                if (enemy != null)
                {
                    enemy.SetUnitData(enemyData);
                }
            }

            _playerUnit.SetUnitData(data.PlayerData);
        }
    }
}
