using System;
using System.Collections.Generic;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace RadialMenu
{
    public class RadialMenuItem : Prototype, IFloating, IFreeLayout, IDraggable
    {
        public Int32 Radius = 200;
        public Int32 ItemRadius = 30;
        public ButtonCore HideButton = new ButtonCore();
        public ButtonCore AddButton = new ButtonCore("Add");

        public RadialMenuItem(CoreWindow wnd)
        {
            ItemsLayoutBox.AddItem(wnd, this, LayoutType.Floating);

            SetPassEvents(false);
            SetStyle(DefaultsService.GetDefaultStyle(typeof(RadialMenu.RadialMenuItem)));
        }

        private bool _isInit = false;

        public override void InitElements()
        {
            base.AddItem(HideButton);
            base.AddItem(AddButton);

            ImageItem icon = new ImageItem(
                DefaultsService.GetDefaultImage(EmbeddedImage.ArrowLeft, EmbeddedImageSize.Size64x64));
            icon.SetRotationAngle(90);
            icon.KeepAspectRatio(true);
            icon.SetColorOverlay(Color.Black);

            HideButton.AddItem(icon);

            // Events
            HideButton.EventMouseClick += (sender, args) =>
            {
                Hide();
            };

            EventScrollUp += (sender, args) =>
            {
                _degreeDiff = -_scrollStep * _toRadianCoeff;
                RearrangeContacts();
            };
            EventScrollDown += (sender, args) =>
            {
                _degreeDiff = _scrollStep * _toRadianCoeff;
                RearrangeContacts();
            };
            EventKeyPress += (sender, args) =>
            {
                if (args.Key == KeyCode.Menu)
                    Hide();
            };
            EventMousePress += OnMousePress;
            EventMouseDrag += OnMouseDrag;
            EventMouseDrop += OnMouseDrop;

            foreach (var item in _contacts)
            {
                base.AddItem(item);
                item.UpdateSize(ItemRadius);
            }

            _isInit = true;
        }

        //IDraggable
        private void OnMousePress(IItem sender, MouseArgs args)
        {
            _beginDegree = CalculateDegreeByPos(args.Position);
        }

        private void OnMouseDrag(IItem sender, MouseArgs args)
        {
            double dragDegree = CalculateDegreeByPos(args.Position);
            _degreeDiff = dragDegree - _beginDegree;
            _beginDegree = dragDegree;
            RearrangeContacts();
        }

        private void OnMouseDrop(IItem sender, MouseArgs args)
        {
            _degreeDiff = 0;
        }

        public override void AddItem(IBaseItem item)
        {
            Contact contact = item as Contact;
            if (contact != null)
            {
                _contacts.Add(contact);
                if (_isInit)
                    base.AddItem(contact);
                contact.UpdateSize(ItemRadius);
            }
            CalculateAlphaStep();
            UpdateLayout();
        }

        public override bool RemoveItem(IBaseItem item)
        {
            if (base.RemoveItem(item))
            {
                Contact contact = item as Contact;
                if (contact != null)
                    _contacts.Remove(contact);
                CalculateAlphaStep();
                UpdateLayout();
                return true;
            }
            return false;
        }

        public List<Contact> GetContacts()
        {
            return new List<Contact>(_contacts);
        }

        // Style override
        public override void SetStyle(Style style)
        {
            if (style == null)
                return;
            base.SetStyle(style);

            Style innerStyle = style.GetInnerStyle("hidebutton");
            HideButton.SetStyle(innerStyle);

            innerStyle = style.GetInnerStyle("addbutton");
            AddButton.SetStyle(innerStyle);
        }

        //IFree
        public override void SetHeight(int height)
        {
            base.SetHeight(height);
            UpdateLayout();
        }

        public override void SetWidth(int width)
        {
            base.SetWidth(width);
            UpdateLayout();
        }

        private bool _isUpdating = false;

        public void UpdateLayout()
        {
            if (_isUpdating)
                return;
            _isUpdating = true;
            UpdateControlButtonPosition();
            RearrangeContacts();
            _isUpdating = false;
        }

        //IFloating
        public void Hide()
        {
            SetVisible(false);
            GetHandler().SetFocus();
        }
        
        public void Hide(MouseArgs args)
        {
            Hide();
        }

        public void Show()
        {
            if (!_isInit)
            {
                InitElements();
                UpdateLayout();
            }
            SetVisible(true);
        }

        public void Show(IItem sender, MouseArgs args)
        {
            Show();
        }

        private bool _ouside = false;
        public bool IsOutsideClickClosable()
        {
            return _ouside;
        }
        public void SetOutsideClickClosable(bool value)
        {
            _ouside = value;
        }

        //Model logic
        private readonly double _doubledPI = 2 * Math.PI;
        private readonly double _toRadianCoeff = Math.PI / 180.0;
        private readonly double _scrollStep = 10.0;
        private double _alpha = Math.PI;
        private double _beginDegree = 0;
        private double _degreeDiff = 0;
        private List<Contact> _contacts = new List<Contact>();
        private double _alphaStep = 0;

        private void UpdateControlButtonPosition()
        {
            int x = -HideButton.GetWidth() / 2;
            int y = -HideButton.GetHeight() / 2;

            x = GetWidth() / 2 + GetX() + x;
            y = GetHeight() / 2 + GetY() + y;

            HideButton.SetPosition(x, y);
            AddButton.SetPosition(x + HideButton.GetWidth() / 2 - AddButton.GetWidth() / 2,
                y + HideButton.GetHeight() + 10);
        }

        private void RearrangeContacts()
        {
            Pointer centerPoint = GetCenter();
            _alpha += _degreeDiff;

            foreach (var item in _contacts)
            {
                int x = (int)(Radius * Math.Cos(_alpha)) + centerPoint.GetX() - ItemRadius;
                int y = (int)(Radius * Math.Sin(_alpha)) + centerPoint.GetY() - ItemRadius;
                item.SetPosition(x, y);
                item.SetConfines();
                _alpha += _alphaStep;
            }
        }

        private double CalculateDegreeByPos(Pointer position)
        {
            Pointer centerPoint = GetCenter();
            double degree = Math.Atan2(position.GetY() - centerPoint.GetY(), position.GetX() - centerPoint.GetX());
            if (degree < 0)
                degree += _doubledPI;
            return degree;
        }

        private void CalculateAlphaStep()
        {
            _alphaStep = _doubledPI / _contacts.Count;
        }

        private Pointer GetCenter()
        {
            Pointer center = new Pointer();
            int xCenter = HideButton.GetWidth() / 2 + HideButton.GetX();
            int yCenter = HideButton.GetHeight() / 2 + HideButton.GetY();
            center.SetPosition(xCenter, yCenter);
            return center;
        }
    }
}
