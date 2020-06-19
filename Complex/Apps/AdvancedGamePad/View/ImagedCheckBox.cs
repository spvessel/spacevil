using SpaceVIL;
using SpaceVIL.Core;

namespace AdvancedGamePad.View
{
    public delegate void EventBoolValueChange(bool value);

    public class ImagedCheckBox : Prototype
    {
        public EventBoolValueChange EventToggle = null;
        private Label _text = null;
        private int _spacing = 10;
        private ImageItem _icon = null;

        public ImagedCheckBox(string text, bool value = false)
        {
            _isChecked = value;
            _text = new Label(text);
            _icon = new ImageItem();

            SetBackground(0, 0, 0, 0);
            SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            SetHeight(30);
        }

        public override void InitElements()
        {
            _icon.IsHover = false;
            _icon.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            _icon.SetSize(40, 30);
            _icon.KeepAspectRatio(true);
            _icon.SetAlignment(ItemAlignment.Left, ItemAlignment.VCenter);
            if (_isChecked)
                _icon.SetImage(Factory.Resources.SwitcherOn);
            else
                _icon.SetImage(Factory.Resources.SwitcherOff);

            _text.IsHover = false;
            _text.SetTextAlignment(ItemAlignment.Left, ItemAlignment.VCenter);
            _text.SetMargin(_icon.GetWidth() + _spacing);
            SetMaxWidth(_text.GetTextWidth() + _icon.GetWidth() + _spacing);

            AddItems(
                _icon,
                _text
            );

            EventMouseHover += (sender, args) =>
            {
                _isHovered = true;
                UpdateImage();
            };
            EventMouseLeave += (sender, args) =>
            {
                _isHovered = false;
                UpdateImage();
            };
            EventMouseClick += (sender, args) =>
            {
                Toggle();
            };

        }

        // logic
        private bool _isHovered = false;
        private bool _isChecked = false;

        public bool IsChecked()
        {
            return _isChecked;
        }

        private void Toggle()
        {
            _isChecked = !_isChecked;
            UpdateImage();
            EventToggle?.Invoke(_isChecked);
        }

        public void SetChecked(bool value)
        {
            if (value == _isChecked)
                return;
            _isChecked = value;
            UpdateImage();
        }

        private void UpdateImage()
        {
            if (_isHovered)
            {
                if (_isChecked)
                    _icon.SetImage(Factory.Resources.SwitcherOnHovered);
                else
                    _icon.SetImage(Factory.Resources.SwitcherOffHovered);
            }
            else
            {
                if (_isChecked)
                    _icon.SetImage(Factory.Resources.SwitcherOn);
                else
                    _icon.SetImage(Factory.Resources.SwitcherOff);
            }
        }

        public void SetText(string text)
        {
            _text.SetText(text);
            SetMaxWidth(_text.GetTextWidth() + _icon.GetWidth() + _spacing);
            Update(GeometryEventType.ResizeWidth);
        }
    }
}