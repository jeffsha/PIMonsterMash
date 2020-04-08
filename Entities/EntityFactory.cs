using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMonsterMash.Entities {
    static class EntityFactory {
        public static T Create<T>(string name, int health = -1, string musicPath = null) where T : Entity, new() {
            var entity = new T();
            var pHealth = (health < 0 ? new Random().Next(1000, 100000) : health);
            var pArt = (entity.Art ?? Art.GetRandom());
            var pMusic = (musicPath ?? MusicPlayer.GetRandom());
            entity.Init(name, pHealth, pArt, pMusic);
            return entity;
        }
    }
}
