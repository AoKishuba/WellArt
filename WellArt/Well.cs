using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellArt
{
    /// <summary>
    /// Stores info for each of 96 wells
    /// </summary>
    internal class Well(int x, int y)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
        public Color Color { get; set; } = Color.White;
        public int PTwentyVolume { get; set; }
        public int PTwoHundredVolume { get; set; }

        public override string ToString()
        {
            return (char)('A' + Y) + X.ToString();
        }
    }
}
