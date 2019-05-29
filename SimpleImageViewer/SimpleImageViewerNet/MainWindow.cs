using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Common;
using SpaceVIL.Decorations;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;

namespace SimpleImageViewer
{
    public class MainWindow : ActiveWindow
    {
        WrapGrid imageArea;
        PreviewArea previewArea;
        AlbumSideList side;

        public override void InitWindow()
        {
            SetParameters(nameof(MainWindow), nameof(MainWindow), 1240, 750, false);
            IsCentered = true;

            var icon = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("SimpleImageViewer.Resources.icon.png"));

            TitleBar title = new TitleBar("Simple Image Viewer - C#");
            title.SetIcon(icon, 24, 24);
            title.SetPadding(4, 0, 5, 0);

            Frame layout = new Frame();
            layout.SetMargin(0, title.GetHeight(), 0, 0);
            layout.SetPadding(0, 0, 0, 0);
            layout.SetSpacing(6);
            layout.SetBackground(60, 60, 60);

            VerticalStack vToolbar = new VerticalStack();
            vToolbar.SetWidthPolicy(SizePolicy.Fixed);
            vToolbar.SetWidth(30);
            vToolbar.SetBackground(32, 32, 32);
            vToolbar.SetPadding(0, 30, 0, 0);

            HorizontalSplitArea splitArea = new HorizontalSplitArea();
            splitArea.SetMargin(vToolbar.GetWidth(), 0, 0, 0);
            splitArea.SetSplitThickness(5);

            imageArea = new WrapGrid(160, 120, Orientation.Horizontal);
            imageArea.SetBackground(Color.Transparent);
            imageArea.SetPadding(15, 6, 6, 6);
            imageArea.GetArea().SetSpacing(6, 6);
            imageArea.VScrollBar.SetStyle(Style.GetSimpleVerticalScrollBarStyle());
            imageArea.SetStretch(true);

            previewArea = new PreviewArea(imageArea);

            side = new AlbumSideList(this, Side.Left, imageArea, previewArea);
            side.SetAreaSize(400);
            Album defaultAlbum = new Album("MyPictures", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + Path.DirectorySeparatorChar + "Pictures");
            defaultAlbum.OnDoubleClick += (sender) =>
            {
                side.Hide();
                Model.FillImageArea(this, imageArea, previewArea, (sender as Album).GetPath());
            };
            side.AddAlbum(defaultAlbum);

            ButtonCore btnSideAreaShow = new ButtonCore();
            btnSideAreaShow.SetStyle(CustomStyles.GetButtonStyle());

            ButtonCore btnOpenFolder = new ButtonCore();
            btnOpenFolder.SetStyle(CustomStyles.GetButtonStyle());

            ButtonCore btnAddAlbum = new ButtonCore();
            btnAddAlbum.SetStyle(CustomStyles.GetButtonStyle());

            //adding
            AddItems(
                title,
                layout
                );
            layout.AddItems(
                vToolbar,
                splitArea
            );

            splitArea.AssignTopItem(previewArea);
            splitArea.AssignBottomItem(imageArea);
            splitArea.SetSplitPosition(300);

            vToolbar.AddItems(
                btnSideAreaShow,
                btnOpenFolder,
                btnAddAlbum
            );

            btnSideAreaShow.AddItem(new ImageItem(DefaultsService.GetDefaultImage(EmbeddedImage.Lines, EmbeddedImageSize.Size32x32), false));
            btnOpenFolder.AddItem(new ImageItem(DefaultsService.GetDefaultImage(EmbeddedImage.Folder, EmbeddedImageSize.Size32x32), false));
            btnAddAlbum.AddItem(new ImageItem(DefaultsService.GetDefaultImage(EmbeddedImage.Import, EmbeddedImageSize.Size32x32), false));

            //events
            EventDrop += (sender, args) =>
            {
                if (side.IsHide)
                {
                    if (File.GetAttributes(args.Paths[0]).HasFlag(FileAttributes.Directory))
                        Model.FillImageArea(this, imageArea, previewArea, args.Paths[0]);
                }
                else
                {
                    foreach (String path in args.Paths)
                    {
                        if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
                        {
                            Album album = new Album(new DirectoryInfo(path).Name, path);
                            side.AddAlbum(album);
                            album.OnDoubleClick += (s) =>
                            {
                                side.Hide();
                                Model.FillImageArea(this, imageArea, previewArea, (s as Album).GetPath());
                            };
                        }
                    }
                }
            };

            btnSideAreaShow.EventMouseClick += (sender, args) =>
            {
                side.Show();
            };

            btnOpenFolder.EventMouseClick += (sender, args) =>
            {
                OpenEntryDialog browse = new OpenEntryDialog("Open Folder:", FileSystemEntryType.Directory, OpenDialogType.Open);
                browse.OnCloseDialog += () =>
                {
                    Model.FillImageArea(this, imageArea, previewArea, browse.GetResult());
                };
                browse.Show(this);
            };

            imageArea.EventScrollUp += (sender, args) =>
            {
                if (args.Mods.HasFlag(KeyMods.Control))
                {
                    int w = imageArea.GetCellWidth() + 8;
                    int h = imageArea.GetCellHeight() + 6;
                    if (w > 400)
                        return;
                    imageArea.SetCellSize(w, h);
                }
            };
            imageArea.EventScrollDown += (sender, args) =>
            {
                if (args.Mods.HasFlag(KeyMods.Control))
                {
                    int w = imageArea.GetCellWidth() - 8;
                    int h = imageArea.GetCellHeight() - 6;
                    if (w < 160)
                        return;
                    imageArea.SetCellSize(w, h);
                }
            };

            btnAddAlbum.EventMouseClick += (sender, args) =>
            {
                OpenEntryDialog browse = new OpenEntryDialog("Open Folder:", FileSystemEntryType.Directory, OpenDialogType.Open);
                browse.OnCloseDialog += () =>
                {
                    if (browse.GetResult() == null || browse.GetResult() == String.Empty)
                        return;

                    Album album = new Album(new DirectoryInfo(browse.GetResult()).Name, browse.GetResult());
                    side.AddAlbum(album);
                    album.OnDoubleClick += (s) =>
                    {
                        side.Hide();
                        Model.FillImageArea(this, imageArea, previewArea, (s as Album).GetPath());
                    };
                };
                browse.Show(this);
            };

            EventOnStart += () =>
            {
                Model.FillImageArea(this, imageArea, previewArea, 
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + Path.DirectorySeparatorChar + "Pictures");
            }
        }
    }
}
