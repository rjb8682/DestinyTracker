using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Newtonsoft.Json.Linq;

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

        public static async Task<string> GetMemberId(string gamerTag)
        {
            var response = await Helpers.DestinyHttpHandler.GetData("account", "Stats/GetMembershipIdByDisplayName/" + gamerTag);
            var accountInfo = JObject.Parse(response);
            var memberId = accountInfo["Response"].ToString();

            if (memberId == "0")
            {
                throw new UnauthorizedAccessException("Gamertag not recognized. Try again.");
            }

            return memberId;
        }

        public static async Task<string> GetUserInformation(string memberId)
        {
            var responseString = await DestinyHttpHandler.GetData("account", "Account/" + memberId + "/");
            var response = JObject.Parse(responseString);
            return response["Response"]["data"].ToString();
        }
    }
}
