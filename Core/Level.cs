using System.Text.Json.Serialization;

namespace Spaceshooter.Core
{
    public class Level
    {
        [JsonInclude] public int PlayerHP;
        [JsonInclude] public int PlayerLives;
        [JsonInclude] public double PlayerShootingSpeed;
        [JsonInclude] public double EnemyShootingSpeed;
        [JsonInclude] public int SimpleEnemies;
        [JsonInclude] public int SimpleEnemiesHP;
        [JsonInclude] public int MediumEnemies;
        [JsonInclude] public int MediumEnemiesHP;
    }
}