using System;

namespace PA3N_5B
{
    public class MSG
    {
        public bool? NewGame { get; set; } = true;
        public String Word { get; set; }

        public override string ToString()
        {
            return $"NewGame: {NewGame}; Word: {Word}";
        }
    }
}
