package SimpleImageViewerJVM;

import java.awt.Color;
import java.awt.Font;

import com.spvessel.spacevil.*;
import com.spvessel.spacevil.Common.*;
import com.spvessel.spacevil.Core.*;
import com.spvessel.spacevil.Decorations.Figure;
import com.spvessel.spacevil.Decorations.ItemState;
import com.spvessel.spacevil.Flags.*;

public class Album extends VerticalStack {
    public EventCommonMethodState onDoubleClick = new EventCommonMethodState();

    private HorizontalStack _topLayout = new HorizontalStack();
    ButtonToggle _expand = new ButtonToggle();
    private ImageItem _arrow = new ImageItem(
            DefaultsService.getDefaultImage(EmbeddedImage.ARROW_LEFT, EmbeddedImageSize.SIZE_32X32));
    public Label name = new Label();
    private ButtonCore _remove = new ButtonCore();

    private HorizontalStack _bottomLayout = new HorizontalStack();
    private Label _pathLabel = new Label("Path:");
    private TextEdit _pathEdit = new TextEdit();
    private ButtonCore _pathBrowse = new ButtonCore();

    public String getPath() {
        return _pathEdit.getText();
    }

    public Album(String name, String path) {
        setSizePolicy(SizePolicy.EXPAND, SizePolicy.FIXED);
        setHeight(30);
        setSpacing(0, 10);
        setMargin(3, 5, 3, 5);
        setAlignment(ItemAlignment.LEFT, ItemAlignment.TOP);
        setBackground(new Color(0, 0, 0, 0));
        setShadow(10, 0, 0, new Color(0, 0, 0, 200));
        this.name.setText(name);
        _pathEdit.setText(path);
        _bottomLayout.setVisible(false);
    }

    @Override
    public void initElements() {
        // top
        _topLayout.setHeightPolicy(SizePolicy.FIXED);
        _topLayout.setHeight(30);
        _topLayout.setSpacing(5, 0);
        _topLayout.setBackground(new Color(255, 255, 255, 20));

        _expand.setSize(20, 30);
        _expand.setBackground(25, 25, 25);
        _expand.addItemState(ItemStateType.TOGGLED, new ItemState(new Color(25, 25, 25)));
        _expand.setPadding(4, 9, 4, 9);
        _arrow.setRotationAngle(180);
        _arrow.setColorOverlay(new Color(210, 210, 210));
        _arrow.keepAspectRatio(true);

        name.setHeightPolicy(SizePolicy.FIXED);
        name.setHeight(30);
        name.setMargin(5, 0, 0, 0);
        name.setFontSize(16);
        name.setFontStyle(Font.BOLD);
        name.setCursor(EmbeddedCursor.HAND);

        _remove.setSize(12, 12);
        _remove.setCustomFigure(new Figure(false, GraphicsMathService.getCross(12, 12, 3, 45)));
        _remove.setBackground(100, 100, 100);
        _remove.setAlignment(ItemAlignment.VCENTER, ItemAlignment.LEFT);
        _remove.setMargin(0, 0, 5, 0);

        // bottom
        _bottomLayout.setHeightPolicy(SizePolicy.FIXED);
        _bottomLayout.setHeight(30);
        _bottomLayout.setSpacing(5, 0);
        _bottomLayout.setAlignment(ItemAlignment.LEFT, ItemAlignment.BOTTOM);

        _pathLabel.setWidthPolicy(SizePolicy.FIXED);
        _pathLabel.setWidth(_pathLabel.getTextWidth() + 5);
        _pathLabel.setFontSize(14);

        _pathBrowse.setSize(30, 30);
        _pathBrowse.setBackground(255, 255, 255, 20);
        _pathBrowse.setPadding(7, 7, 7, 7);
        _pathBrowse.addItemState(ItemStateType.HOVERED, new ItemState(new Color(255, 255, 255, 150)));

        _pathEdit.setBackground(Color.white);

        addItems(_topLayout, _bottomLayout);

        _topLayout.addItems(_expand, name, _remove);

        _bottomLayout.addItems(_pathLabel, _pathEdit, _pathBrowse);

        _expand.addItem(_arrow);
        _pathBrowse.addItem(new ImageItem(
                DefaultsService.getDefaultImage(EmbeddedImage.PENCIL, EmbeddedImageSize.SIZE_32X32), false));

        // events
        _expand.eventToggle.add((sender, args) -> {
            _bottomLayout.setVisible(!_bottomLayout.isVisible());
            if (_bottomLayout.isVisible()) {
                setHeight(70);
                _arrow.setRotationAngle(90);

            } else {
                setHeight(30);
                _arrow.setRotationAngle(180);
            }
        });

        name.eventMouseDoubleClick.add((sender, args) -> {
            onDoubleClick.execute(this);
        });

        _remove.eventMouseClick.add((sender, args) -> {
            remove();
        });
    }

    public void remove()
    {
        getParent().removeItem(this);
    }
}