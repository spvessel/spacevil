package StylingExample;

import java.awt.Color;

import com.spvessel.spacevil.WrapGrid;
import com.spvessel.spacevil.Common.DefaultsService;
import com.spvessel.spacevil.Decorations.CornerRadius;
import com.spvessel.spacevil.Decorations.ItemState;
import com.spvessel.spacevil.Decorations.Shadow;
import com.spvessel.spacevil.Decorations.Style;
import com.spvessel.spacevil.Flags.ItemAlignment;
import com.spvessel.spacevil.Flags.ItemStateType;
import com.spvessel.spacevil.Flags.SizePolicy;

public final class StyleFactory {

    private StyleFactory() {
    }

    public static Style getPictureStyle() {
        // style for Picture
        Style style = new Style();
        style.setSizePolicy(SizePolicy.EXPAND, SizePolicy.EXPAND);
        style.setSpacing(0, 5);
        style.setMargin(3, 3, 3, 3);
        style.setPadding(5, 5, 5, 5);
        style.setAlignment(ItemAlignment.LEFT, ItemAlignment.TOP);
        style.setBackground(80, 80, 80);
        style.setShadow(new Shadow(5, 0, 0, new Color(0, 0, 0, 200)));
        style.isShadowDrop = true;
        style.setBorder(Color.gray, new CornerRadius(10), 1);

        // inner styles: Picture consist of ImageItem and Label
        // style for ImageItem
        Style imageStyle = new Style();
        imageStyle.setSizePolicy(SizePolicy.EXPAND, SizePolicy.EXPAND);
        imageStyle.setAlignment(ItemAlignment.HCENTER, ItemAlignment.VCENTER);
        imageStyle.setBackground(45, 45, 45);
        imageStyle.borderRadius = new CornerRadius(6, 6, 0, 0);
        style.addInnerStyle("image", imageStyle);
        // style for Label
        Style textStyle = new Style();
        textStyle.setSizePolicy(SizePolicy.EXPAND, SizePolicy.FIXED);
        textStyle.height = 30;
        textStyle.setTextAlignment(ItemAlignment.HCENTER, ItemAlignment.VCENTER);
        textStyle.setBackground(150, 150, 150);
        textStyle.setForeground(32, 32, 32);
        textStyle.borderRadius = new CornerRadius(0, 0, 6, 6);
        style.addInnerStyle("text", textStyle);
        
        return style;
    }

    public static void updateWrapGridStyle() {
        // get style from basic theme
        Style selectionStyle = DefaultsService.getDefaultTheme().getThemeStyle(WrapGrid.class).getInnerStyle("area")
                .getInnerStyle("selection");
        // change style for selection
        if (selectionStyle != null) {
            selectionStyle.addItemState(ItemStateType.HOVERED, new ItemState(new Color(10, 162, 232)));
            selectionStyle.addItemState(ItemStateType.TOGGLED, new ItemState(new Color(49, 213, 121)));
            selectionStyle.borderRadius = new CornerRadius(12);
            selectionStyle.setPadding(0, 0, 0, 0);
        }
    }
}
