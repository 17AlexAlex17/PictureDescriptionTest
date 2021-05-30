using System.Globalization;
using UnityEngine;

namespace Utils
{
    public static class StringToVector 
    {
        public static Vector2 GetVector2(string rString)
        {
            var temp = rString.Substring(0, rString.Length - 1).Split(',');
            var x = float.Parse(temp[0], CultureInfo.InvariantCulture.NumberFormat);
            var y = float.Parse(temp[1], CultureInfo.InvariantCulture.NumberFormat);
            return new Vector2(x, y);
        }
    }
}

