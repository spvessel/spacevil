package com.game2048;

import com.spvessel.spacevil.Label;
import com.spvessel.spacevil.Common.DefaultsService;
import com.spvessel.spacevil.Flags.ItemAlignment;

import java.awt.*;

public class Tile extends Label {
    private int value = 0;

    public Tile() {
        setValue(0);
        setTextAlignment(ItemAlignment.HCENTER, ItemAlignment.VCENTER);
        final int size = value < 100 ? 36 : value < 1000 ? 32 : 24;
        setBorderRadius(6);
        setFont(DefaultsService.getDefaultFont(Font.BOLD, size));
        setShadow(5, 2, 2, new Color(0, 0, 0, 100));
    }

    public void setValue(int value) {
        this.value = value;
        if (value == 0) {
            setText("");
        } else {
            setText(Integer.toString(value));
        }

        if (value < 16)
            setForeground(Color.decode("0x776e65"));
        else
            setForeground(Color.decode("0xf9f6f2"));

        setValueColor();
    }

    public int getValue() {
        return value;
    }

    private void setValueColor() {
        switch (value) {
        case 0: {
            setBackground(Color.decode("0xcdc1b4"));
            break;
        }
        case 2: {
            setBackground(Color.decode("0xeee4da"));
            break;
        }
        case 4: {
            setBackground(Color.decode("0xede0c8"));
            break;
        }
        case 8: {
            setBackground(Color.decode("0xf2b179"));
            break;
        }
        case 16: {
            setBackground(Color.decode("0xf59563"));
            break;
        }
        case 32: {
            setBackground(Color.decode("0xf67c5f"));
            break;
        }
        case 64: {
            setBackground(Color.decode("0xf65e3b"));
            break;
        }
        case 128: {
            setBackground(Color.decode("0xedcf72"));
            break;
        }
        case 256: {
            setBackground(Color.decode("0xedcc61"));
            break;
        }
        case 512: {
            setBackground(Color.decode("0xedc850"));
            break;
        }
        case 1024: {
            setBackground(Color.decode("0xedc53f"));
            break;
        }
        case 2048: {
            setBackground(Color.decode("0xedc22e"));
            break;
        }
        default: {
            setBackground(Color.decode("0xff0000"));
            break;
        }
        }
    }
}
