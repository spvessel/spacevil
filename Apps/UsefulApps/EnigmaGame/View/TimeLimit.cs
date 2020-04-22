using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace EnigmaGame.View
{
    internal class TimeLimit : Prototype
    {
        SpaceVIL.Rectangle track = new SpaceVIL.Rectangle();
        SpaceVIL.Rectangle progress = new SpaceVIL.Rectangle();
        Label value = new Label("15");

        public TimeLimit()
        {
            SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            SetHeight(40);
            SetBackground(Color.Transparent);
            SetBorder(new Border(Common.Significant, new CornerRadius(10), 2));
            SetPadding(20, 0, 20, 0);
            IsFocusable = false;
        }

        public override void InitElements()
        {
            track.SetBackground(Common.Selected);
            track.SetHeight(4);
            track.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            track.SetBorderRadius(2);
            track.SetAlignment(ItemAlignment.VCenter | ItemAlignment.Left);
            track.SetMargin(0, 0, 40, 0);

            progress.SetStyle(track.GetCoreStyle());
            progress.SetBackground(Common.Extinguished);
            progress.SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            progress.SetBorderRadius(2);

            value.SetFont(Common.AppFont);
            value.SetFontSize(22);
            value.SetForeground(Common.Text);
            value.SetTextAlignment(ItemAlignment.VCenter | ItemAlignment.Right);

            AddItems(
                progress,
                track,
                value
            );
            UpdateLayout();
        }

        int _maxValue = 15;
        public void SetMaxValue(int value) {
            if (value < 5)
                return;
            _maxValue = value;
        }
        public int GetMaxValue() { return _maxValue; }

        int _minValue = 0;
        public void SetMinValue(int value) { _minValue = value; }
        public int GetMinValue() { return _minValue; }

        int _val = 15;
        public int GetValue()
        {
            return _val;
        }
        public void SetValue(int value)
        {
            if (value < 0)
                value = 0;
            if (value > _maxValue)
                value = _maxValue;
            _val = value;
            UpdateLayout();
        }

        public void UpdateLayout()
        {
            float AllLength = _maxValue - _minValue;
            float DonePercent = (_val - _minValue) / AllLength;
            value.SetText(Math.Round(DonePercent * _maxValue, 1).ToString());
            float w = (float)progress.GetWidth() / (float)_maxValue * _val;
            track.SetWidth((int)w);
        }

        public override void SetWidth(int width)
        {
            base.SetWidth(width);
            UpdateLayout();
        }

        public override void SetX(int _x)
        {
            base.SetX(_x);
            UpdateLayout();
        }
    }
}