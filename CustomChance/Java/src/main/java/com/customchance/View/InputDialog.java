package com.customchance.View;

import java.awt.Color;
import com.spvessel.Common.DefaultsService;
import com.spvessel.Core.InterfaceItem;
import com.spvessel.Core.KeyArgs;
import com.spvessel.Core.MouseArgs;
import com.spvessel.Flags.KeyCode;
import com.spvessel.ButtonCore;
import com.spvessel.TextEdit;
import com.spvessel.TitleBar;
import com.spvessel.VerticalStack;
import com.spvessel.DialogWindow;
import com.spvessel.WindowLayout;

class InputDialog extends DialogWindow {
    public String inputResult = "";
    ButtonCore _add;
    TextEdit _input;

    @Override
    public void initWindow() {
        // window's attr
        WindowLayout Handler = new WindowLayout("InputDialog" + getCount(), "InputDialog" + getCount(), 330, 150,
                true);
        setHandler(Handler);
        Handler.setWindowTitle("Add member");
        Handler.setMinSize(330, 150);
        Handler.setBackground(new Color(45, 45, 45));
        Handler.isDialog = true;
        Handler.isAlwaysOnTop = true;

        _add = new ButtonStand("Add");
        _input = new TextEdit();

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
        _add.setShadow(5, 0, 4, new Color(0, 0, 0, 150));
        _add.eventMouseClick.add((sender, args) -> {
            inputResult = _input.getText();
            Handler.close();
        });

        // adding
        Handler.addItems(title, layout);
        layout.addItems(_input, _add);

        // focus item
        _input.setFocus();
    }

    private void onKeyRelease(InterfaceItem sender, KeyArgs args) {
        if (args.key == KeyCode.ENTER)
            _add.eventMouseClick.execute(_add, new MouseArgs());
    }
}
