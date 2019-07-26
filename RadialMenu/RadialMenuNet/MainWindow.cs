using SpaceVIL;
using SpaceVIL.Core;

namespace RadialMenu
{
    public class MainWindow : ActiveWindow
    {
        public ContactMenu Menu;
        public ButtonCore ShowContactsBtn;
        public RadialMenuItem RadialMenu;

        public override void InitWindow()
        {
            SetParameters("Mainwindow", "RadialMenu Example C#", 800, 600, false);
            SetMinSize(300, 300);
            SetBackground(StyleFactory.CommonBackground);
            SetPadding(0, 0, 0, 0);
            SetAntiAliasingQuality(MSAA.MSAA8x);
            IsCentered = true;

            // title
            TitleBar title = new TitleBar(GetWindowTitle());

            // radial menu 
            RadialMenu = new RadialMenuItem(this);

            // contact menu
            Menu = new ContactMenu(this, RadialMenu);

            ShowContactsBtn = new ButtonCore("Show");
            ShowContactsBtn.SetStyle(StyleFactory.GetRoundedButtonStyle());

            // adding
            AddItems(title, ShowContactsBtn);
        }
    }
}