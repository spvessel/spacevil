using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Common;

namespace MimicSpace
{
    class AddMenuDialog : DialogItem
    {
        public String InputResult = String.Empty;
        private ButtonCore add;
        TextEdit input = new TextEdit();

        public override void InitElements()
        {
            //important!
            base.InitElements();

            //window init
            Window.SetMinSize(330, 150);
            Window.SetBackground(47, 49, 54);
            Window.SetPadding(0, 0, 0, 0);

            //title
            TitleBar title = new TitleBar("Adding a new friend");
            title.SetStyle(Styles.GetTitleBarStyle());
            title.GetMaximizeButton().SetVisible(false);
            title.GetMinimizeButton().SetVisible(false);

            VerticalStack layout = new VerticalStack();
            layout.SetAlignment(ItemAlignment.Top | ItemAlignment.HCenter);
            layout.SetMargin(0, 22, 0, 0);
            layout.SetPadding(6, 15, 6, 6);
            layout.SetSpacing(vertical: 30);
            layout.SetBackground(255, 255, 255, 20);

            //new friend's name
            input.SetBorderRadius(4);
            input.EventKeyRelease += OnKeyRelease;

            //add button
            add = InfinityItemsBox.GetOrdinaryButton();
            add.SetSize(150, 30);
            add.SetBackground(67, 181, 129);
            add.SetForeground(Color.White);
            add.SetText("Add");
            add.SetAlignment(ItemAlignment.HCenter | ItemAlignment.Bottom);
            add.SetPadding(0, 2, 0, 0);
            add.SetShadow(4, 0, 2, Color.FromArgb(150, 0, 0, 0));

            //adding items
            Window.AddItems(
                title,
                layout
            );
            layout.AddItems(
                input,
                add
            );

            add.EventMouseClick += (sender, args) =>
            {
                InputResult = input.GetText();
                Close();
            };

            title.GetCloseButton().EventMouseClick = null;
            title.GetCloseButton().EventMouseClick += (sender, args) =>
            {
                Close();
            };
            //focus on textedit
            input.SetFocus();
        }

        public override void Show(WindowLayout handler)
        {
            InputResult = String.Empty;
            input.SetText(InputResult);
            base.Show(handler);
        }

        public override void Close()
        {
            if (OnCloseDialog != null)
                OnCloseDialog.Invoke();

            base.Close();
        }


        private void OnKeyRelease(IItem sender, KeyArgs args)
        {
            if (args.Key == KeyCode.Enter)
                add.EventMouseClick?.Invoke(add, new MouseArgs());
        }
    }
}