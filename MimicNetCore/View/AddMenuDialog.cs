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
    class AddMenuDialog : DialogWindow
    {
        public String InputResult = String.Empty;
        private ButtonCore add;
        public override void InitWindow()
        {
            //window init
            WindowLayout Handler = new WindowLayout(nameof(AddMenuDialog), "Adding a new friend", 330, 150, true);
            SetHandler(Handler);
            Handler.SetMinSize(330, 150);
            Handler.SetBackground(47, 49, 54);
            Handler.IsDialog = true;
            Handler.IsAlwaysOnTop = true;

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
            TextEdit input = new TextEdit();
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
            add.EventMouseClick += (sender, args) =>
            {
                InputResult = input.GetText();
                Handler.Close();
            };

            //adding items
            Handler.AddItems(
                title,
                layout
            );
            layout.AddItems(
                input,
                add
            );

            //focus on textedit
            input.SetFocus();
        }

        private void OnKeyRelease(IItem sender, KeyArgs args)
        {
            if (args.Key == KeyCode.Enter)
                add.EventMouseClick?.Invoke(add, new MouseArgs());
        }
    }
}