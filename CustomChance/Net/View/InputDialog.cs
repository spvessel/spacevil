using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace CustomChance
{
    class InputDialog : DialogWindow
    {
        public String InputResult = String.Empty;
        ButtonCore _add = new ButtonStand("Add");
        TextEdit _input = new TextEdit();

        public override void InitWindow()
        {
            //window's attr
            WindowLayout Handler = new WindowLayout("_inputDialog", "_add member", 330, 150, true);
            SetHandler(Handler);
            Handler.SetWindowTitle("Add member");
            Handler.SetMinSize(330, 150);
            Handler.SetBackground(45, 45, 45);
            Handler.IsDialog = true;
            Handler.IsAlwaysOnTop = true;

            //title
            TitleBar title = new TitleBar("Adding a new member");
            title.SetFont(DefaultsService.GetDefaultFont(14));
            title.GetMinimizeButton().SetVisible(false);
            title.GetMaximizeButton().SetVisible(false);

            VerticalStack layout = new VerticalStack();
            layout.SetMargin(0, title.GetHeight(), 0, 0);
            layout.SetPadding(6, 15, 6, 6);
            layout.SetSpacing(vertical: 30);
            layout.SetBackground(255, 255, 255, 20);

            //message
            _input.EventKeyRelease += OnKeyRelease;

            //ok
            _add.SetBackground(255, 181, 111);
            _add.SetStyle(Styles.GetButtonStyle());
            _add.SetShadow(5, 0, 4, Color.FromArgb(150, 0, 0, 0));
            _add.EventMouseClick += (sender, args) =>
            {
                InputResult = _input.GetText();
                Handler.Close();
            };

            //adding items
            Handler.AddItems(
                title,
                layout
            );
            layout.AddItems(
                _input, 
                _add
            );

            //focus item
            _input.SetFocus();
        }

        private void OnKeyRelease(IItem sender, KeyArgs args)
        {
            if (args.Key == KeyCode.Enter)
                _add.EventMouseClick?.Invoke(_add, new MouseArgs());
        }
    }
}