using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace ComboBoxExample
{
    internal static class ItemsFactory
    {
        internal static MenuItem GetMenuItem(String name, Bitmap bitmap)
        {
            MenuItem menuItem = new MenuItem(name);
            menuItem.SetStyle(StyleFactory.GetMenuItemStyle());
            menuItem.SetTextMargin(new Indents(25, 0, 0, 0));


            // Optionally: set an event on click
            menuItem.EventMouseClick += (sender, args) =>
            {
                PopUpMessage popUpInfo = new PopUpMessage("You choosed a function:\n" + menuItem.GetText());
                popUpInfo.SetStyle(StyleFactory.GetBluePopUpStyle());
                popUpInfo.SetTimeOut(2000);
                popUpInfo.Show(menuItem.GetHandler());
            };

            // Optionally: add an image into MenuItem
            ImageItem img = new ImageItem(bitmap, false);
            img.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            img.SetSize(20, 20);
            img.SetAlignment(ItemAlignment.Left, ItemAlignment.VCenter);
            img.KeepAspectRatio(true);
            menuItem.AddItem(img);


            // Optionally: add a button into MenuItem
            ButtonCore infoBtn = new ButtonCore("?");
            infoBtn.SetBackground(40, 40, 40);
            infoBtn.SetWidth(20);
            infoBtn.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Expand);
            infoBtn.SetFontStyle(FontStyle.Bold);
            infoBtn.SetForeground(210, 210, 210);
            infoBtn.SetAlignment(ItemAlignment.VCenter, ItemAlignment.Right);
            infoBtn.SetMargin(0, 0, 10, 0);
            infoBtn.SetBorderRadius(3);
            infoBtn.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(0, 140, 210)));
            infoBtn.SetPassEvents(false, InputEventType.MousePress, InputEventType.MouseRelease, InputEventType.MouseDoubleClick);
            infoBtn.IsFocusable = false; // prevent focus this button
            infoBtn.EventMouseClick += (sender, args) =>
            {
                PopUpMessage popUpInfo = new PopUpMessage("This is decorated MenuItem:\n" + menuItem.GetText());
                popUpInfo.SetStyle(StyleFactory.GetDarkPopUpStyle());
                popUpInfo.SetTimeOut(2000);
                popUpInfo.Show(infoBtn.GetHandler());
            };
            menuItem.AddItem(infoBtn);

            return menuItem;
        }

        internal static Ellipse GetDot()
        {
            Ellipse ellipse = new Ellipse(12);
            ellipse.SetSize(8, 8);
            ellipse.SetAlignment(ItemAlignment.VCenter, ItemAlignment.Left);
            ellipse.SetMargin(10, 0, 0, 0);
            return ellipse;
        }
    }
}