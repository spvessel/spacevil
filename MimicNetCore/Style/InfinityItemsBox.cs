using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace MimicSpace
{
    internal static class InfinityItemsBox
    {
        internal static ButtonCore GetUserBarButton()
        {
            ButtonCore btn = new ButtonCore();
            btn.SetSize(25, 25);
            btn.SetBackground(54, 57, 63);
            btn.SetBorderRadius(6);
            return btn;
        }

        internal static Label GetMessage(string message)
        {
            Label lb = new Label();
            lb.SetHeight(30);
            lb.SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            lb.SetText(message);
            lb.SetForeground(154, 156, 159);
            lb.SetFont(DefaultsService.GetDefaultFont(16));
            lb.SetMargin(10, 0, 0, 0);
            lb.SetBorderRadius(6);
            lb.SetPadding(10, 0, 0, 0);
            lb.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(64, 67, 73)));
            return lb;
        }

        internal static ButtonCore GetOrdinaryButton()
        {
            ButtonCore btn = new ButtonCore();
            btn.SetSize(30, 30);
            btn.SetBackground(74, 76, 82);
            btn.SetBorderRadius(6);
            btn.SetFont(new Font(DefaultsService.GetDefaultFont().FontFamily, 16, FontStyle.Bold));
            btn.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(30, 255, 255, 255)));
            return btn;
        }

        internal static VisualContact GetVisualContact(String name, TextEdit input)
        {
            VisualContact contact = new VisualContact(name, input);
            contact.SendMessage.EventMouseClick += (sender, args) =>
            {
                input.SetSubstrateText("Message @" + name);
                input.SetFocus();
            };
            return contact;
        }

        internal static NoteBlock GetNoteBlock()
        {
            NoteBlock block = new NoteBlock();
            block.SetSize(250, 200);
            return block;
        }

        internal static MenuItem GetMenuItem(String name)
        {
            MenuItem item = new MenuItem(name);
            item.SetForeground(Color.LightGray);
            item.SetMargin(1, 1, 1, 1);
            item.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(150, 255, 255, 255)));
            item.SetBorderRadius(3);
            return item;
        }
    }
}