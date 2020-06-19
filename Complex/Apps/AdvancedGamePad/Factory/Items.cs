using System.Drawing;
using System.Reflection;

using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Common;

using AdvancedGamePad.View;

namespace AdvancedGamePad.Factory
{
    public static class Items
    {
        public static VerticalStack GetLayout()
        {
            VerticalStack layout = new VerticalStack();
            layout.SetBackground(Palette.LayoutBackground);
            layout.SetPadding(6, 25, 6, 25);
            layout.SetSpacing(0, 15);

            return layout;
        }

        public static HorizontalStack GetFixedHorizontalStack(int height)
        {
            HorizontalStack layout = new HorizontalStack();
            layout.SetContentAlignment(ItemAlignment.HCenter);
            layout.SetSpacing(15, 0);
            layout.SetHeightPolicy(SizePolicy.Fixed);
            layout.SetHeight(30);

            return layout;
        }

        public static TitleBar GetTitleBar(string text)
        {
            TitleBar tb = new TitleBar(text);
            tb.SetBackground(Palette.CommonDark);
            tb.GetMaximizeButton().SetVisible(false);
            tb.SetIcon(Factory.Resources.TitleBarIcon, 24, 24);

            return tb;
        }

        public static Button GetButton(string text)
        {
            Button btn = new Button(text);
            btn.SetBackground(Palette.ButtonBase);
            btn.SetHeight(30);
            btn.SetWidthPolicy(SizePolicy.Expand);
            btn.SetFontStyle(FontStyle.Bold);
            btn.SetShadow(5, 0, 2, Palette.Shadow);
            btn.SetText(text);

            return btn;
        }

        public static ImagedButton GetImagedButton(Bitmap image)
        {
            ImagedButton btn = new ImagedButton(image);
            btn.SetBackground(Palette.ButtonBase);
            btn.SetSize(30, 30);
            btn.SetShadow(5, 0, 2, Palette.Shadow);

            return btn;
        }

        public static Button GetControlButton(string text)
        {
            Button btn = new Button(text);
            btn.SetBackground(Palette.ButtonBase);
            btn.SetSize(150, 30);
            btn.SetAlignment(ItemAlignment.VCenter, ItemAlignment.Right);
            btn.SetMargin(0, 0, 20, 0);
            btn.SetFontStyle(FontStyle.Bold);
            btn.SetShadow(5, 0, 2, Palette.Shadow);
            btn.SetText(text);

            return btn;
        }

        public static ComboBox GetProfileList()
        {
            ComboBox cb = new ComboBox();

            return cb;
        }

        public static MenuItem GetListItem(string text)
        {
            MenuItem mi = new MenuItem(text);
            mi.SetFont(DefaultsService.GetDefaultFont(16));

            return mi;
        }

        public static ImageItem GetBatteryItem()
        {
            ImageItem img = new ImageItem(Factory.Resources.BatteryEmptyIcon);
            img.SetMargin(0, 10, 0, 20);
            img.SetHeightPolicy(SizePolicy.Fixed);
            img.SetHeight(50);
            img.KeepAspectRatio(true);

            return img;
        }

        public static SpaceVIL.Rectangle GetUnderline()
        {
            SpaceVIL.Rectangle line = new SpaceVIL.Rectangle();
            line.SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            line.SetHeight(3);
            line.SetBackground(Palette.Underline);
            line.SetAlignment(ItemAlignment.Bottom, ItemAlignment.HCenter);
            line.SetVisible(false);

            return line;
        }

        public static Label GetHeaderLabel(string text)
        {
            Label label = new Label(text);
            label.SetFontStyle(FontStyle.Bold);
            label.SetFontSize(20);
            label.SetTextAlignment(ItemAlignment.Left, ItemAlignment.VCenter);
            label.SetHeightPolicy(SizePolicy.Fixed);
            label.SetHeight(30);
            label.SetMargin(10, 0, 0, 0);

            return label;
        }

        public static ImagedCheckBox GetSwitcher(string text, bool value = false)
        {
            ImagedCheckBox imagedCheckBox = new ImagedCheckBox(text, value);
            imagedCheckBox.SetMargin(16, 0, 0, 0);

            return imagedCheckBox;
        }

        public static IBaseItem GetHorizontalDivider()
        {
            SpaceVIL.Rectangle divider = new SpaceVIL.Rectangle();
            divider.SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            divider.SetHeight(1);
            divider.SetBackground(Palette.Divider);
            divider.SetAlignment(ItemAlignment.VCenter, ItemAlignment.HCenter);
            divider.SetMargin(0, 10, 0, 20);

            return divider;
        }

        public static ImageItem GetImageMenuItem(Bitmap bitmap, Color overlay)
        {
            ImageItem image = new ImageItem(bitmap, false);
            image.KeepAspectRatio(true);
            image.SetAlignment(ItemAlignment.Left, ItemAlignment.VCenter);
            image.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            image.SetSize(20, 20);
            image.SetColorOverlay(overlay);
            image.SetMargin(-5, 0, 0, 0);

            return image;
        }

        public static System.Windows.Forms.NotifyIcon GetBallonToolTip(string text)
        {
            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.BalloonTipText = text;
            ni.BalloonTipTitle = "Advanced Gamepad";
            ni.Text = "Advanced Gamepad";
            ni.Icon = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Images.icon.ico"));
            ni.Visible = true;

            return ni;
        }

        public static System.Windows.Forms.ContextMenu GetTrayContextMenu(params System.Windows.Forms.MenuItem[] items)
        {
            System.Windows.Forms.ContextMenu balloonContextMenu = new System.Windows.Forms.ContextMenu();
            balloonContextMenu.MenuItems.AddRange(items);

            return balloonContextMenu;
        }

        public static System.Windows.Forms.MenuItem GetContextMenuItem(string text, int index)
        {
            System.Windows.Forms.MenuItem mi = new System.Windows.Forms.MenuItem();
            mi.Text = text;
            mi.Index = index;

            return mi;
        }
    }
}