using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Xna.Framework;
using Color = Microsoft.Xna.Framework.Color;

/*\ 
 * Based on:
 * problem - https://community.monogame.net/t/rounded-rectangles-in-monogame/9855;
 * author - https://community.monogame.net/u/Jjagg;
 * code - https://gist.github.com/Jjagg/bd0540ded0d399e716f25e00641488e1.
\*/

namespace Schizofascism.Desktop.Graphics
{
    public interface IPrimitiveBatcher
    {
        void DrawLine(Vector2 p1, Vector2 p2, Color color, float lineWidth);
        void DrawLineStrip(IEnumerable<Vector2> points, Color color, float lineWidth);

        void DrawRect(RectangleF rect, Color color, float lineWidth);
        void DrawRoundedRect(RectangleF rectangle, float radius, int segments, Color color, int lineWidth);
        void DrawRoundedRect(RectangleF rectangle,
            float radiusTl, int segmentsTl,
            float radiusTr, int segmentsTr,
            float radiusBr, int segmentsBr,
            float radiusBl, int segmentsBl,
            Color color, int lineWidth = 1);

        void FillRect(RectangleF rect, Color c);
        void FillRoundedRect(RectangleF rectangle, float radius, int segments, Color color);

        void DrawCircle(Vector2 center, float radius, Color color, int sides, float lineWidth);
        void DrawCircleSegment(Vector2 center, float radius, float start, float end, Color color, int sides, float lineWidth);
        void FillCircle(Vector2 center, float radius, Color color, int sides);
        void FillCircleSegment(Vector2 center, float radius, float start, float end, Color color, int sides);

        void DrawString(StringBuilder text, Vector2 position, float size, Color color);

        void Clear();
        void Flush();
    }
}