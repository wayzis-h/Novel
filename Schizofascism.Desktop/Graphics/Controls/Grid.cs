using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Schizofascism.Desktop.Graphics.Controls
{
    public enum GridUnitType
    {
        Absolut,
        Percentage
    }

    public struct GridUnit
    {
        public int Size { get; set; }
        public GridUnitType UnitType { get; set; }
    }

    public struct GridChild
    {
        public Control Control { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public int ColumnSpan { get; set; }
        public int RowSpan { get; set; }
    }

    public class Grid : Control
    {
        public ObservableCollection<GridChild> Children { get; set; }

        public List<GridUnit> Columns { get; set; }
        public List<GridUnit> Rows { get; set; }

        public Grid(MgPrimitiveBatcher primitiveBatcher, Rectangle position) : base(primitiveBatcher, position)
        {
            Children = new ObservableCollection<GridChild>();
            Children.CollectionChanged += Children_CollectionChanged;
            Columns = new List<GridUnit>();
            Rows = new List<GridUnit>();
        }

        private void Children_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var child in e.NewItems.Cast<GridChild>())
                    {
                        var updatedPosition = new Rectangle();
                        if (child.Column == 0)
                        {
                            updatedPosition.X = Placement.X;
                        }
                        else
                        {
                            updatedPosition.X = Placement.X + Placement.Width / 2;
                        }
                        updatedPosition.Width = Placement.Width / 2;
                        if (child.Row == 0)
                        {
                            updatedPosition.Y = Placement.Y;
                        }
                        else
                        {
                            updatedPosition.Y = Placement.Y + Placement.Height / 2;
                        }
                        updatedPosition.Height = Placement.Height / 2;
                        child.Control.Placement = updatedPosition;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var child in Children)
            {
                child.Control.Draw(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var child in Children)
            {
                child.Control.Update(gameTime);
            }
        }
    }
}
