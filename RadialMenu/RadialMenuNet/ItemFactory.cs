using System;

namespace RadialMenu
{
    public static class ItemFactory
    {
        public static Contact GetContact(String name, ContactMenu menu)
        {
            Contact contact = new Contact(name);
            menu.LinkContact(contact);
            return contact;
        }
    }
}