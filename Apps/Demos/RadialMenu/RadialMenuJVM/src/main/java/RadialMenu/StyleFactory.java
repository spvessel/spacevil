package RadialMenu;

import java.awt.Color;
import java.awt.Font;

import com.spvessel.spacevil.GraphicsMathService;
import com.spvessel.spacevil.Common.DefaultsService;
import com.spvessel.spacevil.Decorations.Border;
import com.spvessel.spacevil.Decorations.CornerRadius;
import com.spvessel.spacevil.Decorations.ItemState;
import com.spvessel.spacevil.Decorations.Shadow;
import com.spvessel.spacevil.Decorations.Style;
import com.spvessel.spacevil.Flags.ItemAlignment;
import com.spvessel.spacevil.Flags.ItemStateType;
import com.spvessel.spacevil.Flags.SizePolicy;

public class StyleFactory {
    public static final Color commonBackground = new Color(60, 60, 60);
    public static final Color contactBackground = new Color(55, 55, 55);
    public static final Color transparent = new Color(0, 0, 0, 0);

    private StyleFactory() {
    }

    public static Style getMenuItemStyle() {
        Style style = Style.getMenuItemStyle();
        style.borderRadius = new CornerRadius(3);
        style.setForeground(32, 32, 32);
        style.addItemState(ItemStateType.HOVERED, new ItemState(new Color(0, 0, 0, 30)));
        return style;
    }
    
    public static Style getMenuStyle() {
        Style style = Style.getContextMenuStyle();
        style.setBackground(210, 210, 210);
        style.borderRadius = new CornerRadius(5);
        style.setShadow(new Shadow(5, 2, 2, new Color(0, 0, 0, 180)));
        return style;
    }

    public static Style getRoundedButtonStyle() {
        Style style = Style.getButtonCoreStyle();
        style.setAlignment(ItemAlignment.VCENTER, ItemAlignment.HCENTER);
        style.setSize(50, 50);
        style.setShadow(new Shadow(8, 0, 0, Color.BLACK));
        style.isShadowDrop = true;
        style.setBorder(new Border(new Color(0, 0, 0, 0), new CornerRadius(25), 0));
        return style;
    }

    public static Style getRadialMenuDefaultStyle() {
        Style style = Style.getDefaultCommonStyle();

        style.setSizePolicy(SizePolicy.EXPAND, SizePolicy.EXPAND);
        style.setBackground(0, 0, 0, 120);
        style.isVisible = false;

        Style hideStyle = Style.getButtonCoreStyle();
        hideStyle.setPadding(15, 20, 15, 5);
        hideStyle.setBackground(150, 150, 150);
        hideStyle.setSize(60, 60);
        hideStyle.setBorder(new Border(new Color(0, 0, 0, 0), new CornerRadius(30), 0));
        hideStyle.setShadow(new Shadow(5, 3, 3, Color.BLACK));
        hideStyle.isShadowDrop = true;
        style.addInnerStyle("hidebutton", hideStyle);

        Style addStyle = Style.getButtonCoreStyle();
        addStyle.font = DefaultsService.getDefaultFont(Font.BOLD, 12);
        addStyle.setBackground(100, 200, 130);
        addStyle.setSize(50, 30);
        addStyle.setBorder(new Border(new Color(0, 0, 0, 0), new CornerRadius(15), 0));
        addStyle.setShadow(new Shadow(5, 3, 3, Color.BLACK));
        addStyle.isShadowDrop = true;
        ItemState hover = new ItemState(new Color(255, 255, 255, 30));
        hover.border = new Border(Color.WHITE, new CornerRadius(15), 2);
        addStyle.addItemState(ItemStateType.HOVERED, hover);
        style.addInnerStyle("addbutton", addStyle);

        return style;
    }

    public static Style getContactStyle() {
        Style style = Style.getDefaultCommonStyle();
        style.setSizePolicy(SizePolicy.FIXED, SizePolicy.FIXED);
        style.background = StyleFactory.transparent;

        Style layout = Style.getVerticalStackStyle();
        layout.setSpacing(0, 5);
        style.addInnerStyle("layout", layout);

        Style face = Style.getButtonCoreStyle();
        face.setSizePolicy(SizePolicy.FIXED, SizePolicy.FIXED);
        face.setShadow(new Shadow(5, 2, 2, Color.BLACK));
        face.isShadowDrop = true;
        face.setBackground(120, 120, 120);
        face.setBackground(5, 162, 232);
        face.setAlignment(ItemAlignment.HCENTER, ItemAlignment.VCENTER);
        face.setPadding(8, 8, 8, 8);
        face.shape = GraphicsMathService.getEllipse(30, 30);
        face.addItemState(ItemStateType.HOVERED, new ItemState(new Color(255, 255, 255, 60)));
        face.addItemState(ItemStateType.PRESSED, new ItemState(new Color(0, 0, 0, 60)));
        style.addInnerStyle("face", face);

        Style name = Style.getLabelStyle();
        name.font = DefaultsService.getDefaultFont(12);
        name.background = StyleFactory.contactBackground;
        name.setSizePolicy(SizePolicy.FIXED, SizePolicy.FIXED);
        name.setSize(26, 26);
        name.borderRadius = new CornerRadius(name.width / 2);
        name.setAlignment(ItemAlignment.HCENTER);
        name.setTextAlignment(ItemAlignment.HCENTER, ItemAlignment.VCENTER);
        name.setShadow(new Shadow(5, 0, 0, Color.BLACK));
        name.isShadowDrop = true;
        style.addInnerStyle("name", name);

        Style notification = Style.getLabelStyle();
        notification.setSizePolicy(SizePolicy.FIXED, SizePolicy.FIXED);
        notification.setTextAlignment(ItemAlignment.HCENTER, ItemAlignment.VCENTER);
        notification.setAlignment(ItemAlignment.RIGHT, ItemAlignment.TOP);
        notification.setSize(20, 20);
        notification.setBorder(new Border(Color.WHITE, new CornerRadius(10), 1));
        notification.background = StyleFactory.contactBackground;
        notification.font = DefaultsService.getDefaultFont(Font.BOLD, 12);
        style.addInnerStyle("notification", notification);

        return style;
    }
}