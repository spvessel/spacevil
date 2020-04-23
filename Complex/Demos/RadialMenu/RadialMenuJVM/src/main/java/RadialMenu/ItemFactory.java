package RadialMenu;

public class ItemFactory {
    private ItemFactory() {
    }

    public static Contact getContact(String name, ContactMenu menu) {
        Contact contact = new Contact(name);
        menu.linkContact(contact);
        return contact;
    }
}