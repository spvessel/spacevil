using System;
using SpaceVIL;

namespace RadialMenu
{
    public class ContactMenu
    {
        ContextMenu _menu;
        MenuItem _call;
        MenuItem _remove;
        Contact _sender;
        RadialMenuItem _radialMenu = null;

        public ContactMenu(CoreWindow window, RadialMenuItem radialMenu)
        {
            _radialMenu = radialMenu;
            _menu = new ContextMenu(window);
            _call = new MenuItem("Call");
            _remove = new MenuItem("Remove");

            _menu.SetStyle(StyleFactory.GetMenuStyle());
            _menu.AddItems(_call, _remove);

            StyleFactory.GetMenuItemStyle().SetStyle(_call, _remove);

            InitEvents();
        }

        public void LinkContact(Contact contact)
        {
            contact.EventMouseClick += (sender, args) =>
            {
                _menu.Show(sender, args);
                _sender = contact;
            };
        }

        public void InitEvents()
        {
            _remove.EventMouseClick += (sender, args) =>
            {
                _sender.GetParent().RemoveItem(_sender);
            };

            _call.EventMouseClick += (sender, args) =>
            {
                _radialMenu.Hide();
                PopUpMessage pop = new PopUpMessage("Calling to " + _sender.GetName());
                pop.Show(_menu.GetHandler());
            };
        }
    }
}