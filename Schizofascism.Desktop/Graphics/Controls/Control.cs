using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        protected MgPrimitiveBatcher _batcher;
        protected Rectangle _position;

        public virtual event EventHandler<EventArgs> DrawOrderChanged;
        public virtual event EventHandler<EventArgs> VisibleChanged;
        public virtual event EventHandler<EventArgs> EnabledChanged;
        public virtual event EventHandler<EventArgs> UpdateOrderChanged;

        public Control(MgPrimitiveBatcher primitiveBatcher, Rectangle position)
        {
            _batcher = primitiveBatcher;
            _position = position;
        }

        public abstract void Draw(GameTime gameTime);
        public abstract void Update(GameTime gameTime);
    }
}
