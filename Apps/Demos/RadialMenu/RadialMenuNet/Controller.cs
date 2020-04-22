using System;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;

namespace RadialMenu
{
    public class Controller
    {
        private MainWindow _mainWindow;
        private Model _model;

        public Controller(Model model)
        {
            if (model == null)
                throw new ArgumentNullException("Model can not be NULL");
            AddStylesToDefaultTheme();
            _model = model;
            _mainWindow = new MainWindow();
        }

        private void AddStylesToDefaultTheme()
        {
            DefaultsService.GetDefaultTheme().AddDefaultCustomItemStyle(
                typeof(RadialMenu.RadialMenuItem),
                StyleFactory.GetRadialMenuDefaultStyle());

            DefaultsService.GetDefaultTheme().AddDefaultCustomItemStyle(
                typeof(RadialMenu.Contact),
                StyleFactory.GetContactStyle());
        }

        public void Start()
        {
            InitController();
            _mainWindow.Show();
        }

        private void InitController()
        {
            FillRadialMenu(_mainWindow.RadialMenu);

            _mainWindow.EventKeyPress += (sender, args) =>
            {
                if (args.Key == KeyCode.Menu)
                {
                    ShowContacts();
                }
            };

            _mainWindow.ShowContactsBtn.EventMouseClick += (sender, args) =>
            {
                ShowContacts();
            };

            _mainWindow.RadialMenu.AddButton.EventMouseClick += (sender, args) =>
            {
                _mainWindow.RadialMenu.Hide();
                InputDialog input = new InputDialog("Add a contact...", "Add");
                input.OnCloseDialog += () =>
                {
                    Contact contact = ItemFactory.GetContact(input.GetResult(), _mainWindow.Menu);
                    contact.SetNotificationCount(_model.GetRandomNumber(1, 19));
                    _mainWindow.RadialMenu.AddItem(contact);
                    _mainWindow.RadialMenu.Show();
                };
                input.Show(_mainWindow);
            };
        }

        private void FillRadialMenu(RadialMenuItem RadialMenu)
        {
            for (int i = 0; i < 8; i++)
                RadialMenu.AddItem(ItemFactory.GetContact(_model.getRandomName(), _mainWindow.Menu));

            foreach (Contact contact in RadialMenu.GetContacts())
            {
                contact.SetNotificationCount(_model.GetRandomNumber(1, 19));
            }
        }

        private void ShowContacts()
        {
            _mainWindow.RadialMenu.Show();
            _mainWindow.RadialMenu.SetFocus();
        }
    }
}