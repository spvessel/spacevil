using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace MimicSpace
{
    internal static class Styles
    {
        internal static Style GetTitleBarStyle()
        {
            Style style = new Style();

            style.Font = new Font(DefaultsService.GetDefaultFont().FontFamily, 14, FontStyle.Bold);
            style.Background = Color.FromArgb(255, 32, 34, 37);
            style.Foreground = Color.FromArgb(255, 166, 167, 168);
            style.Height = 22;
            style.WidthPolicy = SizePolicy.Expand;
            style.HeightPolicy = SizePolicy.Fixed;
            style.Alignment = ItemAlignment.Left | ItemAlignment.Top;
            style.TextAlignment = ItemAlignment.Left | ItemAlignment.VCenter;
            style.Padding = new Indents(5, 4, 10, 0);
            style.Spacing = new Spacing(10);

            Style close_style = new Style();
            close_style.Background = Color.FromArgb(255, 166, 167, 168);
            close_style.SetSize(10, 10);
            close_style.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            close_style.Alignment = ItemAlignment.VCenter | ItemAlignment.Right;
            close_style.AddItemState(ItemStateType.Hovered, new ItemState()
            {
                Background = Color.FromArgb(255, 186, 95, 97)
            });
            close_style.Shape = GraphicsMathService.GetCross(15, 15, 1, 45);
            style.AddInnerStyle("closebutton", close_style);

            Style minimize_style = new Style();
            minimize_style.Background = Color.FromArgb(255, 166, 167, 168);
            minimize_style.SetSize(10, 10);
            minimize_style.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            minimize_style.Alignment = ItemAlignment.VCenter | ItemAlignment.Right;
            minimize_style.AddItemState(ItemStateType.Hovered, new ItemState()
            {
                Background = Color.FromArgb(80, 255, 255, 255)
            });
            minimize_style.Shape = GraphicsMathService.GetRectangle(10, 1, 0, 5);
            minimize_style.IsFixedShape = true;
            style.AddInnerStyle("minimizebutton", minimize_style);

            Style maximize_style = new Style();
            maximize_style.SetSize(10, 10);
            maximize_style.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            maximize_style.BorderThickness = 1;
            maximize_style.BorderFill = Color.FromArgb(255, 166, 167, 168);
            maximize_style.Alignment = ItemAlignment.VCenter | ItemAlignment.Right;
            maximize_style.Padding = new Indents(2, 2, 2, 2);

            ItemState hovered = new ItemState();
            hovered.Border.SetFill(Color.FromArgb(255, 84, 124, 94));
            maximize_style.AddItemState(ItemStateType.Hovered, hovered);
            maximize_style.Shape = GraphicsMathService.GetRectangle();
            style.AddInnerStyle("maximizebutton", maximize_style);

            Style title_style = new Style();
            title_style.Margin = new Indents(10, 0, 0, 0);
            style.AddInnerStyle("title", title_style);

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