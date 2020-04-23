using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SpaceVIL;
using System.Drawing;
using SpaceVIL.Core;
using SpaceVIL.Decorations;
using SpaceVIL.Common;

namespace MimicSpace
{
    class UserBar : Prototype
    {
        public bool IsChecked = false;
        static int _count = 0;
        private string name = String.Empty;
        Ellipse border;
        Label contact;
        BlankItem signal;

        public UserBar(string name)
        {
            this.name = name;
            SetItemName("UB_" + _count);
            SetBackground(Color.Transparent);
            SetMinSize(40, 40);
            SetSize(40, 40);
            SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            _count++;
        }

        public override void InitElements()
        {
            border = new Ellipse();
            contact = new Label();
            signal = new BlankItem();

            //contact image border
            border.SetBackground(114, 137, 208);
            border.SetSize(30, 30);
            border.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            border.SetAlignment(ItemAlignment.VCenter | ItemAlignment.Left);

            //contact name
            contact.SetText(name);
            contact.SetFont(new Font(DefaultsService.GetDefaultFont().FontFamily, 14, FontStyle.Bold));
            contact.SetForeground(Color.White);
            contact.SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);
            contact.SetMargin(40, 0, 0, 0);
            contact.SetAlignment(ItemAlignment.Top | ItemAlignment.Left);

            //signal
            signal.SetBackground(67, 181, 129);
            signal.SetSize(14, 14);
            signal.SetBorderFill(Color.FromArgb(255, 66, 70, 77));
            signal.SetBorderThickness(2);
            signal.SetBorderRadius(7);
            signal.SetAlignment(ItemAlignment.Left | ItemAlignment.Bottom);
            signal.SetMargin(18, 0, 0, 3);

            //buttons bar
            HorizontalStack bar = new HorizontalStack();
            bar.SetMargin(120, 0, 0, 0);
            bar.SetSpacing(5, 0);

            //adding
            AddItems(border, signal, contact, bar);
            bar.AddItems(
                InfinityItemsBox.GetUserBarButton(),
                InfinityItemsBox.GetUserBarButton(),
                InfinityItemsBox.GetUserBarButton()
            );
        }
    }
}
