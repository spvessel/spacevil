package RadialMenu;

import com.spvessel.spacevil.ContextMenu;
import com.spvessel.spacevil.CoreWindow;
import com.spvessel.spacevil.MenuItem;
import com.spvessel.spacevil.PopUpMessage;

public class ContactMenu {
    ContextMenu _menu;
    MenuItem _call;
    MenuItem _remove;
    Contact _sender;
    RadialMenuItem _radialMenu = null;

    ContactMenu(CoreWindow window, RadialMenuItem radialMenu) {
        _radialMenu = radialMenu;
        _menu = new ContextMenu(window);
        _call = new MenuItem("Call");
        _remove = new MenuItem("Remove");

        _menu.setStyle(StyleFactory.getMenuStyle());
        _menu.addItems(_call, _remove);

        StyleFactory.getMenuItemStyle().setStyle(_call, _remove);

        initEvents();
    }

    public void linkContact(Contact contact) {
        contact.eventMouseClick.add((sender, args) -> {
            _menu.show(sender, args);
            _sender = contact;
        });
    }

    public void initEvents() {
        _remove.eventMouseClick.add((sender, args) -> {
            _sender.getParent().removeItem(_sender);
        });

        _call.eventMouseClick.add((sender, args) -> {
            _radialMenu.hide();
            PopUpMessage pop = new PopUpMessage("Calling to " + _sender.getName());
            pop.show(_menu.getHandler());
        });
    }
}