using System.IO;
using System.Text.Json;

namespace Hash
{
    internal class Train
    {
        public int id { get; private set; }
        public int number { get; private set; }
        public string destination { get; private set; }
        public string time { get; private set; }

        public bool deleted;
        public Train? nextTrain = null;

        public static Train[] FromFile(string path)
        {
            string content = File.ReadAllText(path);
            Train[] trains = JsonSerializer.Deserialize<Train[]>(content);
            return trains;
        }

        public override string ToString()
        {
            return $"{number} - {destination} - {time}";
        }

        public void UpdateTrain(Train train)
        {
            number= train.number;
            destination = train.destination;
            time = train.time;
            deleted = false;
        }

        public Train(int number, string destination, string time)
        {
            this.number = number;
            this.destination = destination;
            this.time = time;
        }
    }
}
