using System.Text.Json.Serialization;

namespace Spaceshooter.Core
{
    public class Level
    {
        [JsonInclude] public int PlayerHP;
        [JsonInclude] public double PlayerShootingSpeed;
        [JsonInclude] public int SimpleEnemies;
        [JsonInclude] public int playerLives;
    }
}