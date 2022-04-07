using System.Text.RegularExpressions;

namespace FrameworkExtensions.Helpers
{
    public class ColorUtil
    {
        private static string ComponentToHex(int component)
        {
            var hex = component.ToString("X");
            return hex.Length == 1 ? "0" + hex : hex;
        }

        public static string RgbToHex(string rgb)
        {
            var match = new Regex("(?<r>\\d*)\\s*,\\s*(?<g>\\d*)\\s*,\\s*(?<b>\\d*)").Match(rgb);
            return match == Match.Empty ? rgb : RgbToHex(match.Groups["r"].Value, match.Groups["g"].Value, match.Groups["b"].Value);
        }

        public static string RgbToHex(int r, int g, int b) =>
            ("#" + ComponentToHex(r) + ComponentToHex(g) + ComponentToHex(b)).ToLower();

        public static string RgbToHex(string r, string g, string b) =>
            RgbToHex(int.Parse(r), int.Parse(g), int.Parse(b));
    }
}
