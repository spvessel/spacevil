using System;
using System.Drawing;
using SpaceVIL.Common;
using SpaceVIL.Decorations;
using SpaceVIL.Core;

namespace SimpleImageViewer
{
    public static class CustomStyles
    {
        public static Style GetButtonStyle()
        {
            Style style = Style.GetButtonCoreStyle();
            style.Background = Color.Transparent;
            style.SetSize(30, 30);
            style.SetPadding(6, 6, 6, 6);
            style.GetState(ItemStateType.Hovered).Background = Color.FromArgb(20, 255, 255, 255);
            return style;
        }

        public static void InitWparGridStyle()
        {
            Style style = Style.GetWrapGridStyle();
            Style wrap_style = style.GetInnerStyle("area");
            Style wrapper_style = wrap_style.GetInnerStyle("selection");
            wrapper_style.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(215, 91, 225, 152)));
            wrapper_style.AddItemState(ItemStateType.Toggled, new ItemState(Color.FromArgb(91, 225, 152)));
            DefaultsService.GetDefaultTheme().ReplaceDefaultItemStyle(typeof(SpaceVIL.WrapGrid), style);
        }
    }
}
