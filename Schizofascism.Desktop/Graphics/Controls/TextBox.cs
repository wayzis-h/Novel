using Microsoft.Xna.Framework;

namespace Schizofascism.Desktop.Graphics.Controls
{
    public class TextBox : Control
    {
        public string Text { get; set; }

        private Vector2 _vectorPosition;

        public TextBox(string text, MgPrimitiveBatcher primitiveBatcher, Rectangle position)
            : base(primitiveBatcher, position)
        {
            Text = text;
            _vectorPosition = position.Location.ToVector2();
        }

        public override void Draw(GameTime gameTime)
        {
            _batcher.DrawStringCropped(Text, _vectorPosition, _position, 10, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            return;
        }
    }
}
