package com.customchance.View;

import com.spvessel.Decorations.*;
import com.spvessel.Flags.*;

import java.awt.Color;
import java.awt.Font;

import com.spvessel.Common.*;

class Styles {
    private Styles() {
    }

    static Style getButtonStyle() {
        Style style = new Style();
        style.background = new Color(255, 181, 111);
        style.foreground = Color.black;
        style.borderRadius = new CornerRadius();
        style.font = DefaultsService.getDefaultFont(Font.BOLD, 14);
        style.width = 150;
        style.height = 30;
        style.widthPolicy = SizePolicy.FIXED;
        style.heightPolicy = SizePolicy.FIXED;
        style.setAlignment(ItemAlignment.HCENTER, ItemAlignment.TOP);
        style.setTextAlignment(ItemAlignment.HCENTER, ItemAlignment.VCENTER);
        style.addItemState(ItemStateType.HOVERED, new ItemState(new Color(255, 255, 255, 60)));
        return style;
    }

    static Style getLabelStyle() {
        Style style = Style.getLabelStyle();
        style.foreground = new Color(210, 210, 210);
        style.font = DefaultsService.getDefaultFont(Font.BOLD, 14);
        style.height = 30;
        style.heightPolicy = SizePolicy.FIXED;
        style.setAlignment(ItemAlignment.VCENTER, ItemAlignment.LEFT);
        style.setTextAlignment(ItemAlignment.LEFT, ItemAlignment.VCENTER);
        return style;
    }

    static Style getCommonContainerStyle() {
        Style style = new Style();

        style.background = new Color(54, 57, 63);
        style.widthPolicy = SizePolicy.EXPAND;
        style.heightPolicy = SizePolicy.EXPAND;
        style.setAlignment(ItemAlignment.LEFT, ItemAlignment.TOP);

        return style;
    }
}