package com.customchance.View;

import java.awt.Color;
import java.awt.image.BufferedImage;
import java.io.IOException;
import javax.imageio.ImageIO;
import com.customchance.Model.CommonLogic;
import com.customchance.Model.Member;

import com.spvessel.Common.DefaultsService;
import com.spvessel.Core.*;
import com.spvessel.Flags.*;
import com.spvessel.*;

public class MainWindow extends ActiveWindow {
    public ButtonCore addButton;
    public ButtonCore startButton;
    ListBox _listBox;

    @Override
    public void initWindow() {
        // Window attr
        WindowLayout Handler = new WindowLayout(this.getClass().getSimpleName(), "CustomChance", 360, 500, true);
        setHandler(Handler);
        Handler.setMinSize(350, 500);
        Handler.setBackground(new Color(45, 45, 45));
        Handler.getWindow().eventKeyRelease.add((sender, args) -> onKeyRelease(sender, args));

        BufferedImage iBig = null;
        BufferedImage iSmall = null;
        try {
            iBig = ImageIO.read(MainWindow.class.getResourceAsStream("/icons/icon_big.png"));
            iSmall = ImageIO.read(MainWindow.class.getResourceAsStream("/icons/icon_small.png"));
        } catch (IOException e) {
        }
        if (iBig != null && iSmall != null)
            Handler.setIcon(iBig, iSmall);

        // title attr
        TitleBar title = new TitleBar("Custom Chance");
        title.setIcon(iBig, 16, 16);
        title.setFont(DefaultsService.getDefaultFont(14));
        title.getMaximizeButton().setVisible(false);
        title.getCloseButton().eventMouseClick.clear();
        title.getCloseButton().eventMouseClick.add((sender, args) -> {
            CommonLogic.getInstance().trySerialize();
            this.getHandler().close();
        });

        // layout attr
        VerticalStack layout = new VerticalStack();
        layout.setMargin(0, title.getHeight(), 0, 0);
        layout.setPadding(3, 3, 3, 3);
        layout.setSpacing(0, 5);
        layout.setBackground(255, 255, 255, 20);

        // listbox
        _listBox = new ListBox();
        _listBox.setBackground(new Color(52, 52, 52));
        _listBox.setHScrollBarVisible(ScrollBarVisibility.NEVER);
        _listBox.setVScrollBarVisible(ScrollBarVisibility.NEVER);
        _listBox.setSelectionVisibility(false);

        // addButton
        addButton = new ButtonStand("Add a Member!");
        addButton.setStyle(Styles.getButtonStyle());
        addButton.setMargin(0, 5, 0, 5);
        addButton.setShadow(5, 0, 4, new Color(0, 0, 0, 150));
        addButton.eventMouseClick.add((sender, args) -> {
            InputDialog dialog = new InputDialog();
            dialog.onCloseDialog.add(() -> {
                String result = dialog.inputResult;

                // add logic member
                if (CommonLogic.getInstance().addMember(CommonLogic.getInstance().Storage.Members, result)) {
                    // add member to ui
                    MemberItem member = new MemberItem();
                    member.memberName.setText(result);
                    member.index = CommonLogic.getInstance().Storage.Members.size() - 1;
                    _listBox.addItem(member);
                }

                Handler.getWindow().setFocused(true);
            });
            dialog.show(Handler);
        });

        // addButton
        startButton = new ButtonStand("Make a Chance!");
        startButton.setStyle(Styles.getButtonStyle());
        startButton.setMargin(0, 5, 0, 5);
        startButton.setShadow(5, 0, 4, new Color(0, 0, 0, 150));
        startButton.eventMouseClick.add((sender, args) -> {
            if (CommonLogic.getInstance().Storage.Members == null
                    || CommonLogic.getInstance().Storage.Members.isEmpty())
                return;
            CommonLogic.getInstance().startRandom(CommonLogic.getInstance().Storage.Members);
            updateUI();
        });

        // adding
        Handler.addItems(title, layout);
        layout.addItems(addButton, _listBox, startButton);

        if (CommonLogic.getInstance().Storage.Members.size() > 0)
            restoreItems();
    }

    private void restoreItems() {
        for (Member item : CommonLogic.getInstance().Storage.Members) {
            // add member to ui
            MemberItem member = new MemberItem();
            _listBox.addItem(member);
            member.memberName.setText(item.name);
            member.memberValue.setText(item.value + "%");
            member.setWinner(item.isWinner);
            member.index = CommonLogic.getInstance().Storage.Members.size() - 1;
        }
    }

    public void updateUI() {
        int index = 0;
        for (InterfaceBaseItem var : _listBox.getListContent()) {
            MemberItem member = (MemberItem) var;
            member.memberName.setText(CommonLogic.getInstance().Storage.Members.get(index).name);
            member.memberValue.setText(CommonLogic.getInstance().Storage.Members.get(index).value + "%");
            member.setWinner(CommonLogic.getInstance().Storage.Members.get(index).isWinner);
            index++;
        }
    }

    private void onKeyRelease(InterfaceItem sender, KeyArgs args) {
        if (args.key == KeyCode.SPACE)
            addButton.eventMouseClick.execute(addButton, new MouseArgs());
    }
}
