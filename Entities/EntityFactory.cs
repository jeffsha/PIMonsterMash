using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMonsterMash.Entities {
    static class EntityFactory {
        public static T Create<T>(string name = null, int health = -1, string music = null) where T : Entity, new() {
            var entity = new T();
            var art = (entity.Art ?? Art.GetRandom());
            name = (name ?? Names.GetRandom());
            health = (health < 0 ? new Random().Next(1000, 100000) : health);
            music = (music ?? MusicPlayer.GetRandom());
            entity.Init(name, health, art, music);
            return entity;
        }
    }
}
