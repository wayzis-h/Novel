using Microsoft.Xna.Framework;
using System.Drawing;

namespace Schizofascism.Desktop.Graphics
{
    public static class Extensions
    {
        public static Vector2 RotatedQuarter(this Vector2 vector)
        {
            return new Vector2(vector.Y, vector.X * -1);
        }

        public static RectangleF Inflate(this RectangleF rect, float a)
        {
            return new RectangleF(rect.X - a, rect.Y - a, rect.Width + a * 2, rect.Height + a * 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        /// <remarks>Not in use!</remarks>
        public static RectangleF Inflate(this RectangleF rect, float x, float y)
        {
            return new RectangleF(rect.X - x, rect.Y - y, rect.Width + x * 2, rect.Height + y * 2);
        }
    }
}
