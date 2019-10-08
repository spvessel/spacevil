using System.Drawing;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace ToolTipExample
{
    public class MainWindow : ActiveWindow
    {
        public override void InitWindow()
        {
            // apply new style for ToolTipItem
            InitToolTipStyle();
            // window attr
            SetParameters("MainWindow", "ToolTipExample", 800, 450);
            IsCentered = true;

            // create toolbars
            HorizontalStack toolBar = ItemsFactory.GetToolBarLayout();
            VerticalStack sideBar = ItemsFactory.GetSideBarLayout();

            // add items to window
            AddItems(
                toolBar,
                sideBar,
                ItemsFactory.GetAreaForPermanentToolTip()
            );

            // 1. add items with standard tooltip to ToolBar
            toolBar.AddItems(
                ItemsFactory.GetTool(DefaultsService.GetDefaultImage(EmbeddedImage.File, EmbeddedImageSize.Size32x32), "Create a new file"),
                ItemsFactory.GetTool(DefaultsService.GetDefaultImage(EmbeddedImage.Folder, EmbeddedImageSize.Size32x32), "Create a new folder"),
                ItemsFactory.GetTool(DefaultsService.GetDefaultImage(EmbeddedImage.RecycleBin, EmbeddedImageSize.Size32x32), "Delete"),
                ItemsFactory.GetTool(DefaultsService.GetDefaultImage(EmbeddedImage.Refresh, EmbeddedImageSize.Size32x32), "Refresh")
            );

            // 2. add items with MyToolTip to sideBar
            sideBar.AddItems(
                ItemsFactory.GetSideTool(DefaultsService.GetDefaultImage(EmbeddedImage.File, EmbeddedImageSize.Size32x32),
                    new MyToolTip(this, "Create new file:\nCreates a new file in the root directory.")),
                ItemsFactory.GetHorizontalDivider(), // divider
                ItemsFactory.GetSideTool(DefaultsService.GetDefaultImage(EmbeddedImage.Folder, EmbeddedImageSize.Size32x32),
                    new MyToolTip(this, "Create new folder:\nCreates a new folder in the root directory.")),
                ItemsFactory.GetHorizontalDivider(), // divider
                ItemsFactory.GetSideTool(DefaultsService.GetDefaultImage(EmbeddedImage.RecycleBin, EmbeddedImageSize.Size32x32),
                    new MyToolTip(this, "Delete:\nDelete selected file entry in root directory.")),
                ItemsFactory.GetHorizontalDivider(), // divider
                ItemsFactory.GetSideTool(DefaultsService.GetDefaultImage(EmbeddedImage.Refresh, EmbeddedImageSize.Size32x32),
                    new MyToolTip(this, "Refresh:\nUpdates the root directory."))
            );
        }

        private void InitToolTipStyle()
        {
            // create style for ToolTipItem
            Style style = Style.GetToolTipStyle();
            style.SetBorder(new Border(Color.White, new CornerRadius(0, 5, 0, 5), 1));
            style.Background = Color.FromArgb(180, 180, 180);
            style.Foreground = Color.Black;
            style.Font = DefaultsService.GetDefaultFont(13);
            style.SetShadow(new Shadow(5, 0, 3, Color.FromArgb(150, 0, 0, 0)));
            style.IsShadowDrop = true;
            Style textStyle = style.GetInnerStyle("text");
            if (textStyle != null)
                textStyle.SetMargin(25, 5, 10, 5);

            // manage tooltip via static ToolTip class
            ToolTip.SetStyle(this, style);
            ToolTip.AddItems(this, ItemsFactory.GetDecor());
        }
    }
}
