namespace Entities {
    abstract class Entity {
        public int Health { get; private set; }
        public string Name { get; private set; }

        public void init(string name, int health) {
            Name = name;
            Health = health;
        }

        public int Damage(int amount) {
           return Health -= amount;
        }

        public int Heal(int amount) {
            return Health += amount;
        }

        public abstract char[] Draw();
    }
}