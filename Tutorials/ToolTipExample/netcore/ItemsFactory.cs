using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace ToolTipExample
{
    public static class ItemsFactory
    {
        public static HorizontalStack GetToolBarLayout()
        {
            HorizontalStack layout = new HorizontalStack();
            layout.SetHeightPolicy(SizePolicy.Fixed);
            layout.SetHeight(40);
            layout.SetBackground(35, 35, 35);
            layout.SetSpacing(3, 0);
            layout.SetPadding(10, 0, 0, 0);
            return layout;
        }

        public static VerticalStack GetSideBarLayout()
        {
            VerticalStack layout = new VerticalStack();
            layout.SetWidthPolicy(SizePolicy.Fixed);
            layout.SetWidth(40);
            layout.SetBackground(35, 35, 35);
            layout.SetMargin(0, 55, 0, 55);
            layout.SetContentAlignment(ItemAlignment.VCenter);
            return layout;
        }

        public static Label GetAreaForPermanentToolTip()
        {
            Label area = new Label();
            area.SetTextAlignment(ItemAlignment.VCenter, ItemAlignment.HCenter);
            area.SetMargin(55, 55, 55, 55);
            area.SetBackground(45, 45, 45);
            area.SetFontSize(25);
            area.SetText("ToolTip area.\nWhen the mouse cursor inside this item\nthe tooltip is always displayed.");
            area.SetBorder(new Border(Color.FromArgb(50, 50, 50), new CornerRadius(), 1));
            area.SetToolTip("It is the tooltip area.\nWhen the mouse cursor inside this item\nthe tooltip is always displayed.");
            area.EventMouseHover += (sender, args) =>
            {
                ToolTip.SetTimeOut(area.GetHandler(), 0);
            };
            area.EventMouseLeave += (sender, args) =>
            {
                ToolTip.SetTimeOut(area.GetHandler(), 300);
            };
            return area;
        }

        public static Prototype GetTool(Bitmap icon, String tooltip)
        {
            Tool tool = new Tool(icon, tooltip);
            return tool;
        }

        public static Prototype GetSideTool(Bitmap icon, MyToolTip tooltip)
        {
            Tool tool = new Tool(icon);
            tool.SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            tool.SetHeight(40);
            tool.SetPadding(9, 9, 9, 9);
            tool.EventMouseHover += (sender, args) =>
            {
                tooltip.Show(sender, args);
            };
            tool.EventMouseLeave += (sender, args) =>
            {
                tooltip.Hide();
            };
            return tool;
        }

        public static IBaseItem GetHorizontalDivider()
        {
            SpaceVIL.Rectangle divider = new SpaceVIL.Rectangle();
            divider.SetHeight(1);
            divider.SetWidthPolicy(SizePolicy.Expand);
            divider.SetMargin(5, 10, 5, 10);
            divider.SetBackground(100, 100, 100);
            return divider;
        }

        public static IBaseItem GetVerticalDivider()
        {
            SpaceVIL.Rectangle divider = new SpaceVIL.Rectangle();
            divider.SetWidth(2);
            divider.SetHeightPolicy(SizePolicy.Expand);
            divider.SetMargin(0, 0, 0, 0);
            divider.SetBackground(100, 100, 100);
            return divider;
        }

        public static IBaseItem GetDecor()
        {
            Ellipse ellipse = new Ellipse(12);
            ellipse.SetSize(8, 8);
            ellipse.SetAlignment(ItemAlignment.VCenter, ItemAlignment.Left);
            ellipse.SetBackground(169, 89, 213);
            ellipse.SetMargin(5, 0, 0, 0);
            return ellipse;
        }
    }
}