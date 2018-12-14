package com.customchance.View;

import java.awt.Color;
import java.awt.Font;
import com.customchance.Model.CommonLogic;
import com.spvessel.Decorations.CustomFigure;
import com.spvessel.Decorations.ItemState;
import com.spvessel.GraphicsMathService;
import com.spvessel.Flags.ItemAlignment;
import com.spvessel.Flags.ItemStateType;
import com.spvessel.Flags.SizePolicy;
import com.spvessel.ButtonCore;
import com.spvessel.Grid;
import com.spvessel.Label;
import com.spvessel.Prototype;

class MemberItem extends Prototype {
    public static int _count = 0;
    public int index = 0;
    public Grid layout;
    public ButtonCore memberRemove;
    public Label memberName;
    public Label memberValue;
    private boolean _is_winner = false;

    public boolean isWinner() {
        return _is_winner;
    }

    public void setWinner(boolean value) {
        _is_winner = value;
        updateStyle(value);
    }

    private void updateStyle(boolean winner) {
        if (winner) {
            setBackground(45, 45, 45);
            memberName.setFontStyle(Font.BOLD);
            memberValue.setFontStyle(Font.BOLD);
        } else {
            setBackground(new Color(0, 0, 0, 0));
            memberName.setFontStyle(Font.PLAIN);
            memberValue.setFontStyle(Font.PLAIN);
        }
    }

    public MemberItem() {
        // self view attr
        setItemName("Member_" + _count);
        setBackground(0, 0, 0, 0);
        setSize(0, 30);
        setSizePolicy(SizePolicy.EXPAND, SizePolicy.FIXED);
        setPadding(5, 0, 5, 0);
        _count++;

        layout = new Grid(1, 3);
        memberName = new Label();
        memberValue = new Label();
        memberRemove = new ButtonCore();
    }

    @Override
    public void initElements() {
        // Name
        memberName.setStyle(Styles.getLabelStyle());
        
        // Value
        memberValue.setStyle(Styles.getLabelStyle());
        memberValue.setText("0%");
        memberValue.setWidth(45);
        memberValue.setWidthPolicy(SizePolicy.FIXED);
        memberValue.setTextAlignment(ItemAlignment.VCENTER, ItemAlignment.RIGHT);
        memberValue.setMargin(0, 0, 10, 0);

        // Button
        memberRemove.setBackground(new Color(255, 181, 111));
        memberRemove.setSize(14, 14);
        memberRemove.setSizePolicy(SizePolicy.FIXED, SizePolicy.FIXED);
        memberRemove.setAlignment(ItemAlignment.VCENTER, ItemAlignment.LEFT);
        memberRemove.setCustomFigure(new CustomFigure(false, GraphicsMathService.getCross(14, 14, 5, 45)));
        memberRemove.addItemState(ItemStateType.HOVERED, new ItemState(new Color(255, 255, 255, 125)));
        memberRemove.eventMouseClick.add((sender, args) -> disposeSelf());

        // adding
        addItem(layout);
        layout.addItems(memberName, memberValue, memberRemove);
    }

    public void disposeSelf() {
        CommonLogic.getInstance().deleteMember(CommonLogic.getInstance().Storage.Members, memberName.getText());
        getParent().removeItem(this);
    }
}
