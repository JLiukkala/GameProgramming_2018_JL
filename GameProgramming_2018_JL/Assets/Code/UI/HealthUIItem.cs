using UnityEngine;
using UnityEngine.UI;
using TankGame.Messaging;

namespace TankGame.UI
{
    public class HealthUIItem : MonoBehaviour
    {
        private Unit _unit;

        private Text _text;

        private ISubscription<UnitDiedMessage> _unitDiedSubscription;

        public bool IsEnemy
        {
            get { return _unit != null && _unit is EnemyUnit; }
        }

        protected void OnDestroy()
        {
            UnregisterEventListeners();
        }

        public void Init(Unit unit)
        {
            _unit = unit;
            _text = GetComponentInChildren<Text>();
            _text.color = IsEnemy ? Color.red : Color.green;
            _unit.Health.HealthChanged += OnUnitHealthChanged;
            //_unit.Health.UnitDied += OnUnitDied;
            _unitDiedSubscription = GameManager.Instance.MessageBus.Subscribe<UnitDiedMessage>(OnUnitDied);
            SetText(_unit.Health.CurrentHealth);
        }

        private void OnUnitDied(UnitDiedMessage msg)
        {
            if (msg.DeadUnit == _unit)
            {
                UnregisterEventListeners();
            }
        }

        private void UnregisterEventListeners()
        {
            _unit.Health.HealthChanged -= OnUnitHealthChanged;
            if (!GameManager.IsClosing)
            {
                GameManager.Instance.MessageBus.Unsubscribe(_unitDiedSubscription);
            }
            //_unit.Health.UnitDied -= OnUnitDied;
        }

        private void OnUnitHealthChanged(Unit unit, int health)
        {
            SetText(health);
        }

        private void SetText(int health)
        {
            //C# 6 Syntax:
            //_text.text = $"{_unit.name} health: {health}";
            _text.text = string.Format("{0} health: {1}", _unit.name, health);

        }
    }
}
