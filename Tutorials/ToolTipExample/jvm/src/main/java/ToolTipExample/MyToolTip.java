package ToolTipExample;

import com.spvessel.spacevil.CoreWindow;
import com.spvessel.spacevil.ItemsLayoutBox;
import com.spvessel.spacevil.Label;
import com.spvessel.spacevil.Prototype;
import com.spvessel.spacevil.Core.InterfaceBaseItem;
import com.spvessel.spacevil.Core.InterfaceFloating;
import com.spvessel.spacevil.Core.InterfaceItem;
import com.spvessel.spacevil.Core.MouseArgs;
import com.spvessel.spacevil.Flags.LayoutType;

// this is an example of creating a custom simple tooltip
public class MyToolTip extends Prototype implements InterfaceFloating {

    Label _label = null;

    public MyToolTip(CoreWindow window, String text) {
        // MyToolTip attr
        setVisible(false);
        setBackground(35, 35, 35);
        isFocusable = false;
        // Label attr
        _label = new Label(text, false);
        _label.setMargin(10, 0, 10, 0);
        // add MyToolTip to ItemsLayoutBox: it is necessary for InterfaceFloating items
        ItemsLayoutBox.addItem(window, this, LayoutType.FLOATING);
    }

    private boolean _init = false;

    @Override
    public void initElements() {
        if (!_init) {
            addItems(ItemsFactory.getVerticalDivider(), _label);
            _init = true;
        }
    }

    // implement InterfaceFloating
    @Override
    public void hide() {
        setVisible(false);

    }

    @Override
    public void hide(MouseArgs arg0) {
        hide();
    }

    @Override
    public boolean isOutsideClickClosable() {
        return false;
    }

    @Override
    public void setOutsideClickClosable(boolean arg0) {
    }

    @Override
    public void show() {
        setVisible(true);
    }

    @Override
    public void show(InterfaceItem sender, MouseArgs args) {
        initElements();
        InterfaceBaseItem item = (InterfaceBaseItem) sender;
        setPosition(item.getX() + item.getWidth(), item.getY());
        setHeight(item.getHeight());
        setWidth(_label.getTextWidth() + _label.getMargin().left + _label.getMargin().right);
        show();
    }
}
