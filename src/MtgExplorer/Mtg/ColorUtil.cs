using System;
using System.Collections.Generic;
using System.Linq;

namespace MtgExplorer.Mtg
{
    public static class ColorUtil
    {
        public static Color[] GetUniqueColors(string manaCost)
        {
            List<string> pieces = manaCost.Split('}').Select(item => item.Trim(new[] { '{', '}' })).ToList();
            var colors = new List<Color>();
            foreach (string piece in pieces)
            {
                Color color;
                if (piece.Contains("/"))
                {
                    var components = piece.Split('/');
                    foreach (var component in components)
                    {
                        color = GetColorFromString(component);
                        if (color != Color.None)
                        {
                            colors.Add(color);
                        }
                    }
                    continue;
                }
                color = GetColorFromString(piece);
                if (color != Color.None)
                {
                    colors.Add(color);
                }

            }
            if (colors.Count == 0)
            {
                // Find a colorless mana cost
                if (pieces.Any(piece => piece.All(Char.IsDigit)))
                {
                    colors.Add(Color.Colorless);
                }
            }
            Color[] result = colors.Distinct().ToArray();
            return result;
        }

        private static Color GetColorFromString(string text)
        {
            // The five colors are white, blue, black, red, and green. The white mana symbol is represented
            // by {W}, blue by {U}, black by {B}, red by {R}, and green by {G}. Phrexian is {P}

            if (String.Equals("B", text, StringComparison.OrdinalIgnoreCase))
            {
                return Color.Black;
            }
            if (String.Equals("U", text, StringComparison.OrdinalIgnoreCase))
            {
                return Color.Blue;
            }
            if (String.Equals("G", text, StringComparison.OrdinalIgnoreCase))
            {
                return Color.Green;
            }
            if (String.Equals("R", text, StringComparison.OrdinalIgnoreCase))
            {
                return Color.Red;
            }
            if (String.Equals("W", text, StringComparison.OrdinalIgnoreCase))
            {
                return Color.White;
            }
            if (String.Equals("P", text, StringComparison.OrdinalIgnoreCase))
            {
                return Color.Phrexian;
            }
            return Color.None;
        }

        public static Color[] GetCardColor(string manaCost, Color[] colorIndicator)
        {
            if (colorIndicator == null || colorIndicator.Length == 0)
            {
                Color[] colors = GetUniqueColors(manaCost);
                return colors;
            }
            // TODO: How to deal with color indicator
            // See 204.2
            return colorIndicator;
        }
    }
}