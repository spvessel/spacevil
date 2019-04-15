package com.customchance.View;

import java.awt.Color;

import com.spvessel.spacevil.Common.DefaultsService;
import com.spvessel.spacevil.Core.InterfaceItem;
import com.spvessel.spacevil.Core.KeyArgs;
import com.spvessel.spacevil.Core.MouseArgs;
import com.spvessel.spacevil.Flags.KeyCode;
import com.spvessel.spacevil.ButtonCore;
import com.spvessel.spacevil.CoreWindow;
import com.spvessel.spacevil.TextEdit;
import com.spvessel.spacevil.TitleBar;
import com.spvessel.spacevil.VerticalStack;
import com.spvessel.spacevil.DialogItem;

class InputDialog extends DialogItem {
    public String inputResult = "";
    ButtonCore _add = new ButtonStand("Add");;
    TextEdit _input = new TextEdit();

    public InputDialog() {
        setItemName("InputDialog_");
    }

    @Override
    public void initElements() {
        super.initElements();

        window.setBackground(new Color(45, 45, 45));

        // title
        TitleBar title = new TitleBar("Adding a new member");
        title.setFont(DefaultsService.getDefaultFont(14));
        title.getMinimizeButton().setVisible(false);
        title.getMaximizeButton().setVisible(false);

        VerticalStack layout = new VerticalStack();
        layout.setMargin(0, title.getHeight(), 0, 0);
        layout.setPadding(6, 15, 6, 6);
        layout.setSpacing(0, 30);
        layout.setBackground(255, 255, 255, 20);

        // message
        _input.eventKeyRelease.add((sender, args) -> onKeyRelease(sender, args));

        // ok
        _add.setBackground(255, 181, 111);
        _add.setStyle(Styles.getButtonStyle());
        _add.setShadow(5, 0, 4, new Color(0, 0, 0, 120));
        _add.eventMouseClick.add((sender, args) -> {
            inputResult = _input.getText();
            close();
        });

        // adding
        window.addItems(title, layout);
        layout.addItems(_input, _add);

        title.getCloseButton().eventMouseClick.clear();
        title.getCloseButton().eventMouseClick.add((sender, args) -> {
            close();
        });
    }

    @Override
    public void show(CoreWindow handler) {
        super.show(handler);
        _input.setFocus();
    }

    @Override
    public void close() {
        if (onCloseDialog != null)
            onCloseDialog.execute();

        super.close();
    }

    private void onKeyRelease(InterfaceItem sender, KeyArgs args) {
        if (args.key == KeyCode.ENTER)
            _add.eventMouseClick.execute(_add, new MouseArgs());
    }
}
