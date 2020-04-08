using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities {
    static class EntityFactory {
        public static T Create<T>(string name, int health) where T : Entity, new() {
            var entity = new T();
            entity.init(name, health);
            return entity;
        }
    }
}
