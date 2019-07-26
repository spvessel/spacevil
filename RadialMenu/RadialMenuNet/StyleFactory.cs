using System.Drawing;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Decorations;
using SpaceVIL.Core;

namespace RadialMenu
{
    public static class StyleFactory
    {
        public static readonly Color CommonBackground = Color.FromArgb(60, 60, 60);
        public static readonly Color ContactBackground = Color.FromArgb(55, 55, 55);

        public static Style GetRadialMenuDefaultStyle()
        {
            Style style = Style.GetDefaultCommonStyle();

            style.SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);
            style.SetBackground(0, 0, 0, 120);
            style.IsVisible = false;

            Style hideStyle = Style.GetButtonCoreStyle();
            hideStyle.SetPadding(15, 20, 15, 5);
            hideStyle.SetBackground(150, 150, 150);
            hideStyle.SetSize(60, 60);
            hideStyle.SetBorder(new Border(Color.Transparent, new CornerRadius(30), 0));
            hideStyle.SetShadow(new Shadow(5, 3, 3, Color.Black));
            hideStyle.IsShadowDrop = true;
            style.AddInnerStyle("hidebutton", hideStyle);

            Style addStyle = Style.GetButtonCoreStyle();
            addStyle.Font = DefaultsService.GetDefaultFont(FontStyle.Bold, 12);
            addStyle.SetBackground(100, 200, 130);
            addStyle.SetSize(50, 30);
            addStyle.SetBorder(new Border(Color.Transparent, new CornerRadius(15), 0));
            addStyle.SetShadow(new Shadow(5, 3, 3, Color.Black));
            addStyle.IsShadowDrop = true;
            ItemState hover = new ItemState(Color.FromArgb(30, 255, 255, 255));
            hover.Border = new Border(Color.White, new CornerRadius(15), 2);
            addStyle.AddItemState(ItemStateType.Hovered, hover);
            style.AddInnerStyle("addbutton", addStyle);

            return style;
        }

        public static Style GetContactFaceStyle()
        {
            Style style = Style.GetButtonCoreStyle();
            style.SetBackground(120, 120, 120);
            style.SetBackground(5, 162, 232);
            style.SetAlignment(ItemAlignment.HCenter, ItemAlignment.VCenter);
            style.SetPadding(8, 8, 8, 8);
            style.Shape = GraphicsMathService.GetEllipse(30, 30);
            style.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(60, 255, 255, 255)));
            style.AddItemState(ItemStateType.Pressed, new ItemState(Color.FromArgb(60, 0, 0, 0)));

            return style;
        }

        public static Style GetMenuItemStyle()
        {
            Style style = Style.GetMenuItemStyle();
            style.BorderRadius = new CornerRadius(3);
            style.SetForeground(32, 32, 32);
            style.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(30, 0, 0, 0)));
            return style;
        }

        public static Style GetMenuStyle()
        {
            Style style = Style.GetContextMenuStyle();
            style.SetBackground(210, 210, 210);
            style.BorderRadius = new CornerRadius(5);
            style.SetShadow(new Shadow(5, 2, 2, Color.FromArgb(180, 0, 0, 0)));
            return style;
        }

        public static Style GetRoundedButtonStyle()
        {
            Style style = Style.GetButtonCoreStyle();
            style.SetAlignment(ItemAlignment.VCenter, ItemAlignment.HCenter);
            style.SetSize(50, 50);
            style.SetShadow(new Shadow(8, 0, 0, Color.Black));
            style.IsShadowDrop = true;
            style.SetBorder(new Border(Color.Transparent, new CornerRadius(25), 0));
            return style;
        }

        public static Style GetContactStyle()
        {
            Style style = Style.GetDefaultCommonStyle();
            style.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            style.Background = Color.Transparent;

            Style layout = Style.GetVerticalStackStyle();
            layout.SetSpacing(0, 5);
            style.AddInnerStyle("layout", layout);

            Style face = StyleFactory.GetContactFaceStyle();
            style.AddInnerStyle("face", face);

            Style name = Style.GetLabelStyle();
            name.Font = DefaultsService.GetDefaultFont(12);
            name.Background = StyleFactory.ContactBackground;
            name.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            name.SetSize(26, 26);
            name.BorderRadius = new CornerRadius(name.Width / 2);
            name.SetAlignment(ItemAlignment.HCenter);
            name.SetTextAlignment(ItemAlignment.HCenter, ItemAlignment.VCenter);
            name.SetShadow(new Shadow(5, 0, 0, Color.Black));
            name.IsShadowDrop = true;
            style.AddInnerStyle("name", name);

            Style notification = Style.GetLabelStyle();
            notification.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            notification.SetTextAlignment(ItemAlignment.HCenter, ItemAlignment.VCenter);
            notification.SetAlignment(ItemAlignment.Right, ItemAlignment.Top);
            notification.SetSize(20, 20);
            notification.SetBorder(new Border(Color.White, new CornerRadius(10), 1));
            notification.Background = StyleFactory.ContactBackground;
            notification.Font = DefaultsService.GetDefaultFont(FontStyle.Bold, 12);
            style.AddInnerStyle("notification", notification);

            return style;
        }
    }
}