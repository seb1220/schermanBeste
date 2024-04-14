using System.Collections.Generic;

namespace PA3_Client
{
    public enum MessageType { NEW, PICK, EXPLODE, SAVE }

    public class Config
    {
        public int GameNumber { get; set; } = 0;
        public int Width { get; set; }
        public int Height { get; set; }
        public int Mines { get; set; }

        override public string ToString()
        {
            return $"GameNumber: {GameNumber}, Width: {Width}, Height: {Height}, Mines: {Mines}";
        }
    }

    public class Pick
    {
        public int X { get; set; }
        public int Y { get; set; }

        override public string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }

    public class Mines
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Count { get; set; }

        override public string ToString()
        {
            return $"X: {X}, Y: {Y}, Count: {Count}";
        }
    }

    public class MSG
    {
        public MessageType Type { get; set; }
        public Config? Config { get; set; }
        public Pick? Pick { get; set; }
        public List<Mines>? Nearby { get; set; }

        override public string ToString()
        {
            return $"Type: {Type}, Config: {Config!}, Pick: {Pick!}, Nearby: {Nearby!}";
        }
    }
}