package RadialMenu;

import com.spvessel.spacevil.BlankItem;
import com.spvessel.spacevil.ImageItem;
import com.spvessel.spacevil.Label;
import com.spvessel.spacevil.Prototype;
import com.spvessel.spacevil.VerticalStack;
import com.spvessel.spacevil.Common.DefaultsService;
import com.spvessel.spacevil.Decorations.Style;
import com.spvessel.spacevil.Flags.EmbeddedImage;
import com.spvessel.spacevil.Flags.EmbeddedImageSize;

public class Contact extends Prototype {
    private VerticalStack _layout;
    private ImageItem _iconImage;
    private BlankItem _faceItem;
    private Label _notificationNumber;
    private Label _nameLabel;

    public Contact(String name) {
        _layout = new VerticalStack();
        _iconImage = new ImageItem(DefaultsService.getDefaultImage(EmbeddedImage.USER, EmbeddedImageSize.SIZE_64X64));
        _faceItem = new BlankItem();
        _nameLabel = new Label(name);
        _notificationNumber = new Label();

        setStyle(DefaultsService.getDefaultStyle(Contact.class));
    }

    public String getName() {
        return _nameLabel.getText();
    }

    public void updateSize(int radius) {
        int size = radius * 2;
        _faceItem.setSize(size, size);
        setHeight(size + _layout.getSpacing().vertical + _nameLabel.getHeight());

        int maxSize = (size > _nameLabel.getWidth()) ? size : _nameLabel.getWidth();
        setWidth(maxSize);
    }

    @Override
    public void initElements() {
        _iconImage.keepAspectRatio(true);
        _nameLabel.setWidth(_nameLabel.getTextWidth() + 20);

        addItems(_layout, _notificationNumber);
        _layout.addItems(_faceItem, _nameLabel);
        _faceItem.addItem(_iconImage);
    }

    public void setNotificationCount(int value) {
        _notificationNumber.setText(Integer.toString(value));
    }

    @Override
    public void setStyle(Style style) {
        if (style == null)
            return;
        super.setStyle(style);

        Style innerStyle = style.getInnerStyle("layout");
        _layout.setStyle(innerStyle);

        innerStyle = style.getInnerStyle("face");
        _faceItem.setStyle(innerStyle);

        innerStyle = style.getInnerStyle("name");
        _nameLabel.setStyle(innerStyle);

        innerStyle = style.getInnerStyle("notification");
        _notificationNumber.setStyle(innerStyle);
    }
}