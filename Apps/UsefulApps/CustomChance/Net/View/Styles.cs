using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace CustomChance
{
    internal static class Styles
    {
        internal static Style GetButtonStyle()
        {
            Style style = new Style();
            style.Background = Color.FromArgb(255, 255, 181, 111);
            style.Foreground = Color.Black;
            style.BorderRadius = new CornerRadius();
            style.Font = new Font(DefaultsService.GetDefaultFont().FontFamily, 14, FontStyle.Bold);
            style.Width = 150;
            style.Height = 30;
            style.WidthPolicy = SizePolicy.Fixed;
            style.HeightPolicy = SizePolicy.Fixed;
            style.Alignment = ItemAlignment.HCenter | ItemAlignment.Top;
            style.TextAlignment = ItemAlignment.HCenter | ItemAlignment.VCenter;
            style.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(30, 255, 255, 255)));
            return style;
        }

        internal static Style GetLabelStyle()
        {
            Style style = Style.GetLabelStyle();
            style.Foreground = Color.FromArgb(255, 210, 210, 210);
            style.Font = new Font(DefaultsService.GetDefaultFont().FontFamily, 14, FontStyle.Bold);
            style.Height = 30;
            style.HeightPolicy = SizePolicy.Fixed;
            style.Alignment = ItemAlignment.VCenter | ItemAlignment.Left;
            style.TextAlignment = ItemAlignment.Left | ItemAlignment.VCenter;
            return style;
        }

        internal static Style GetCommonContainerStyle()
        {
            Style style = new Style();

            style.Background = Color.FromArgb(255, 54, 57, 63);
            style.WidthPolicy = SizePolicy.Expand;
            style.HeightPolicy = SizePolicy.Expand;
            style.Alignment = ItemAlignment.Left | ItemAlignment.Top;

            return style;
        }
    }
}