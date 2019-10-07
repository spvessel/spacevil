package ToolTipExample;

import java.awt.Color;
import java.awt.image.BufferedImage;

import com.spvessel.spacevil.ImageItem;
import com.spvessel.spacevil.Prototype;
import com.spvessel.spacevil.Decorations.ItemState;
import com.spvessel.spacevil.Flags.ItemStateType;
import com.spvessel.spacevil.Flags.SizePolicy;

public class Tool extends Prototype {
    ImageItem image = null;

    public Tool(BufferedImage icon) {
        image = new ImageItem(icon);
        setSizePolicy(SizePolicy.FIXED, SizePolicy.EXPAND);
        setWidth(30);
        setBackground(0, 0, 0, 0);
        setPadding(4, 4, 4, 4);
        addItemState(ItemStateType.HOVERED, new ItemState(new Color(255, 255, 255, 20)));
        addItemState(ItemStateType.PRESSED, new ItemState(new Color(40, 40, 40)));
    }

    public Tool(BufferedImage icon, String tooltip) {
        this(icon);
        setToolTip(tooltip);
    }

    @Override
    public void initElements() {
        image.keepAspectRatio(true);
        addItem(image);
    }
}
