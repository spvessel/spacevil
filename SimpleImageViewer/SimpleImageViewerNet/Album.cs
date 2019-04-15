using System;
using System.Drawing;
using SpaceVIL.Common;
using SpaceVIL.Decorations;
using SpaceVIL.Core;
using SpaceVIL;
using System.IO;

namespace SimpleImageViewer
{
    public class Album : Prototype
    {
        public EventCommonMethodState OnDoubleClick;

        private HorizontalStack _topLayout = new HorizontalStack();
        internal ButtonToggle _expand = new ButtonToggle();
        private ImageItem _arrow = new ImageItem(DefaultsService.GetDefaultImage(EmbeddedImage.ArrowLeft, EmbeddedImageSize.Size32x32));
        public Label Name = new Label();
        private ButtonCore _remove = new ButtonCore();

        private HorizontalStack _bottomLayout = new HorizontalStack();
        private Label _pathLabel = new Label("Path:");
        private TextEdit _pathEdit = new TextEdit();
        private ButtonCore _pathBrowse = new ButtonCore();

        public String GetPath()
        {
            return _pathEdit.GetText();
        }

        public Album(String name, String path)
        {
            SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            SetHeight(30);
            SetSpacing(0, 10);
            SetMargin(3, 5, 3, 5);
            SetAlignment(ItemAlignment.Left, ItemAlignment.Top);
            SetBackground(Color.Transparent);
            SetShadow(10, 0, 0, Color.FromArgb(200, 0, 0, 0));
            Name.SetText(name);
            _pathEdit.SetText(path);
            _bottomLayout.SetVisible(false);
        }

        public override void InitElements()
        {
            //top
            _topLayout.SetHeightPolicy(SizePolicy.Fixed);
            _topLayout.SetHeight(30);
            _topLayout.SetSpacing(5, 0);
            _topLayout.SetBackground(Color.FromArgb(20, 255, 255, 255));

            _expand.SetSize(20, 30);
            _expand.SetBackground(25, 25, 25);
            _expand.GetState(ItemStateType.Toggled).Background = Color.FromArgb(25, 25, 25);
            _expand.SetPadding(4, 9, 4, 9);
            _arrow.SetRotationAngle(180);
            _arrow.SetColorOverlay(Color.FromArgb(210, 210, 210));
            _arrow.KeepAspectRatio(true);

            Name.SetHeightPolicy(SizePolicy.Fixed);
            Name.SetHeight(30);
            Name.SetMargin(5, 0, 0, 0);
            Name.SetFontSize(16);
            Name.SetFontStyle(FontStyle.Bold);

            _remove.SetSize(12, 12);
            _remove.SetCustomFigure(new CustomFigure(false, GraphicsMathService.GetCross(12, 12, 3, 45)));
            _remove.SetBackground(100, 100, 100);
            _remove.SetAlignment(ItemAlignment.VCenter, ItemAlignment.Left);
            _remove.SetMargin(0, 0, 5, 0);

            //bottom
            _bottomLayout.SetHeightPolicy(SizePolicy.Fixed);
            _bottomLayout.SetHeight(30);
            _bottomLayout.SetSpacing(5, 0);
            _bottomLayout.SetAlignment(ItemAlignment.Left, ItemAlignment.Bottom);

            _pathLabel.SetWidthPolicy(SizePolicy.Fixed);
            _pathLabel.SetWidth(_pathLabel.GetTextWidth() + 5);
            // _pathLabel.SetMargin(5, 0, 0, 0);
            _pathLabel.SetFontSize(14);

            _pathBrowse.SetSize(30, 30);
            _pathBrowse.SetBackground(255, 255, 255, 20);
            _pathBrowse.SetPadding(7, 7, 7, 7);
            _pathBrowse.GetState(ItemStateType.Hovered).Background = Color.FromArgb(150, 255, 255, 255);

            AddItems(
                _topLayout,
                _bottomLayout
            );

            _topLayout.AddItems(
                _expand,
                Name,
                _remove
            );

            _bottomLayout.AddItems(
                _pathLabel,
                _pathEdit,
                _pathBrowse
            );

            _expand.AddItem(_arrow);
            _pathBrowse.AddItem(new ImageItem(DefaultsService.GetDefaultImage(EmbeddedImage.Pencil, EmbeddedImageSize.Size32x32), false));

            //events
            _expand.EventToggle += (sender, args) =>
            {
                _bottomLayout.SetVisible(!_bottomLayout.IsVisible());
                if (_bottomLayout.IsVisible())
                {
                    SetHeight(70);
                    _arrow.SetRotationAngle(90);
                }
                else
                {
                    SetHeight(30);
                    _arrow.SetRotationAngle(180);
                }
            };

            Name.EventMouseDoubleClick += (sender, args) =>
            {
                OnDoubleClick.Invoke(this);
            };

            _remove.EventMouseClick += (sender, args) =>
            {
                Remove();
            };
        }

        public void Remove()
        {
            GetParent().RemoveItem(this);
        }
    }
}
