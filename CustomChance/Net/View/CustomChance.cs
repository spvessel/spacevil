using System;
using System.Threading;
using System.Drawing;
using System.Reflection;

using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Common;
using SpaceVIL.Decorations;

namespace CustomChance
{
    public class MainWindow : ActiveWindow
    {
        public ButtonCore AddButton;
        public ButtonCore StartButton;
        private ListBox _listBox;

        public override void InitWindow()
        {
            //Window attr
            SetParameters(nameof(MainWindow), nameof(CustomChance), 360, 500, false);
            SetMinSize(350, 500);
            SetBackground(45, 45, 45);
            EventKeyRelease += (sender, args) => OnKeyRelease(sender, args);
            EventClose = () =>
            {
                CommonLogic.GetInstance().TrySerialize();
                Close();
            };

            //icons
            var big = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("CustomChance.icon_big.png"));
            var small = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream("CustomChance.icon_small.png"));
            SetIcon(big, small);

            //title
            TitleBar title = new TitleBar("Custom Chance");
            title.SetIcon(small, 16, 16);
            title.SetFont(DefaultsService.GetDefaultFont(14));
            title.GetMaximizeButton().SetVisible(false);

            //layout
            VerticalStack layout = new VerticalStack();
            layout.SetMargin(0, title.GetHeight(), 0, 0);
            layout.SetPadding(3, 3, 3, 3);
            layout.SetSpacing(vertical: 5);
            layout.SetBackground(255, 255, 255, 20);

            //listBox
            _listBox = new ListBox();
            _listBox.SetBackground(52, 52, 52);
            _listBox.SetHScrollBarVisible(ScrollBarVisibility.Never);
            _listBox.SetVScrollBarVisible(ScrollBarVisibility.Never);
            _listBox.SetSelectionVisible(false);

            //AddButton
            AddButton = new ButtonStand("Add a Member!");
            AddButton.SetStyle(Styles.GetButtonStyle());
            AddButton.SetMargin(0, 5, 0, 5);
            AddButton.SetShadow(5, 0, 4, Color.FromArgb(150, 0, 0, 0));
            AddButton.EventMouseClick += (sender, args) =>
            {
                InputDialog dialog = new InputDialog();

                dialog.OnCloseDialog += () =>
                {
                    String result = dialog.InputResult;
                    //add member
                    if (CommonLogic.GetInstance().AddMember(CommonLogic.GetInstance().Storage.Members, result))
                    {
                        //add member to ui
                        MemberItem member = new MemberItem();
                        member.MemberName.SetText(result);
                        member.Index = CommonLogic.GetInstance().Storage.Members.Count - 1;
                        _listBox.AddItem(member);
                    }
                };
                dialog.Show(this);
            };

            //StartButton
            StartButton = new ButtonStand("Make a Chance!");
            StartButton.SetStyle(Styles.GetButtonStyle());
            StartButton.SetMargin(0, 5, 0, 5);
            StartButton.SetShadow(5, 0, 4, Color.FromArgb(150, 0, 0, 0));
            StartButton.EventMouseClick += (sender, args) =>
            {
                if (CommonLogic.GetInstance().Storage.Members == null || CommonLogic.GetInstance().Storage.Members.Count == 0)
                    return;
                CommonLogic.GetInstance().StartRandom(CommonLogic.GetInstance().Storage.Members);
                UpdateUI();
            };

            //adding
            AddItems(
                title,
                layout
            );
            layout.AddItems(
                AddButton,
                _listBox,
                StartButton
            );

            //load save file
            if (CommonLogic.GetInstance().Storage.Members.Count > 0)
                RestoreItems();
        }

        private void RestoreItems()
        {
            foreach (var item in CommonLogic.GetInstance().Storage.Members)
            {
                //add member to ui
                MemberItem member = new MemberItem();
                _listBox.AddItem(member);
                member.MemberName.SetText(item.Name);
                member.MemberValue.SetText(item.Value + "%");
                member.IsWinner = item.IsWinner;
                member.Index = CommonLogic.GetInstance().Storage.Members.Count - 1;
            }
        }

        public void UpdateUI()
        {
            int index = 0;
            foreach (MemberItem member in _listBox.GetListContent())
            {
                member.MemberName.SetText(CommonLogic.GetInstance().Storage.Members[index].Name);
                member.MemberValue.SetText(CommonLogic.GetInstance().Storage.Members[index].Value + "%");
                member.IsWinner = CommonLogic.GetInstance().Storage.Members[index].IsWinner;
                index++;
            }
        }

        private void OnKeyRelease(IItem sender, KeyArgs args)
        {
            if (args.Key == KeyCode.Space)
                AddButton.EventMouseClick?.Invoke(AddButton, new MouseArgs());
        }
    }
}
