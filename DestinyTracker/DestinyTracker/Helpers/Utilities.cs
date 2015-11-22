using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace DestinyTracker.Helpers
{
    public class Utilities
    {
        public static Color GetColorFromHex(string hex, byte alpha = 255)
        {
            return Color.FromArgb(alpha,
                                  Convert.ToByte(hex.Substring(1, 2), 16),
                                  Convert.ToByte(hex.Substring(3, 2), 16),
                                  Convert.ToByte(hex.Substring(5, 2), 16));
        }
    }
}
