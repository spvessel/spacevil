using System;
using System.Drawing;
using SpaceVIL.Common;
using SpaceVIL.Decorations;
using SpaceVIL.Core;
using SpaceVIL;
using System.IO;

namespace SimpleImageViewer
{
    public class PreviewArea : Prototype
    {
        private WrapGrid _wrapLink;
        private HorizontalStack _toolbar = new HorizontalStack();
        public Frame Area = new Frame();
        private ImageItem _image = new ImageItem();

        private Label _pictureSize = new Label();
        private Label _pictureName = new Label();

        public PreviewArea(WrapGrid area)
        {
            _wrapLink = area;
            SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);
            SetSpacing(0, 5);
            SetAlignment(ItemAlignment.Left, ItemAlignment.Top);
            SetPadding(0, 0, 0, 0);
            SetBackground(20, 20, 20);
            SetMinHeight(100);
        }

        public override void InitElements()
        {
            _image.KeepAspectRatio(true);
            _image.IsHover = false;

            _toolbar.SetHeightPolicy(SizePolicy.Fixed);
            _toolbar.SetHeight(30);
            _toolbar.SetBackground(32, 32, 32);
            _toolbar.SetSpacing(10, 0);
            _toolbar.SetPadding(30, 0, 10, 0);

            _pictureSize.SetWidthPolicy(SizePolicy.Fixed);
            _pictureSize.SetWidth(100);
            _pictureSize.SetTextAlignment(ItemAlignment.VCenter, ItemAlignment.Right);

            Area.SetMargin(0, 30, 0, 0);

            ButtonCore _expand = new ButtonCore();
            _expand.SetSize(30, 30);
            _expand.SetAlignment(ItemAlignment.Right, ItemAlignment.Bottom);
            _expand.SetMargin(0, 0, 10, 10);
            _expand.SetBackground(Color.Transparent);
            _expand.SetBorderRadius(15);
            _expand.SetPadding(5, 5, 5, 5);
            _expand.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(20, 255, 255, 255)));
            _expand.AddItemState(ItemStateType.Pressed, new ItemState(Color.Transparent));

            ButtonCore _menu = new ButtonCore();
            _menu.SetSize(30, 30);
            _menu.SetAlignment(ItemAlignment.Right, ItemAlignment.Top);
            _menu.SetMargin(0, 40, 10, 0);
            _menu.SetBackground(Color.Transparent);
            _menu.SetBorderRadius(15);
            _menu.SetPadding(5, 5, 5, 5);
            _menu.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(20, 255, 255, 255)));
            _menu.AddItemState(ItemStateType.Pressed, new ItemState(Color.Transparent));

            base.AddItem(_toolbar);
            base.AddItem(Area);
            base.AddItem(_expand);
            base.AddItem(_menu);
            Area.AddItem(_image);

            ImageItem eye = new ImageItem(DefaultsService.GetDefaultImage(EmbeddedImage.Eye, EmbeddedImageSize.Size32x32), false);
            eye.KeepAspectRatio(true);

            ImageItem gear = new ImageItem(DefaultsService.GetDefaultImage(EmbeddedImage.Gear, EmbeddedImageSize.Size32x32), false);
            gear.KeepAspectRatio(true);

            _expand.AddItem(eye);
            _menu.AddItem(gear);

            ContextMenu _rotationMenu = new ContextMenu(GetHandler());
            _rotationMenu.ActiveButton = MouseButton.ButtonLeft;
            MenuItem _rot0 = new MenuItem("Rotate 0\u00b0");
            _rot0.EventMouseClick += (sender, args) =>
            {
                _image.SetRotationAngle(0);
            };
            MenuItem _rot90 = new MenuItem("Rotate 90\u00b0");
            _rot90.EventMouseClick += (sender, args) =>
            {
                _image.SetRotationAngle(90);
            };
            MenuItem _rot180 = new MenuItem("Rotate 180\u00b0");
            _rot180.EventMouseClick += (sender, args) =>
            {
                _image.SetRotationAngle(180);
            };
            MenuItem _rot270 = new MenuItem("Rotate -90\u00b0");
            _rot270.EventMouseClick += (sender, args) =>
            {
                _image.SetRotationAngle(270);
            };
            _rotationMenu.AddItems(_rot0, _rot90, _rot180, _rot270);
            _menu.EventMouseClick += (sender, args) =>
            {
                _rotationMenu.Show(sender, args);
            };

            _toolbar.AddItems(_pictureName, _pictureSize);

            _expand.EventMouseClick += (sender, args) =>
            {
                String picture = Model.GetPicturePath(_wrapLink, _pictureName.GetText());
                if (picture != String.Empty)
                {
                    using (Bitmap img = new Bitmap(picture))
                    {
                        ImageItem image = new ImageItem(Model.DownScaleBitmap(img, 1920, 1080), false);
                        FullImageViewer viewer = new FullImageViewer(image);
                        viewer.Show(GetHandler());
                    }
                }
            };

            eye.SetColorOverlay(Color.FromArgb(0, 91, 225, 152), false);
            gear.SetColorOverlay(Color.FromArgb(0, 110, 170, 255), false);

            _expand.EventMouseHover += (sender, args) =>
            {
                eye.SetColorOverlay(true);
            };
            _expand.EventMouseLeave += (sender, args) =>
            {
                eye.SetColorOverlay(false);
            };

            _menu.EventMouseHover += (sender, args) =>
            {
                gear.SetColorOverlay(true);
            };
            _menu.EventMouseLeave += (sender, args) =>
            {
                gear.SetColorOverlay(false);
            };
        }

        public void SetPictureInfo(String name, int w, int h)
        {
            _pictureName.SetText(name);
            _pictureSize.SetText(w + " x " + h);
        }

        public void SetImage(Bitmap bitmap)
        {
            _image.SetImage(Model.DownScaleBitmap(bitmap, 1280, 720));
        }
    }
}
