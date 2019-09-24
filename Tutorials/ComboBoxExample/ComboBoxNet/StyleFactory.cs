using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace ComboBoxExample
{
    internal static class StyleFactory
    {
        internal static Style GetMenuItemStyle()
        {
            // Get current style of an item and change it
            Style style = Style.GetMenuItemStyle();
            style.SetBackground(255, 255, 255, 7);
            style.Foreground = Color.FromArgb(210, 210, 210);
            style.BorderRadius = new CornerRadius(3);
            style.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(45, 255, 255, 255)));
            return style;
        }

        internal static Style GetComboBoxDropDownStyle()
        {
            // Get current style of an item and change it
            Style style = Style.GetComboBoxDropDownStyle();
            style.SetBackground(50, 50, 50);
            style.SetBorder(new Border(Color.FromArgb(255, 100, 100, 100), new CornerRadius(0, 0, 5, 5), 1));
            style.SetShadow(new Shadow(10, 3, 3, Color.FromArgb(150, 0, 0, 0)));
            style.IsShadowDrop = true;
            return style;
        }

        internal static Style GetComboBoxStyle()
        {
            // Get current style of an item and change it
            Style style = Style.GetComboBoxStyle();
            style.SetBackground(45, 45, 45);
            style.SetForeground(210, 210, 210);
            style.SetBorder(new Border(Color.FromArgb(255, 255, 181, 111), new CornerRadius(10, 0, 0, 10), 2));
            style.SetShadow(new Shadow(10, 3, 3, Color.FromArgb(150, 0, 0, 0)));
            style.IsShadowDrop = true;

            // Note: every complex item has a few inner styles for its children
            // for example ComboBox has drop down area, selection item, drob down button (with arrow)
            // Replace inner style
            style.RemoveInnerStyle("dropdownarea");
            Style dropDownAreaStyle = GetComboBoxDropDownStyle(); // Get our own style
            style.AddInnerStyle("dropdownarea", dropDownAreaStyle);

            // Change inner style
            Style selectionStyle = style.GetInnerStyle("selection");
            if (selectionStyle != null)
            {
                selectionStyle.BorderRadius = new CornerRadius(10, 0, 0, 10);
                selectionStyle.SetBackground(0, 0, 0, 0);
                selectionStyle.SetPadding(25, 0, 0, 0);
                selectionStyle.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(20, 255, 255, 255)));
            }

            // Change inner style
            Style dropDownButtonStyle = style.GetInnerStyle("dropdownbutton");
            if (dropDownButtonStyle != null)
                dropDownButtonStyle.BorderRadius = new CornerRadius(0, 0, 0, 10);

            return style;
        }

        internal static Style GetBluePopUpStyle()
        {
            // Get current style of an item and change it
            Style style = Style.GetPopUpMessageStyle();
            style.SetBackground(10, 162, 232);
            style.SetForeground(0, 0, 0);
            style.Height = 60;
            style.BorderRadius = new CornerRadius(12);
            style.SetAlignment(ItemAlignment.Bottom, ItemAlignment.HCenter);
            style.SetMargin(0, 0, 0, 50);

            // Change inner style
            Style closeButtonStyle = style.GetInnerStyle("closebutton");
            if (closeButtonStyle != null)
            {
                closeButtonStyle.SetBackground(10, 10, 10, 255);
                closeButtonStyle.AddItemState(ItemStateType.Hovered, new ItemState(Color.White));
            }

            return style;
        }

        internal static Style GetDarkPopUpStyle()
        {
            // Get current style of an item and change it
            Style style = Style.GetPopUpMessageStyle();
            style.Height = 60;
            style.SetAlignment(ItemAlignment.Bottom, ItemAlignment.HCenter);
            style.SetMargin(0, 0, 0, 50);
            return style;
        }
    }
}