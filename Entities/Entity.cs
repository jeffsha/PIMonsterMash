using System;
using System.Collections.Generic;

namespace PIMonsterMash.Entities {

    abstract class Entity {
        public int Health { get; private set; }
        public string Name { get; private set; }

        public string MusicPath { get; private set; }

        public virtual List<string> Art { get; private set; }

        public void Init(string name, int health, List<string> art, string musicPath) {
            Name = name;
            Health = health;
            Art = art;
            MusicPath = musicPath;
        }

        public void Spawn() {
            Spawned(this);
        }

        public int Damage(int amount) {
            Health -= amount;
            Damaged(this, new EntityDamageEventArgs(amount));
            return Health;
        }

        public int Heal(int amount) {
            Health += amount;
            Healed(this, new EntityDamageEventArgs(amount));
            return Health;
        }

        public delegate void SpawnedHandler(Entity sender);
        public event SpawnedHandler Spawned;

        public delegate void DamageHandler(Entity sender, EntityDamageEventArgs e);
        public event DamageHandler Damaged;

        public delegate void HealHandler(Entity sender, EntityDamageEventArgs e);
        public event HealHandler Healed;

    }

    public class EntityDamageEventArgs : EventArgs {
        public int Damage { get; set; }

        public EntityDamageEventArgs(int damage)
        {
            Damage = damage;
        }
    }

}