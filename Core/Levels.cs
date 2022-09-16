using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Spaceshooter.Core
{
    public class Levels
    {
        [JsonInclude] public List<Level> levels;
        public Levels()
        {
            levels = new();
        }
        public Level Get(int id)
        {
            return levels[id];
        }
    }
}
