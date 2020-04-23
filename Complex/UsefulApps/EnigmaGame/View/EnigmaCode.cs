using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace EnigmaGame.View
{
    internal class EnigmaCode : VerticalStack
    {
        public Card Top = new Card();
        public Card Bottom = new Card();
        private Label _number = Common.GetNumber(0);
        private VerticalStack _cards = new VerticalStack();

        public EnigmaCode()
        {
            SetSpacing(0, 10);
            IsFocusable = false;
        }

        public override void InitElements()
        {
            _cards.SetBorderFill(Common.Neitral);
            _cards.SetBorderThickness(2);
            _cards.SetPadding(3, 3, 3, 3);
            _cards.SetSpacing(0, -5);
            Top.SetBorderThickness(0);
            Bottom.SetBorderThickness(0);
            AddItems(
                _cards,
                _number
            );
            _cards.AddItems(
                Top,
                Bottom
            );
        }

        public void SetAsLeftSide()
        {
            _cards.SetBorderRadius(new CornerRadius(10, 0, 10, 0));
            Top.SetBorderRadius(new CornerRadius(10, 0, 0, 0));
            Bottom.SetBorderRadius(new CornerRadius(0, 0, 10, 0));
        }
        public void SetAsRightSide()
        {
            _cards.SetBorderRadius(new CornerRadius(0, 10, 0, 10));
            Top.SetBorderRadius(new CornerRadius(0, 10, 0, 0));
            Bottom.SetBorderRadius(new CornerRadius(0, 0, 0, 10));
        }
        public void SetNumber(int number)
        {
            String text = String.Empty;
            if (number < 10)
                text = "0" + number;
            _number.SetText(text);
        }
        public void SetSelected(bool value)
        {
            if (value)
            {
                _number.SetForeground(Common.Selected);
                _cards.SetBorderFill(Common.Selected);
                _cards.SetBorderThickness(3);
                Top.SetSelected(true, false);
                Top.SetBorderFill(Color.Transparent);
                Bottom.SetSelected(true, false);
                Bottom.SetBorderFill(Color.Transparent);
            }
            else
            {
                _number.SetForeground(Common.Neitral);
                _cards.SetBorderFill(Common.Neitral);
                _cards.SetBorderThickness(2);
            }
        }
    }
}