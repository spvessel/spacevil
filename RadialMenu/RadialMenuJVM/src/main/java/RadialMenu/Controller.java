package RadialMenu;

import com.spvessel.spacevil.InputDialog;
import com.spvessel.spacevil.Common.DefaultsService;
import com.spvessel.spacevil.Flags.KeyCode;

public class Controller {
    private MainWindow _mainWindow;
    private Model _model;

    public Controller(Model model) {
        if (model == null)
            throw new NullPointerException("Model can not be NULL");
        addStylesToDefaultTheme();
        _model = model;
        _mainWindow = new MainWindow();
    }

    private void addStylesToDefaultTheme() {
        DefaultsService.getDefaultTheme().addDefaultCustomItemStyle(Contact.class, StyleFactory.getContactStyle());

        DefaultsService.getDefaultTheme().addDefaultCustomItemStyle(RadialMenuItem.class,
                StyleFactory.getRadialMenuDefaultStyle());
    }

    public void start() {
        initController();
        _mainWindow.show();
    }

    private void initController() {
        fillRadialMenu(_mainWindow.radialMenu);

        _mainWindow.eventKeyPress.add((sender, args) -> {
            if (args.key == KeyCode.MENU) {
                showContacts();
            }
        });

        _mainWindow.showContactsBtn.eventMouseClick.add((sender, args) -> {
            showContacts();
        });

        _mainWindow.radialMenu.addButton.eventMouseClick.add((sender, args) -> {
            _mainWindow.radialMenu.hide();
            InputDialog input = new InputDialog("Add a contact...", "Add");
            input.onCloseDialog.add(() -> {
                Contact contact = ItemFactory.getContact(input.getResult(), _mainWindow.menu);
                contact.setNotificationCount(_model.getRandomNumber(1, 19));
                _mainWindow.radialMenu.addItem(contact);
                _mainWindow.radialMenu.show();
            });
            input.show(_mainWindow);
        });
    }

    private void fillRadialMenu(RadialMenuItem radialMenu) {
        for (int i = 0; i < 8; i++)
            radialMenu.addItem(ItemFactory.getContact(_model.getRandomName(), _mainWindow.menu));

        for (Contact contact : radialMenu.getContacts()) {
            contact.setNotificationCount(_model.getRandomNumber(1, 19));
        }
    }

    private void showContacts() {
        _mainWindow.radialMenu.show();
        _mainWindow.radialMenu.setFocus();
    }
}