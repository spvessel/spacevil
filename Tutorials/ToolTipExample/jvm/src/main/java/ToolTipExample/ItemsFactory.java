package ToolTipExample;

import java.awt.Color;
import java.awt.image.BufferedImage;

import com.spvessel.spacevil.Ellipse;
import com.spvessel.spacevil.HorizontalStack;
import com.spvessel.spacevil.Label;
import com.spvessel.spacevil.Prototype;
import com.spvessel.spacevil.Rectangle;
import com.spvessel.spacevil.ToolTip;
import com.spvessel.spacevil.VerticalStack;
import com.spvessel.spacevil.Core.InterfaceBaseItem;
import com.spvessel.spacevil.Decorations.Border;
import com.spvessel.spacevil.Decorations.CornerRadius;
import com.spvessel.spacevil.Flags.ItemAlignment;
import com.spvessel.spacevil.Flags.SizePolicy;

public final class ItemsFactory {

    private ItemsFactory() {
    }

    public static HorizontalStack getToolBarLayout() {
        HorizontalStack layout = new HorizontalStack();
        layout.setHeightPolicy(SizePolicy.FIXED);
        layout.setHeight(40);
        layout.setBackground(35, 35, 35);
        layout.setSpacing(3, 0);
        layout.setPadding(10, 0, 0, 0);
        return layout;
    }

    public static VerticalStack getSideBarLayout() {
        VerticalStack layout = new VerticalStack();
        layout.setWidthPolicy(SizePolicy.FIXED);
        layout.setWidth(40);
        layout.setBackground(35, 35, 35);
        layout.setMargin(0, 55, 0, 55);
        layout.setContentAlignment(ItemAlignment.VCENTER);
        return layout;
    }

    public static Label getAreaForPermanentToolTip() {
        Label area = new Label();
        area.setTextAlignment(ItemAlignment.VCENTER, ItemAlignment.HCENTER);
        area.setMargin(55, 55, 55, 55);
        area.setBackground(45, 45, 45);
        area.setFontSize(25);
        area.setText("ToolTip area.\nWhen the mouse cursor inside this item\nthe tooltip is always displayed.");
        area.setBorder(new Border(new Color(50, 50, 50), new CornerRadius(), 1));
        area.setToolTip(
                "It is the tooltip area.\nWhen the mouse cursor inside this item\nthe tooltip is always displayed.");
        area.eventMouseHover.add((sender, args) -> {
            ToolTip.setTimeOut(area.getHandler(), 0);
        });
        area.eventMouseLeave.add((sender, args) -> {
            ToolTip.setTimeOut(area.getHandler(), 300);
        });
        return area;
    }

    public static Prototype getTool(BufferedImage icon, String tooltip) {
        Tool tool = new Tool(icon, tooltip);
        return tool;
    }

    public static Prototype getSideTool(BufferedImage icon, MyToolTip tooltip) {
        Tool tool = new Tool(icon);
        tool.setSizePolicy(SizePolicy.EXPAND, SizePolicy.FIXED);
        tool.setHeight(40);
        tool.setPadding(9, 9, 9, 9);
        tool.eventMouseHover.add((sender, args) -> {
            tooltip.show(sender, args);
        });
        tool.eventMouseLeave.add((sender, args) -> {
            tooltip.hide();
        });
        return tool;
    }

    public static InterfaceBaseItem getHorizontalDivider() {
        Rectangle divider = new Rectangle();
        divider.setHeight(1);
        divider.setWidthPolicy(SizePolicy.EXPAND);
        divider.setMargin(5, 10, 5, 10);
        divider.setBackground(100, 100, 100);
        return divider;
    }

    public static InterfaceBaseItem getVerticalDivider() {
        Rectangle divider = new Rectangle();
        divider.setWidth(2);
        divider.setHeightPolicy(SizePolicy.EXPAND);
        divider.setMargin(0, 0, 0, 0);
        divider.setBackground(100, 100, 100);
        return divider;
    }

    public static InterfaceBaseItem getDecor() {
        Ellipse ellipse = new Ellipse(12);
        ellipse.setSize(8, 8);
        ellipse.setAlignment(ItemAlignment.VCENTER, ItemAlignment.LEFT);
        ellipse.setBackground(169, 89, 213);
        ellipse.setMargin(5, 0, 0, 0);
        return ellipse;
    }
}
