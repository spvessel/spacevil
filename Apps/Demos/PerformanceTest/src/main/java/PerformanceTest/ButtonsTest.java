package PerformanceTest;

import java.awt.Color;
import java.util.LinkedList;
import java.util.List;

import com.spvessel.spacevil.ActiveWindow;
import com.spvessel.spacevil.ButtonCore;
import com.spvessel.spacevil.HorizontalStack;
import com.spvessel.spacevil.Label;
import com.spvessel.spacevil.VerticalStack;
import com.spvessel.spacevil.WrapGrid;
import com.spvessel.spacevil.Core.InterfaceBaseItem;

public class ButtonsTest extends ActiveWindow {
    Label _statusLineOutput;

    @Override
    public void initWindow() {
        setParameters("ButtonsTest", "ButtonsTest JVM : SpaceVIL v0.3.4.1", 800, 800, true);
        isCentered = true;
        setBackground(24, 24, 24);
        setPadding(2, 30, 2, 2);

        _statusLineOutput = ItemsFactory.getStatusLine("No button has been pressed");
        addItem(_statusLineOutput);

        eventOnStart.add(() -> {
            // wrapTest(100000);
            shadowTest();
        });
    }

    private void wrapTest(int number) {
        WrapGrid wrapGrid = ItemsFactory.getWrapGrid();
        addItems(wrapGrid);
        List<InterfaceBaseItem> content = new LinkedList<>();
        for (int i = 0; i < number; i++) {
            content.add(ItemsFactory.getSimpleButton(i + 1, _statusLineOutput));
        }
        wrapGrid.setListContent(content);
    }

    private void shadowTest() {
        setBackground(200, 200, 200);
        VerticalStack vStack = ItemsFactory.getVerticalStack();
        addItems(vStack);
        int index = 0;
        for (int i = 0; i < 32; i++) {
            HorizontalStack h = ItemsFactory.getHorizontalStack();
            vStack.addItem(h);
            for (int j = 0; j < 32; j++) {
                InterfaceBaseItem btn = ItemsFactory.getSimpleButton(++index, _statusLineOutput);

                //set size & shadow
                btn.setSize(14, 14);
                ((ButtonCore) btn).setShadow(10, 3, 3, Color.BLACK);
                ((ButtonCore) btn).setShadowExtension(10, 10);

                h.addItem(btn);
            }
        }
    }
}