using System;
using System.Collections.Generic;

namespace PIMonsterMash.Entities {

    abstract class Entity {
        public int Health { get; private set; }
        public string Name { get; private set; }

        public string MusicPath { get; private set; }

        public virtual List<string> Art { get; private set; }

        public void init(string name, int health, List<string> art, string musicPath) {
            Name = name;
            Health = health;
            Art = art;
            MusicPath = musicPath;
        }

        public void spawn() {
            Spawned(this);
        }

        public int Damage(int amount) {
            var health = Health -= amount;
            Damaged(health);
            return health;
        }

        public int Heal(int amount) {
            var health = Health += amount;
            Healed(health);
            return health;
        }

        public delegate void SpawnedHandler(Entity sender);
        public event SpawnedHandler Spawned;

        public delegate void DamageHandler(int health);
        public event DamageHandler Damaged;

        public delegate void HealHandler(int health);
        public event HealHandler Healed;

    }
}