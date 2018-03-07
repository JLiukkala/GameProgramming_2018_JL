using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TankGame
{
    public class Health
    {
        public event Action<Unit> UnitDied;

        public int CurrentHealth { get; protected set; }

        public Unit Owner { get; private set; }

        public Health(Unit owner, int startingHealth)
        {
            Owner = owner;
            CurrentHealth = startingHealth;
        }

        public virtual bool TakeDamage(int damage) 
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, CurrentHealth);
            bool didDie = CurrentHealth == 0;
            if (didDie)
            {
                RaiseUnitDiedEvent();
            }
            return didDie;
        }

        protected void RaiseUnitDiedEvent()
        {
            if (UnitDied != null)
            {
                UnitDied(Owner);
            }
        }

        public void SetHealth(int health)
        {
            //TODO: What if the unit is dead?
            CurrentHealth = health;
        }
    }
}
