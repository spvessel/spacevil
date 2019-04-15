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
    class VisualContact : Prototype
    {
        public bool IsChecked = false;
        static int _count = 0;
        private string name = String.Empty;
        Ellipse border;
        Label contact;
        ButtonCore close;
        BlankItem signal;
        ItemState hovered = new ItemState(Color.FromArgb(255, 54, 57, 63));
        ContextMenu cm;
        public MenuItem Call;
        public MenuItem SendMessage;
        public MenuItem RemoveFriend;
TextEdit _input;
        public VisualContact(string name, TextEdit input)
        {
            _input = input;
            this.name = name;
            SetItemName("VC_" + _count);
            SetBackground(Color.Transparent);
            SetMinSize(40, 40);
            SetSize(40, 40);
            SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            SetBorderRadius(new CornerRadius(3));
            SetPadding(8, 0, 0, 0);
            AddItemState(ItemStateType.Hovered, hovered);
            _count++;

            EventMouseClick += MouseClick;

            Call = InfinityItemsBox.GetMenuItem("Call");
            SendMessage = InfinityItemsBox.GetMenuItem("Send message");
            RemoveFriend = InfinityItemsBox.GetMenuItem("Remove friend");
        }

        public override void InitElements()
        {
            border = new Ellipse();
            contact = new Label();
            close = new ButtonCore();
            signal = new BlankItem();

            InitContactMenu(GetHandler());

            //contact image border
            border.SetBackground(250, 166, 26);
            border.SetSize(30, 30);
            border.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            border.SetAlignment(ItemAlignment.VCenter | ItemAlignment.Left);

            //contact name
            contact.SetText(name);
            contact.SetFont(new Font(DefaultsService.GetDefaultFont().FontFamily, 14, FontStyle.Bold));
            contact.SetAlignment(ItemAlignment.VCenter | ItemAlignment.Left);
            contact.SetTextAlignment(ItemAlignment.VCenter | ItemAlignment.Left);
            contact.SetForeground(101, 102, 106);
            contact.SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);
            contact.SetMargin(40, 0, 0, 0);

            //contact close
            close.SetVisible(false);
            close.SetBackground(91, 94, 99);
            close.SetSize(10, 10);
            close.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            close.SetAlignment(ItemAlignment.VCenter | ItemAlignment.Right);
            close.SetMargin(0, 0, 12, 0);
            close.SetCustomFigure(new CustomFigure(false, GraphicsMathService.GetCross(10, 10, 1, 45)));
            close.AddItemState(ItemStateType.Hovered, new ItemState()
            {
                Background = Color.FromArgb(255, 255, 255, 255)
            });
            close.EventMouseClick += (sender, args) => DisposeSelf();

            signal.SetBackground(67, 181, 129);
            signal.SetSize(14, 14);
            signal.SetBorderFill(Color.FromArgb(255, 66, 70, 77));
            signal.SetBorderThickness(2);
            signal.SetBorderRadius(7);
            signal.SetAlignment(ItemAlignment.Left | ItemAlignment.Bottom);
            signal.SetMargin(18, 0, 0, 3);

            //adding
            AddItems(border, signal, contact, close);
            // IsFocusable = true;
        }

        public void DisposeSelf()
        {
            GetParent().RemoveItem(this);
        }

        public void MouseClick(IItem sender, MouseArgs args)
        {
            if (!IsChecked)
                UncheckOthers();
            cm.Show(sender, args);
        }

        public void UncheckOthers()
        {
            IsChecked = true;
            List<IBaseItem> list = GetParent().GetParent().GetItems();
            foreach (var item in list)
            {
                SelectionItem tmp = item as SelectionItem;
                if (item.Equals(GetParent()) || tmp == null)
                    continue;

                VisualContact _current = (tmp.GetContent() as VisualContact);
                _current.IsChecked = false;
                _current.Update();
            }
            Update();
        }

        public override void SetMouseHover(bool value)
        {
            if (value)
            {
                close.SetVisible(true);
                contact.SetForeground(Color.White);
            }
            else
            {
                close.SetVisible(false);
                if (!IsChecked)
                    contact.SetForeground(101, 102, 106);
            }
            base.SetMouseHover(value);
            Update();
        }
        public void Update()
        {
            if (IsChecked)
            {
                hovered.Background = Color.FromArgb(255, 66, 70, 77);
                SetBackground(66, 70, 77);
                contact.SetForeground(Color.White);
            }
            else
            {
                hovered.Background = Color.FromArgb(255, 54, 57, 63);
                SetBackground(Color.Transparent);
                contact.SetForeground(101, 102, 106);
            }
        }

        void InitContactMenu(CoreWindow handler)
        {
            cm = new ContextMenu(GetHandler());
            cm.SetBorderRadius(5);
            cm.SetBorderThickness(1);
            cm.SetBorderFill(32, 32, 32);
            cm.SetBackground(60, 60, 60);
            cm.ItemList.SetSelectionVisible(false);
            cm.ActiveButton = MouseButton.ButtonRight;
            cm.SetReturnFocus(_input);

            Call.SetForeground(Color.LightGray);
            Call.EventMouseClick += (sender, args) =>
            {
                PopUpMessage pop = new PopUpMessage("Calling " + contact.GetText() + "...");
                pop.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(42, 44, 49)));
                pop.SetBorderRadius(new CornerRadius(6, 6, 6, 6));
                pop.SetFont(DefaultsService.GetDefaultFont(18));
                pop.SetShadow(5, 3, 3, Color.FromArgb(200, 0, 0, 0));
                pop.Show(handler);
            };
            SendMessage.SetForeground(Color.LightGray);

            RemoveFriend.SetForeground(Color.LightGray);
            RemoveFriend.EventMouseClick += (sender, args) =>
            {
                DisposeSelf();
            };

            //add menuitems
            cm.AddItems(
                Call,
                SendMessage,
                RemoveFriend
            );
        }
    }
}
