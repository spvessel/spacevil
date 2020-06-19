using SpaceVIL;
using SpaceVIL.Core;

using AdvancedGamePad.Factory;

namespace AdvancedGamePad.View
{
    public class SpeedSlider : HorizontalStack
    {
        internal EventCommonMethod EventValueChanged = null; 
        private HorizontalSlider _slider = null;
        private Label _name = null;
        private Label _speed = null;
        public SpeedSlider(string name)
        {
            SetHeightPolicy(SizePolicy.Fixed);
            SetHeight(30);
            SetSpacing(10);
            _name = new Label(name);
            _speed = new Label();
            _slider = new HorizontalSlider();
        }

        public override void InitElements()
        {
            _name.SetWidthPolicy(SizePolicy.Fixed);
            _name.SetWidth(150);

            _speed.SetWidthPolicy(SizePolicy.Fixed);
            _speed.SetWidth(30);
            _speed.SetTextAlignment(ItemAlignment.Left, ItemAlignment.VCenter);

            _slider.SetIgnoreStep(false);
            _slider.Handler.SetShadow(5, 0, 2, Palette.Shadow);
            _slider.SetMaxValue(10);
            _slider.SetMinValue(1);
            _slider.SetStep(1);
            _slider.EventValueChanged += (sender) =>
            {
                _speed.SetText("x" + (int)_slider.GetCurrentValue());
                EventValueChanged?.Invoke();
            };

            AddItems(
                _name,
                _slider,
                _speed
            );

            SetSpeed(3);
        }

        public void SetSpeed(int value)
        {
            _slider.SetCurrentValue(value);
        }

        public int GetSpeed()
        {
            return (int)_slider.GetCurrentValue();
        }

        public void SetText(string text)
        {
            _name.SetText(text);
        }
    }
}