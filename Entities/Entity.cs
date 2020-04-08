using System.Collections.Generic;

namespace PIMonsterMash.Entities {
    abstract class Entity {
        public int Health { get; private set; }
        public string Name { get; private set; }

        public virtual List<string> Art { get; private set; }

        public void init(string name, int health, List<string> art) {
            Name = name;
            Health = health;
            Art = art;
        }

        public int Damage(int amount) {
           return Health -= amount;
        }

        public int Heal(int amount) {
            return Health += amount;
        }
    }
}