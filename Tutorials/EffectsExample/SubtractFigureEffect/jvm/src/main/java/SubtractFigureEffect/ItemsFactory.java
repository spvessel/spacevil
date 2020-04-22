package SubtractFigureEffect;

import java.awt.Color;
import java.awt.Font;
import java.util.Arrays;
import java.util.List;

import com.spvessel.spacevil.ButtonToggle;
import com.spvessel.spacevil.Ellipse;
import com.spvessel.spacevil.GraphicsMathService;
import com.spvessel.spacevil.Label;
import com.spvessel.spacevil.Common.DefaultsService;
import com.spvessel.spacevil.Core.InterfaceBaseItem;
import com.spvessel.spacevil.Core.InterfaceEffect;
import com.spvessel.spacevil.Decorations.Figure;
import com.spvessel.spacevil.Decorations.SubtractFigure;
import com.spvessel.spacevil.Flags.ItemAlignment;

public final class ItemsFactory {

    private ItemsFactory() {
    }

    public static InterfaceBaseItem getLabel(String text) {
        Label label = new Label(text);
        label.setTextAlignment(ItemAlignment.HCENTER, ItemAlignment.TOP);
        label.setPadding(0, 20, 0, 0);
        label.setFont(DefaultsService.getDefaultFont(Font.BOLD, 19));
        return label;
    }

    public static ButtonToggle getSwitchButton() {
        ButtonToggle btn = new ButtonToggle("Enable Subtract Effect");
        btn.setSize(200, 40);
        btn.setAlignment(ItemAlignment.HCENTER, ItemAlignment.BOTTOM);
        btn.setMargin(0, 0, 0, 20);
        btn.setBorderRadius(btn.getHeight() / 2);
        btn.eventToggle.add((sender, args) -> {
            if (btn.isToggled()) {
                btn.setText("Disable Subtract Effect");
            } else {
                btn.setText("Enable Subtract Effect");
            }
        });
        return btn;
    }

    public static InterfaceBaseItem getCircle(int diameter, Color color) {
        Ellipse circle = new Ellipse(64);
        circle.setSize(diameter, diameter);
        circle.setBackground(color);
        circle.setAlignment(ItemAlignment.HCENTER, ItemAlignment.VCENTER);
        circle.setShadow(5, 0, 0, Color.BLACK);
        circle.setShadowExtension(2, 2);
        return circle;
    }

    public static void setCircleAlignment(InterfaceBaseItem circle, ItemAlignment... alignment) {
        List<ItemAlignment> list = Arrays.asList(alignment);

        int offset = circle.getWidth() / 3;

        if (list.contains(ItemAlignment.TOP)) {
            circle.setMargin(circle.getMargin().left, circle.getMargin().top - offset + 10, circle.getMargin().right,
                    circle.getMargin().bottom);
        }

        if (list.contains(ItemAlignment.BOTTOM)) {
            circle.setMargin(circle.getMargin().left, circle.getMargin().top, circle.getMargin().right,
                    circle.getMargin().bottom - offset);
        }

        if (list.contains(ItemAlignment.LEFT)) {
            circle.setMargin(circle.getMargin().left - offset, circle.getMargin().top, circle.getMargin().right,
                    circle.getMargin().bottom);
        }

        if (list.contains(ItemAlignment.RIGHT)) {
            circle.setMargin(circle.getMargin().left, circle.getMargin().top, circle.getMargin().right - offset,
                    circle.getMargin().bottom);
        }
    }

    public static InterfaceEffect getCircleEffect(InterfaceBaseItem circle, InterfaceBaseItem subtract) {
        int diameter = circle.getHeight();
        float scale = 1.1f;
        int diff = (int) (diameter * scale - diameter) / 2;
        int xOffset = subtract.getX() - circle.getX() - diff;
        int yOffset = subtract.getY() - circle.getY() - diff;

        SubtractFigure effect = new SubtractFigure(
                new Figure(false, GraphicsMathService.getEllipse(diameter, diameter, 0, 0, 64)));
        effect.setAlignment(ItemAlignment.VCENTER, ItemAlignment.HCENTER);
        effect.setSizeScale(scale, scale);
        effect.setPositionOffset(xOffset, yOffset);

        return effect;
    }

    public static SubtractFigure getCircleCenterEffect(InterfaceBaseItem circle) {
        float scale = 0.4f;
        int diameter = (int) (circle.getHeight() * scale);
        SubtractFigure effect = new SubtractFigure(
                new Figure(true, GraphicsMathService.getEllipse(diameter, diameter, 0, 0, 64)));
        effect.setAlignment(ItemAlignment.VCENTER, ItemAlignment.HCENTER);
        return effect;
    }
}