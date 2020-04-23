using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;

namespace EnigmaGame.View
{
    internal class Card : Prototype
    {
        int[] card = null;
        internal int[] GetCardValue()
        {
            return card;
        }

        Grid grid = new Grid(3, 3);

        public Card()
        {
            SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);
            SetBackground(Color.Transparent);
            SetAlignment(ItemAlignment.VCenter | ItemAlignment.HCenter);
            SetBorderThickness(1);
            SetBorderRadius(0);
            SetBorderFill(Common.Significant);
            IsFocusable = false;
        }

        public override void InitElements()
        {
            grid.SetPadding(2, 2, 2, 2);
            grid.SetAlignment(ItemAlignment.VCenter | ItemAlignment.HCenter);

            AddItems(
                grid
            );
            for (int i = 0; i < 9; i++)
            {
                grid.AddItem(Common.GetPoint());
            }
        }

        public void SetSelected(bool value, bool border)
        {
            if (value)
                SetBackground(15, 15, 15);
            else
                SetBackground(Color.Transparent);

            if (border)
            {
                SetMargin(-1, -1, -1, -1);
                SetBorderThickness(3);
                SetBorderFill(Common.Selected);
            }
            else
            {
                SetMargin(0, 0, 0, 0);
                SetBorderThickness(1);
                SetBorderFill(Common.Significant);
            }

            var points = grid.GetItems();
            for (int i = 0; i < card.Length; i++)
            {
                if (i > points.Count)
                    break;
                if (card[i] == 1)
                {
                    if (value)
                        points[i].SetBackground(Common.Selected);
                    else
                        points[i].SetBackground(Common.Significant);
                }
            }
        }
        public void SetCard(int[] matrix)
        {
            SetBackground(Color.Transparent);
            card = matrix;
            var points = grid.GetItems();
            for (int i = 0; i < matrix.Length; i++)
            {
                if (i > points.Count)
                    break;
                if (matrix[i] == 1)
                    points[i].SetBackground(Common.Significant);
                else
                    points[i].SetBackground(Common.Extinguished);
            }
        }
    }
}