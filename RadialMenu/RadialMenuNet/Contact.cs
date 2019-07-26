using System;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace RadialMenu
{
    public class Contact : Prototype
    {
        private VerticalStack _layout;
        private ImageItem _iconImage;
        private BlankItem _faceItem;
        private Label _notificationNumber;
        private Label _nameLabel;

        public Contact(String name)
        {
            _layout = new VerticalStack();
            _iconImage = new ImageItem(DefaultsService.GetDefaultImage(EmbeddedImage.User, EmbeddedImageSize.Size64x64));
            _faceItem = new BlankItem();
            _nameLabel = new Label(name);
            _notificationNumber = new Label();

            SetStyle(DefaultsService.GetDefaultStyle(typeof(RadialMenu.Contact)));
        }

        public String GetName()
        {
            return _nameLabel.GetText();
        }

        public void UpdateSize(int radius)
        {
            int size = radius * 2;
            _faceItem.SetSize(size, size);
            SetHeight(size + _layout.GetSpacing().Vertical + _nameLabel.GetHeight());

            int maxSize = (size > _nameLabel.GetWidth()) ? size : _nameLabel.GetWidth();
            SetWidth(maxSize);
        }

        public override void InitElements()
        {
            _iconImage.KeepAspectRatio(true);
            _nameLabel.SetWidth(_nameLabel.GetTextWidth() + 20);

            AddItems(_layout, _notificationNumber);
            _layout.AddItems(_faceItem, _nameLabel);
            _faceItem.AddItem(_iconImage);
        }

        public void SetNotificationCount(int value)
        {
            _notificationNumber.SetText(value.ToString());
        }

        public override void SetStyle(Style style)
        {
            if (style == null)
                return;
            base.SetStyle(style);

            Style innerStyle = style.GetInnerStyle("layout");
            _layout.SetStyle(innerStyle);

            innerStyle = style.GetInnerStyle("face");
            _faceItem.SetStyle(innerStyle);

            innerStyle = style.GetInnerStyle("name");
            _nameLabel.SetStyle(innerStyle);

            innerStyle = style.GetInnerStyle("notification");
            _notificationNumber.SetStyle(innerStyle);
        }
    }
}