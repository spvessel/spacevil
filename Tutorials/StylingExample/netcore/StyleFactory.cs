using System.Drawing;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace StylingExample
{
    public static class StyleFactory
    {
        public static Style GetPictureStyle()
        {
            // style for Picture
            Style style = new Style();
            style.SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);
            style.SetSpacing(0, 5);
            style.SetMargin(3, 3, 3, 3);
            style.SetPadding(5, 5, 5, 5);
            style.SetAlignment(ItemAlignment.Left, ItemAlignment.Top);
            style.SetBackground(80, 80, 80);
            style.SetShadow(new Shadow(5, 0, 0, Color.FromArgb(200, 0, 0, 0)));
            style.IsShadowDrop = true;
            style.SetBorder(Color.Gray, new CornerRadius(10), 1);

            // inner stylesExpand: Picture consist of ImageItem and Label
            // stylHCentere for ImageItemVCenter
            Style imageStyle = new Style();
            imageStyle.SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);
            imageStyle.SetAlignment(ItemAlignment.HCenter, ItemAlignment.VCenter);
            imageStyle.SetBackground(45, 45, 45);
            imageStyle.BorderRadius = new CornerRadius(6, 6, 0, 0);
            style.AddInnerStyle("image", imageStyle);
            // style for Label
            Style textStyle = new Style();
            textStyle.SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            textStyle.Height = 30;
            textStyle.SetTextAlignment(ItemAlignment.HCenter, ItemAlignment.VCenter);
            // textStyle.SetAlignment(ItemAlignment.LEFT, ItemAlignment.BOTTOM);
            textStyle.SetBackground(150, 150, 150);
            textStyle.SetForeground(32, 32, 32);
            textStyle.BorderRadius = new CornerRadius(0, 0, 6, 6);
            style.AddInnerStyle("text", textStyle);
            return style;
        }

        public static void UpdateWrapGridStyle()
        {
            // get style from basic theme
            Style selectionStyle =
                DefaultsService.GetDefaultTheme().GetThemeStyle(typeof(WrapGrid)).GetInnerStyle("area").GetInnerStyle("selection");
            // change style for selection
            if (selectionStyle != null)
            {
                selectionStyle.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(10, 162, 232)));
                selectionStyle.AddItemState(ItemStateType.Toggled, new ItemState(Color.FromArgb(49, 213, 121)));
                selectionStyle.BorderRadius = new CornerRadius(12);
                selectionStyle.SetPadding(0, 0, 0, 0);
            }
        }
    }
}