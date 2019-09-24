package ComboBoxExample;

import java.awt.Color;
import java.awt.Font;
import java.awt.image.BufferedImage;

import com.spvessel.spacevil.ButtonCore;
import com.spvessel.spacevil.Ellipse;
import com.spvessel.spacevil.ImageItem;
import com.spvessel.spacevil.MenuItem;
import com.spvessel.spacevil.PopUpMessage;
import com.spvessel.spacevil.Decorations.Indents;
import com.spvessel.spacevil.Decorations.ItemState;
import com.spvessel.spacevil.Flags.InputEventType;
import com.spvessel.spacevil.Flags.ItemAlignment;
import com.spvessel.spacevil.Flags.ItemStateType;
import com.spvessel.spacevil.Flags.SizePolicy;

final class ItemsFactory {
    private ItemsFactory() {
    }

    static MenuItem getMenuItem(String name, BufferedImage bitmap) {
        MenuItem menuItem = new MenuItem(name);
        menuItem.setStyle(StyleFactory.getMenuItemStyle());
        menuItem.setTextMargin(new Indents(25, 0, 0, 0));

        // Optionally: set an event on click
        menuItem.eventMouseClick.add((sender, args) -> {
            PopUpMessage popUpInfo = new PopUpMessage("You choosed a function:\n" + menuItem.getText());
            popUpInfo.setStyle(StyleFactory.getBluePopUpStyle());
            popUpInfo.setTimeOut(2000);
            popUpInfo.show(menuItem.getHandler());
        });

        // Optionally: add an image into MenuItem
        ImageItem img = new ImageItem(bitmap, false);
        img.setSizePolicy(SizePolicy.FIXED, SizePolicy.FIXED);
        img.setSize(20, 20);
        img.setAlignment(ItemAlignment.LEFT, ItemAlignment.VCENTER);
        img.keepAspectRatio(true);
        menuItem.addItem(img);

        // Optionally: add a button into MenuItem
        ButtonCore infoBtn = new ButtonCore("?");
        infoBtn.setBackground(40, 40, 40);
        infoBtn.setWidth(20);
        infoBtn.setSizePolicy(SizePolicy.FIXED, SizePolicy.EXPAND);
        infoBtn.setFontStyle(Font.BOLD);
        infoBtn.setForeground(210, 210, 210);
        infoBtn.setAlignment(ItemAlignment.VCENTER, ItemAlignment.RIGHT);
        infoBtn.setMargin(0, 0, 10, 0);
        infoBtn.setBorderRadius(3);
        infoBtn.addItemState(ItemStateType.HOVERED, new ItemState(new Color(10, 140, 210)));
        infoBtn.setPassEvents(false, InputEventType.MOUSE_PRESS, InputEventType.MOUSE_RELEASE,
                InputEventType.MOUSE_DOUBLE_CLICK);
        infoBtn.isFocusable = false; // prevent focus this button
        infoBtn.eventMouseClick.add((sender, args) -> {
            PopUpMessage popUpInfo = new PopUpMessage("This is decorated MenuItem:\n" + menuItem.getText());
            popUpInfo.setStyle(StyleFactory.getDarkPopUpStyle());
            popUpInfo.setTimeOut(2000);
            popUpInfo.show(infoBtn.getHandler());
        });
        menuItem.addItem(infoBtn);

        return menuItem;
    }

    static Ellipse getDot() {
        Ellipse ellipse = new Ellipse(12);
        ellipse.setSize(8, 8);
        ellipse.setAlignment(ItemAlignment.VCENTER, ItemAlignment.LEFT);
        ellipse.setMargin(10, 0, 0, 0);
        return ellipse;
    }
}