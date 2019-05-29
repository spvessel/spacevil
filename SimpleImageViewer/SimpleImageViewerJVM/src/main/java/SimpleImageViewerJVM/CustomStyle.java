package SimpleImageViewerJVM;

import java.awt.Color;

import com.spvessel.spacevil.WrapGrid;
import com.spvessel.spacevil.Common.DefaultsService;
import com.spvessel.spacevil.Decorations.ItemState;
import com.spvessel.spacevil.Decorations.Style;
import com.spvessel.spacevil.Flags.*;

public class CustomStyle {
    public static Style getButtonStyle() {
        Style style = Style.getButtonCoreStyle();
        style.background = new Color(0, 0, 0, 0);
        style.setSize(30, 30);
        style.setPadding(6, 6, 6, 6);
        style.getState(ItemStateType.HOVERED).background = new Color(255, 255, 255, 20);
        return style;
    }

    public static void initWparGridStyle()
    {
        Style style = Style.getWrapGridStyle();
        Style wrap_style = style.getInnerStyle("area");
        Style wrapper_style = wrap_style.getInnerStyle("selection");
        wrapper_style.addItemState(ItemStateType.HOVERED, new ItemState(new Color(91, 225, 152, 215)));
        wrapper_style.addItemState(ItemStateType.TOGGLED, new ItemState(new Color(91, 225, 152)));
        DefaultsService.getDefaultTheme().replaceDefaultItemStyle(WrapGrid.class, style);
    }
}