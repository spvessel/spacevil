using System;
using System.Drawing;
using SpaceVIL.Common;
using SpaceVIL.Decorations;
using SpaceVIL.Core;
using SpaceVIL;
using System.IO;
using System.Collections.Generic;

namespace SimpleImageViewer
{
    public class AlbumSideList : SideArea
    {
        private bool _isInit = false;
        private List<Album> _list = new List<Album>();
        private ListBox _albumList = new ListBox();
        private ButtonCore _addButton = new ButtonCore();

        private WrapGrid _area;
        private PreviewArea _preview;

        public AlbumSideList(CoreWindow handler, Side attachSide, WrapGrid area, PreviewArea preview) : base(handler, attachSide)
        {
            _area = area;
            _preview = preview;
        }

        public override void InitElements()
        {
            base.InitElements();
            if (_isInit)
                return;

            Window.SetBackground(60, 60, 60);

            Label _name = new Label();
            _name.SetFont(DefaultsService.GetDefaultFont(FontStyle.Bold, 28));
            _name.SetText("My Albums");
            _name.SetMargin(0, 0, 0, 0);
            _name.SetMargin(0, 25, 0, 0);
            _name.SetPadding(10, 10, 10, 0);
            _name.SetHeightPolicy(SizePolicy.Fixed);
            _name.SetHeight(70);

            _albumList.SetSelectionVisible(false);
            _albumList.SetVScrollBarVisible(ScrollBarVisibility.AsNeeded);
            _albumList.SetHScrollBarVisible(ScrollBarVisibility.Never);
            _albumList.SetBackground(Color.Transparent);
            _albumList.SetMargin(10, 100, 10, 50);
            _albumList.VScrollBar.SetStyle(Style.GetSimpleVerticalScrollBarStyle());

            Style style = new Style();
            style.Background = Color.Transparent;
            style.Foreground = Color.Black;
            style.Font = DefaultsService.GetDefaultFont();
            style.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            style.SetSize(40, 40);
            style.SetAlignment(ItemAlignment.Right, ItemAlignment.Bottom);
            style.SetTextAlignment(ItemAlignment.Right, ItemAlignment.Bottom);
            style.SetPadding(4, 4, 4, 4);
            style.SetMargin(0, 0, 10, 10);
            style.SetSpacing(0, 0);
            style.SetBorder(new Border(Color.Transparent, new CornerRadius(20), 0));
            style.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(100, 255, 255, 255)));
            style.AddItemState(ItemStateType.Pressed, new ItemState(Color.FromArgb(100, 0, 0, 0)));
            _addButton.SetStyle(style);

            CustomShape plus = new CustomShape(GraphicsMathService.GetCross(30, 30, 4, 0));
            plus.SetAlignment(ItemAlignment.HCenter, ItemAlignment.VCenter);
            plus.SetBackground(100, 100, 100);
            plus.SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);

            AddItems(
                _name,
                _albumList,
                _addButton
            );

            _addButton.AddItem(plus);

            _addButton.EventMouseClick += (sender, args) =>
            {
                Hide();
                OpenEntryDialog browse = new OpenEntryDialog("Open Folder:", FileSystemEntryType.Directory, OpenDialogType.Open);
                browse.OnCloseDialog += () =>
                {
                    if (browse.GetResult() == null || browse.GetResult().Equals(String.Empty))
                    {
                        Show();
                        return;
                    }
                    Album album = new Album(new DirectoryInfo(browse.GetResult()).Name, browse.GetResult());
                    AddAlbum(album);
                    album.OnDoubleClick += (s) =>
                    {
                        Hide();
                        Model.FillImageArea(GetHandler(), _area, _preview, (s as Album).GetPath());
                    };
                };
                browse.Show(GetHandler());
            };

            if (_list.Count > 0)
                foreach (Album item in _list)
                {
                    _albumList.AddItem(item);
                    item._expand.EventToggle += (sender, args) =>
                    {
                        _albumList.GetWrapper(item).UpdateSize();
                        _albumList.GetArea().UpdateLayout();
                    };
                }

            _isInit = true;
        }

        public void AddAlbum(Album album)
        {
            if (_isInit)
            {
                _albumList.AddItem(album);
                album._expand.EventToggle += (sender, args) =>
                {
                    _albumList.GetWrapper(album).UpdateSize();
                    _albumList.GetArea().UpdateLayout();
                };
            }
            else
                _list.Add(album);
        }


        public bool IsHide = true;

        public override void Show()
        {
            IsHide = false;
            base.Show();
        }

        public override void Hide()
        {
            IsHide = true;
            base.Hide();
        }
    }
}
