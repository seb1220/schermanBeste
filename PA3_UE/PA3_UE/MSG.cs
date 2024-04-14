namespace PA3_UE
{
    public class MSG
    {
        public PacketType Type { get; set; }

        // Configuration
        public int Size { get; set; }

        // Shot
        public int Index { get; set; }

        // ShotResponse
        public bool HitMine { get; set; }
        public int NearbyMines { get; set; }

        // Finish
        public bool Finished { get; set; }
    }

    public enum PacketType
    {
        Configuration, Shot, ShotResponse, Finish
    }
}
