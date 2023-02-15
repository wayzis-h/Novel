using Microsoft.Xna.Framework;
using System;

namespace Schizofascism.Desktop.Graphics.Controls
{
    public abstract class Control : IDrawable, IUpdateable
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

        public Rectangle Placement
        {
            get => _placement;
            set
            {
                if (_placement != value)
                {
                    _placement = value;
                    PlacementChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        protected Rectangle _placement;

        protected MgPrimitiveBatcher _batcher;

        public virtual event EventHandler<EventArgs> DrawOrderChanged;
        public virtual event EventHandler<EventArgs> VisibleChanged;
        public virtual event EventHandler<EventArgs> EnabledChanged;
        public virtual event EventHandler<EventArgs> UpdateOrderChanged;
        public virtual event EventHandler<EventArgs> PlacementChanged;


        public Control(MgPrimitiveBatcher primitiveBatcher, Rectangle position)
        {
            _batcher = primitiveBatcher;
            _placement = position;
        }

        public abstract void Draw(GameTime gameTime);
        public abstract void Update(GameTime gameTime);
    }
}
