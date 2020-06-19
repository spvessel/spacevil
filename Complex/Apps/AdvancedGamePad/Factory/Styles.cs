using System.Drawing;
using AdvancedGamePad.View;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace AdvancedGamePad.Factory
{
    public static class Styles
    {
        public static void ReplaceBasicStyles()
        {
            DefaultsService.GetDefaultTheme().ReplaceDefaultItemStyle(typeof(ComboBox), GetComboBoxListStyle());
            DefaultsService.GetDefaultTheme().ReplaceDefaultItemStyle(typeof(MenuItem), GetMenuItemStyle());
            DefaultsService.GetDefaultTheme().ReplaceDefaultItemStyle(typeof(Label), GetLabelStyle());
            DefaultsService.GetDefaultTheme().ReplaceDefaultItemStyle(typeof(TabView), GetTabViewStyle());
            DefaultsService.GetDefaultTheme().ReplaceDefaultItemStyle(typeof(Tab), GetTabStyle());
            DefaultsService.GetDefaultTheme().ReplaceDefaultItemStyle(typeof(TextEdit), GetTextEditStyle());
            DefaultsService.GetDefaultTheme().ReplaceDefaultItemStyle(typeof(MessageItem), GetMessageItemStyle());
            DefaultsService.GetDefaultTheme().ReplaceDefaultItemStyle(typeof(InputDialog), GetInputDialogStyle());
            DefaultsService.GetDefaultTheme().AddDefaultCustomItemStyle(typeof(ManualKeyBinder), GetManualKeyBinderStyle());
        }

        public static Style GetComboBoxListStyle()
        {
            Style style = Style.GetComboBoxStyle();
            style.Font = DefaultsService.GetDefaultFont(16);
            style.Background = Palette.Gray;
            style.AddItemState(ItemStateType.Hovered, new ItemState(Palette.Hover));
            style.SetShadow(new Shadow(5, 0, 2, Palette.Shadow));
            style.IsShadowDrop = true;

            Style dropdownStyle = style.GetInnerStyle("dropdownarea");
            dropdownStyle.Background = Palette.LightGray;
            dropdownStyle.SetShadow(new Shadow(5, 0, 2, Palette.Shadow));
            dropdownStyle.IsShadowDrop = true;
            dropdownStyle.SetPadding(new Indents(-2, 0, -2, 0));

            Style selectionStyle = dropdownStyle.GetInnerStyle("itemlist")
                                                .GetInnerStyle("area")
                                                .GetInnerStyle("selection");
            selectionStyle.AddItemState(ItemStateType.Toggled, new ItemState(Color.Transparent));

            return style;
        }

        public static Style GetMenuItemStyle()
        {
            Style style = Style.GetMenuItemStyle();
            style.Height = 30;
            style.AddItemState(ItemStateType.Hovered, new ItemState(Palette.ProfileSelection));

            return style;
        }

        public static Style GetLabelStyle()
        {
            Style style = Style.GetLabelStyle();
            style.Font = DefaultsService.GetDefaultFont(16);
            style.SetTextAlignment(ItemAlignment.VCenter, ItemAlignment.Right);
            style.Padding = new Indents(0, 0, 6, 0);
            return style;
        }

        public static Style GetTabViewStyle()
        {
            Style style = Style.GetTabViewStyle();
            style.Background = Palette.CommonDark;

            Style tabBarStyle = style.GetInnerStyle("tabbar");
            tabBarStyle.Height = 36;

            return style;
        }

        public static Style GetTabStyle()
        {
            Style style = Style.GetTabStyle();
            style.Background = Palette.Dark;

            style.BorderRadius = new CornerRadius();
            style.SetPadding(0, 0, 0, 0);
            style.SetTextAlignment(ItemAlignment.HCenter, ItemAlignment.VCenter);
            style.Font = DefaultsService.GetDefaultFont(FontStyle.Bold, 18);
            style.AddItemState(ItemStateType.Toggled, new ItemState(Color.FromArgb(70, 70, 70)));

            Style viewStyle = style.GetInnerStyle("view");
            viewStyle.Background = Palette.Background;
            viewStyle.Margin = new Indents(0, 1, 0, 0);

            return style;
        }

        public static Style GetTextEditStyle()
        {
            Style style = Style.GetTextEditStyle();
            Style text = style.GetInnerStyle("text");
            text.AddItemState(ItemStateType.Hovered, new ItemState(Palette.Hover));
            text.AddItemState(ItemStateType.Focused, new ItemState(Palette.KeyBindFocused));
            return style;
        }

        public static Style GetMessageItemStyle()
        {
            Style style = Style.GetMessageItemStyle();

            Style window = style.GetInnerStyle("window");
            window.Width = 360;

            Style btn = style.GetInnerStyle("button");
            btn.Background = Palette.ButtonBase;
            btn.Width = 130;

            Style message = style.GetInnerStyle("message");
            message.Font = DefaultsService.GetDefaultFont(14);
            message.SetTextAlignment(ItemAlignment.HCenter, ItemAlignment.VCenter);

            return style;
        }

        public static Style GetInputDialogStyle()
        {
            Style style = Style.GetInputDialogStyle();

            Style window = style.GetInnerStyle("window");
            window.Width = 300;

            Style btn = style.GetInnerStyle("button");
            btn.Background = Palette.ButtonBase;
            btn.Width = 130;

            return style;
        }

        public static Style GetManualKeyBinderStyle()
        {
            Style style = new Style();
            style.SetAlignment(ItemAlignment.HCenter, ItemAlignment.VCenter);
            style.SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);
            style.SetBackground(0, 0, 0, 150);
            style.BorderRadius = new CornerRadius();
            style.Padding = new Indents();
            style.Margin = new Indents();
            style.Spacing = new Spacing();

            Style windowStyle = Style.GetFrameStyle();
            windowStyle.SetAlignment(ItemAlignment.HCenter, ItemAlignment.VCenter);
            windowStyle.SetPadding(0, 0, 0, 0);
            windowStyle.SetMargin(100, 150, 100, 150);
            windowStyle.SetBackground(45, 45, 45);
            windowStyle.SetShadow(new Shadow(10, 0, 0, Color.Black));
            windowStyle.IsShadowDrop = true;
            style.AddInnerStyle("window", windowStyle);

            Style closeStyle = new Style();
            closeStyle.Background = Color.FromArgb(255, 100, 100, 100);
            closeStyle.SetSize(20, 20);
            closeStyle.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            closeStyle.Alignment = ItemAlignment.Top | ItemAlignment.Right;
            closeStyle.SetMargin(0, 10, 10, 0);
            closeStyle.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(186, 95, 97)));
            closeStyle.Shape = GraphicsMathService.GetCross(20, 20, 3, 45);
            closeStyle.IsFixedShape = true;
            style.AddInnerStyle("closebutton", closeStyle);

            return style;
        }

        public static Style GetContextMenuItemStyle()
        {
            Style style = Style.GetMenuItemStyle();
            style.GetInnerStyle("text").SetMargin(25, 0, 0, 0);
            style.SetForeground(210, 210, 210);
            style.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(30, 255, 255, 255)));

            return style;
        }
    }
}