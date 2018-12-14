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
            lb.SetMargin(20, 0, 0, 0);
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

        internal static VisualContact GetVisualContact(String name, TextEdit input_message)
        {
            VisualContact contact = new VisualContact(name);
            contact.SendMessage.EventMouseClick += (sender, args) =>
            {
                input_message.SetText("Message @" + name);
                input_message.SetFocus();
                input_message.SelectAll();
            };
            return contact;
        }

        internal static NoteBlock GetNoteBlock()
        {
            NoteBlock block = new NoteBlock();
            block.SetSize(250, 200);
            return block;
        }
    }
}