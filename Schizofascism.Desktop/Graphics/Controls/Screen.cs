using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Schizofascism.Desktop.Graphics.Controls
{
    public class Screen : Control
    {
        public List<Control> Children { get; set; }

        public Screen(MgPrimitiveBatcher primitiveBatcher, Rectangle position)
            : base(primitiveBatcher, position)
        {
            Children = new List<Control>();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var child in Children)
            {
                child.Draw(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var child in Children)
            {
                child.Update(gameTime);
            }
        }
    }
}
