using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Schizofascism.Desktop.Graphics.Controls
{
    public class Button : IUpdateable, IDrawable
    {
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    EnabledChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        private bool _enabled;

        public int UpdateOrder
        {
            get => _updateOrder;
            set
            {
                if (_updateOrder != value)
                {
                    _updateOrder = value;
                    UpdateOrderChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        private int _updateOrder;

        public int DrawOrder
        {
            get => _drawOrder;
            set
            {
                if (_drawOrder != value)
                {
                    _drawOrder = value;
                    DrawOrderChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        private int _drawOrder;

        public bool Visible
        {
            get => _visible;
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    VisibleChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        private bool _visible;

        private MgPrimitiveBatcher _batcher;
        private Rectangle _position;
        private MouseState _prevState;
        private bool _isMouseOver;

        public Button(MgPrimitiveBatcher primitiveBatcher, Rectangle position)
        {
            _batcher = primitiveBatcher;
            _position = position;
        }

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;
        public event EventHandler<EventArgs> Clicked;

        public void Draw(GameTime gameTime)
        {
            _batcher.FillRect(_position.ToRectangleF(), _isMouseOver ? Color.Aqua : Color.Black);
            _batcher.DrawRect(_position.ToRectangleF(), Color.Gray);
        }

        public void Update(GameTime gameTime)
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
