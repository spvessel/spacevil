package ComboBoxExample;

import java.awt.Color;

import com.spvessel.spacevil.Decorations.Border;
import com.spvessel.spacevil.Decorations.CornerRadius;
import com.spvessel.spacevil.Decorations.ItemState;
import com.spvessel.spacevil.Decorations.Shadow;
import com.spvessel.spacevil.Decorations.Style;
import com.spvessel.spacevil.Flags.ItemAlignment;
import com.spvessel.spacevil.Flags.ItemStateType;

final class StyleFactory {
    private StyleFactory() {
    }

    static Style getMenuItemStyle() {
        // get current style of an item and change it
        Style style = Style.getMenuItemStyle();
        style.setBackground(255, 255, 255, 7);
        style.foreground = new Color(210, 210, 210);
        style.borderRadius = new CornerRadius(3);
        style.addItemState(ItemStateType.HOVERED, new ItemState(new Color(255, 255, 255, 45)));
        return style;
    }

    static Style getComboBoxDropDownStyle() {
        // get current style of an item and change it
        Style style = Style.getComboBoxDropDownStyle();
        style.setBackground(50, 50, 50);
        style.setBorder(new Border(new Color(100, 100, 100), new CornerRadius(0, 0, 5, 5), 1));
        style.setShadow(new Shadow(10, 3, 3, new Color(0, 0, 0, 150)));
        style.isShadowDrop = true;
        return style;
    }

    static Style getComboBoxStyle() {
        // get current style of an item and change it
        Style style = Style.getComboBoxStyle();
        style.setBackground(45, 45, 45);
        style.setForeground(210, 210, 210);
        style.setBorder(new Border(new Color(255, 181, 111), new CornerRadius(10, 0, 0, 10), 2));
        style.setShadow(new Shadow(10, 3, 3, new Color(0, 0, 0, 150)));
        style.isShadowDrop = true;

        // Note: every complex item has a few inner styles for its children
        // for example ComboBox has drop down area, selection item, drob down button (with arrow)
        // Replace inner style
        style.removeInnerStyle("dropdownarea");
        Style dropDownAreaStyle = getComboBoxDropDownStyle(); // get our own style
        style.addInnerStyle("dropdownarea", dropDownAreaStyle);

        // Change inner style
        Style selectionStyle = style.getInnerStyle("selection");
        if (selectionStyle != null) {
            selectionStyle.borderRadius = new CornerRadius(10, 0, 0, 10);
            selectionStyle.setBackground(0, 0, 0, 0);
            selectionStyle.setPadding(25, 0, 0, 0);
            selectionStyle.addItemState(ItemStateType.HOVERED, new ItemState(new Color(255, 255, 255, 20)));
        }

        // Change inner style
        Style dropDownButtonStyle = style.getInnerStyle("dropdownbutton");
        if (dropDownButtonStyle != null)
            dropDownButtonStyle.borderRadius = new CornerRadius(0, 0, 0, 10);

        return style;
    }

    static Style getBluePopUpStyle() {
        // get current style of an item and change it
        Style style = Style.getPopUpMessageStyle();
        style.setBackground(10, 162, 232);
        style.setForeground(0, 0, 0);
        style.height = 60;
        style.borderRadius = new CornerRadius(12);
        style.setAlignment(ItemAlignment.BOTTOM, ItemAlignment.HCENTER);
        style.setMargin(0, 0, 0, 50);

        // Change inner style
        Style closeButtonStyle = style.getInnerStyle("closebutton");
        if (closeButtonStyle != null) {
            closeButtonStyle.setBackground(10, 10, 10, 255);
            closeButtonStyle.addItemState(ItemStateType.HOVERED, new ItemState(Color.white));
        }

        return style;
    }

    static Style getDarkPopUpStyle() {
        // get current style of an item and change it
        Style style = Style.getPopUpMessageStyle();
        style.height = 60;
        style.setAlignment(ItemAlignment.BOTTOM, ItemAlignment.HCENTER);
        style.setMargin(0, 0, 0, 50);
        return style;
    }
}