package RadialMenu;

import java.awt.Color;
import java.util.LinkedList;
import java.util.List;

import com.spvessel.spacevil.ButtonCore;
import com.spvessel.spacevil.CoreWindow;
import com.spvessel.spacevil.ImageItem;
import com.spvessel.spacevil.ItemsLayoutBox;
import com.spvessel.spacevil.Prototype;
import com.spvessel.spacevil.Common.DefaultsService;
import com.spvessel.spacevil.Core.InterfaceBaseItem;
import com.spvessel.spacevil.Core.InterfaceDraggable;
import com.spvessel.spacevil.Core.InterfaceFloating;
import com.spvessel.spacevil.Core.InterfaceFreeLayout;
import com.spvessel.spacevil.Core.InterfaceItem;
import com.spvessel.spacevil.Core.MouseArgs;
import com.spvessel.spacevil.Core.Pointer;
import com.spvessel.spacevil.Decorations.Style;
import com.spvessel.spacevil.Flags.EmbeddedImage;
import com.spvessel.spacevil.Flags.EmbeddedImageSize;
import com.spvessel.spacevil.Flags.KeyCode;
import com.spvessel.spacevil.Flags.LayoutType;

public class RadialMenuItem extends Prototype implements InterfaceFloating, InterfaceFreeLayout, InterfaceDraggable {

    public int radius = 200;
    public int itemRadius = 30;
    public ButtonCore hideButton = new ButtonCore();
    public ButtonCore addButton = new ButtonCore("Add");

    public RadialMenuItem(CoreWindow wnd) {
        ItemsLayoutBox.addItem(wnd, this, LayoutType.FLOATING);

        setPassEvents(false);
        setStyle(DefaultsService.getDefaultStyle(RadialMenuItem.class));
    }

    private boolean _isInit = false;

    @Override
    public void initElements() {
        super.addItem(hideButton);
        super.addItem(addButton);

        ImageItem icon = new ImageItem(
                DefaultsService.getDefaultImage(EmbeddedImage.ARROW_LEFT, EmbeddedImageSize.SIZE_64X64));
        icon.setRotationAngle(90);
        icon.keepAspectRatio(true);
        icon.setColorOverlay(Color.BLACK);

        hideButton.addItem(icon);

        // Events
        hideButton.eventMouseClick.add((sender, args) -> {
            hide();
        });
        eventScrollUp.add((sender, args) -> {
            _degreeDiff = -_scrollStep * _toRadianCoeff;
            rearrangeContacts();
        });
        eventScrollDown.add((sender, args) -> {
            _degreeDiff = _scrollStep * _toRadianCoeff;
            rearrangeContacts();
        });
        eventKeyPress.add((sender, args) -> {
            if (args.key == KeyCode.MENU)
                hide();
        });
        eventMousePress.add(this::onMousePress);
        eventMouseDrag.add(this::onMouseDrag);
        eventMouseDrop.add(this::onMouseDrop);

        for (Contact item : _contacts) {
            super.addItem(item);
            item.updateSize(itemRadius);
        }

        _isInit = true;
    }

    // IDraggable
    private void onMousePress(InterfaceItem sender, MouseArgs args) {
        _beginDegree = calculateDegreeByPos(args.position);
    }

    private void onMouseDrag(InterfaceItem sender, MouseArgs args) {
        double dragDegree = calculateDegreeByPos(args.position);
        _degreeDiff = dragDegree - _beginDegree;
        _beginDegree = dragDegree;
        rearrangeContacts();
    }

    private void onMouseDrop(InterfaceItem sender, MouseArgs args) {
        _degreeDiff = 0;
    }

    // Our own container's AddItem
    @Override
    public void addItem(InterfaceBaseItem item) {
        if (item instanceof Contact) {
            Contact contact = (Contact) item;
            _contacts.add(contact);
            if (_isInit)
                super.addItem(contact);
            contact.updateSize(itemRadius);
        }
        calculateAlphaStep();
        updateLayout();
    }

    @Override
    public boolean removeItem(InterfaceBaseItem item) {
        if (super.removeItem(item)) {
            _contacts.remove(item);
            calculateAlphaStep();
            updateLayout();
            return true;
        }
        return false;
    }

    public List<Contact> getContacts() {
        return new LinkedList<>(_contacts);
    }

    // Style override
    @Override
    public void setStyle(Style style) {
        if (style == null)
            return;
        super.setStyle(style);

        Style innerStyle = style.getInnerStyle("hidebutton");
        hideButton.setStyle(innerStyle);

        innerStyle = style.getInnerStyle("addbutton");
        addButton.setStyle(innerStyle);
    }

    // IFree
    @Override
    public void setHeight(int height) {
        super.setHeight(height);
        updateLayout();
    }

    @Override
    public void setWidth(int width) {
        super.setWidth(width);
        updateLayout();
    }

    private boolean _isUpdating = false;

    // @Override
    public void updateLayout() {
        if (_isUpdating)
            return;
        _isUpdating = true;
        updateControlButtonsPosition();
        rearrangeContacts();
        _isUpdating = false;
    }

    // IFloating
    public void hide() {
        setVisible(false);
        getHandler().setFocus();
    }

    public void show() {
        if (!_isInit) {
            initElements();
            updateLayout();
        }
        setVisible(true);
    }

    public void show(InterfaceItem sender, MouseArgs args) {
        show();
    }

    private boolean _ouside = false;

    public boolean isOutsideClickClosable() {
        return _ouside;
    }

    public void setOutsideClickClosable(boolean value) {
        _ouside = value;
    }

    // logic
    private final double _doubledPI = 2 * Math.PI;
    private final double _toRadianCoeff = Math.PI / 180.0;
    private final double _scrollStep = 10.0;
    private double _alpha = Math.PI;
    private double _beginDegree = 0;
    private double _degreeDiff = 0;
    private List<Contact> _contacts = new LinkedList<Contact>();
    private double _alphaStep = 0;

    private void updateControlButtonsPosition() {
        int x = -hideButton.getWidth() / 2;
        int y = -hideButton.getHeight() / 2;

        x = getWidth() / 2 + getX() + x;
        y = getHeight() / 2 + getY() + y;

        hideButton.setPosition(x, y);
        addButton.setPosition(x + hideButton.getWidth() / 2 - addButton.getWidth() / 2,
                y + hideButton.getHeight() + 10);
    }

    private void rearrangeContacts() {
        Pointer centerPoint = getCenter();
        _alpha += _degreeDiff;

        for (Contact item : _contacts) {
            int x = (int) (radius * Math.cos(_alpha)) + centerPoint.getX() - itemRadius;
            int y = (int) (radius * Math.sin(_alpha)) + centerPoint.getY() - itemRadius;
            item.setPosition(x, y);
            item.setConfines();
            _alpha += _alphaStep;
        }
    }

    private double calculateDegreeByPos(Pointer position) {
        Pointer centerPoint = getCenter();
        double degree = Math.atan2(position.getY() - centerPoint.getY(), position.getX() - centerPoint.getX());
        if (degree < 0)
            degree += _doubledPI;
        return degree;
    }

    private void calculateAlphaStep() {
        _alphaStep = _doubledPI / _contacts.size();
    }

    private Pointer getCenter() {
        Pointer center = new Pointer();
        int xCenter = hideButton.getWidth() / 2 + hideButton.getX();
        int yCenter = hideButton.getHeight() / 2 + hideButton.getY();
        center.setPosition(xCenter, yCenter);
        return center;
    }
}