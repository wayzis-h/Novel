using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Schizofascism.Desktop.Graphics.Controls
{
    public class Image : Control
    {
        private Texture2D _texture;

        public Image(Texture2D texture, MgPrimitiveBatcher primitiveBatcher, Rectangle position)
            : base(primitiveBatcher, position)
        {
            _texture = texture;
        }

        public override void Draw(GameTime gameTime)
        {
            _batcher.SpriteBatcher.Begin();
            _batcher.SpriteBatcher.Draw(_texture, _placement, Color.White);
            _batcher.SpriteBatcher.End();
        }

        public override void Update(GameTime gameTime)
        {
            return;
        }
    }
}
