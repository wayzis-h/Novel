using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Schizofascism.Desktop.Graphics.Controls
{
    public class Button : Control
    {
        public string Text
        {
            get => _text.Text;
            set => _text.Text = value;
        }

        private TextBox _text;
        private MouseState _prevState;
        private bool _isMouseOver;

        public Button(MgPrimitiveBatcher primitiveBatcher, Rectangle position)
            : base(primitiveBatcher, position)
        {
            _text = new TextBox(string.Empty, primitiveBatcher, position);
        }

        public event EventHandler<EventArgs> Clicked;

        public override void Draw(GameTime gameTime)
        {
            _batcher.FillRect(_position.ToRectangleF(), _isMouseOver ? Color.Aqua : Color.Black);
            _batcher.DrawRect(_position.ToRectangleF(), Color.Gray);
            _batcher.Flush();

            _text.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            var state = Mouse.GetState();
            if (_position.Contains(state.Position))
            {
                _isMouseOver = true;
                if (_prevState.LeftButton == ButtonState.Pressed && state.LeftButton == ButtonState.Released)
                {
                    Clicked?.Invoke(this, new EventArgs());
                }
            }
            else
            {
                _isMouseOver = false;
            }
            _prevState = state;
        }
    }
}
