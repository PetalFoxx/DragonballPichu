using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonballPichu.Common.Systems
{
    internal class Direction
    {
        Dictionary<string, string> directionsToShorthand = new Dictionary<string, string>()
        {
            { "North", "N" },
            { "East", "E" },
            { "South", "S" },
            { "West", "W" },
            { "Up", "N" },
            { "Right", "E" },
            { "Down", "S" },
            { "Left", "W" },
            { "N", "N" },
            { "E", "E" },
            { "S", "S" },
            { "W", "W" }
        };

        Dictionary<string, string> rotateRightDictionary = new Dictionary<string, string>()
        {
            { "N", "E" },
            { "E", "S" },
            { "S", "W" },
            { "W", "N" }
        };

        Dictionary<string, string> rotateLeftDictionary = new Dictionary<string, string>()
        {
            { "N", "W" },
            { "W", "S" },
            { "S", "E" },
            { "E", "N" }
        };

        string name;
        public Direction(string name) 
        {
            if(name == null) throw new ArgumentNullException("Direction Name");
            if (directionsToShorthand.ContainsKey(name))
            {
                this.name = name;
            }
            else
            {
                this.name = null;
            }
        }

        public string nameToShorthand()
        {
            return directionsToShorthand[name];
        }

        public Boolean compare(Direction direction)
        {
            return nameToShorthand().Equals(direction.nameToShorthand());
        }

        public Direction rotate(Boolean right)
        {
            if (right)
            {
                return new Direction(rotateRightDictionary[nameToShorthand()]);
            }
            return new Direction(rotateLeftDictionary[nameToShorthand()]);
        }


    }
}
