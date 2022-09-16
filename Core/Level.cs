using System.IO;
using System.Text.Json;
namespace Spaceshooter.Core
{
    public class Level
    {
        public int ID;
        public int PlayerHP;
        public int PlayerShootingSpeed;
        public int SimpleEnemies;
        public Level()
        {
            string fileName = "D:\\4F\\space-shooter\\levels.json";
            string jsonString = File.ReadAllText(fileName);
            ID = JsonSerializer.Deserialize<Level>(jsonString).ID;
        }
    }
}