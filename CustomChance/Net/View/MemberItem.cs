using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceVIL;
using System.Drawing;
using SpaceVIL.Core;
using SpaceVIL.Decorations;
using SpaceVIL.Common;

namespace CustomChance
{
    class MemberItem : Prototype
    {
        public static int _count = 0;
        public int Index = 0;
        public Grid Layout;
        public ButtonCore MemberRemove;
        public Label MemberName;
        public Label MemberValue;
        private bool _is_winner = false;
        public bool IsWinner
        {
            get { return _is_winner; }
            set
            {
                _is_winner = value;
                UpdateStyle(value);
            }
        }

        private void UpdateStyle(bool winner)
        {
            if (winner)
            {
                SetBackground(45, 45, 45);
                MemberName.SetFontStyle(FontStyle.Bold);
                MemberValue.SetFontStyle(FontStyle.Bold);
            }
            else
            {
                SetBackground(Color.Transparent);
                MemberName.SetFontStyle(FontStyle.Regular);
                MemberValue.SetFontStyle(FontStyle.Regular);
            }
        }

        public MemberItem()
        {
            SetItemName("Member_" + _count);
            SetBackground(Color.Transparent);
            SetSize(0, 30);
            SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            SetPadding(5, 0, 5, 0);
            _count++;

            Layout = new Grid(1, 3);
            MemberName = new Label();
            MemberValue = new Label();
            MemberRemove = new ButtonCore();
        }

        public override void InitElements()
        {
            //Name
            MemberName.SetStyle(Styles.GetLabelStyle());
            MemberName.SetFontStyle(FontStyle.Regular);

            //Value
            MemberValue.SetStyle(Styles.GetLabelStyle());
            MemberValue.SetFontStyle(FontStyle.Regular);
            MemberValue.SetText("0%");
            MemberValue.SetWidth(45);
            MemberValue.SetWidthPolicy(SizePolicy.Fixed);
            MemberValue.SetTextAlignment(ItemAlignment.VCenter | ItemAlignment.Right);
            MemberValue.SetMargin(0, 0, 10, 0);

            //Button
            MemberRemove.SetBackground(255, 181, 111);
            MemberRemove.SetSize(14, 14);
            MemberRemove.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            MemberRemove.SetAlignment(ItemAlignment.VCenter | ItemAlignment.Left);
            MemberRemove.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(125, 255, 255, 255)));
            MemberRemove.SetCustomFigure(new Figure(false, GraphicsMathService.GetCross(14, 14, 5, 45)));
            MemberRemove.EventMouseClick += (sender, args) => DisposeSelf();

            //Adding
            AddItem(Layout);

            Layout.AddItems(
                MemberName,
                MemberValue,
                MemberRemove
            );
        }
        public void DisposeSelf()
        {
            CommonLogic.GetInstance().DeleteMember(
                CommonLogic.GetInstance().Storage.Members,
                MemberName.GetText());
            GetParent().RemoveItem(this);
        }
    }
}