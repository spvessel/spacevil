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
    class InputDialog : DialogItem
    {
        public String InputResult = String.Empty;
        ButtonCore _add = new ButtonStand("Add");
        TextEdit _input = new TextEdit();

        public InputDialog() : base()
        {
            SetItemName("InputDialog_");
        }

        public override void InitElements()
        {
            base.InitElements();

            //window's attr
            Window.SetBackground(45, 45, 45);

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
                Close();
            };

            //adding items
            Window.AddItems(
                title,
                layout
            );
            layout.AddItems(
                _input,
                _add
            );

            title.GetCloseButton().EventMouseClick = null;
            title.GetCloseButton().EventMouseClick += (sender, args) =>
            {
                Close();
            };
        }

        public override void Show(WindowLayout handler)
        {
            InputResult = String.Empty;
            _input.SetText(String.Empty);
            base.Show(handler);
            _input.SetFocus();
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
                _add.EventMouseClick?.Invoke(_add, new MouseArgs());
        }
    }
}