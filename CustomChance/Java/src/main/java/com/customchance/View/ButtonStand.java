package com.customchance.View;

import java.awt.Color;

import com.spvessel.spacevil.ButtonCore;
import com.spvessel.spacevil.Rectangle;
import com.spvessel.spacevil.Flags.SizePolicy;
import com.spvessel.spacevil.Flags.ItemAlignment;

class ButtonStand extends ButtonCore {
    Rectangle _stand;

    public ButtonStand() {
        super();
    }

    public ButtonStand(String text) {
        super(text);
    }

    @Override
    public void initElements() {
        super.initElements();
        _stand = new Rectangle();
        _stand.setBackground(Color.white);
        _stand.setHeight(3);
        _stand.setSizePolicy(SizePolicy.EXPAND, SizePolicy.FIXED);
        _stand.setAlignment(ItemAlignment.BOTTOM, ItemAlignment.HCENTER);
        _stand.setVisible(false);
        addItem(_stand);
    }

    @Override
    public void setMouseHover(boolean value) {
        super.setMouseHover(value);
        if (value)
            _stand.setVisible(true);
        else
            _stand.setVisible(false);
    }
}