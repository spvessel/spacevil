package SimpleImageViewerJVM;

import java.awt.Color;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;

import com.spvessel.spacevil.*;
import com.spvessel.spacevil.Common.*;
import com.spvessel.spacevil.Decorations.ItemState;
import com.spvessel.spacevil.Flags.*;

public class PreviewArea extends Prototype {
    private WrapGrid _wrapLink;
    private HorizontalStack _toolbar = new HorizontalStack();
    public Frame area = new Frame();
    private ImageItem _image = new ImageItem();

    private Label _pictureSize = new Label();
    private Label _pictureName = new Label();

    public PreviewArea(WrapGrid area) {
        _wrapLink = area;
        setSizePolicy(SizePolicy.EXPAND, SizePolicy.EXPAND);
        setSpacing(0, 5);
        setAlignment(ItemAlignment.LEFT, ItemAlignment.TOP);
        setPadding(0, 0, 0, 0);
        setBackground(20, 20, 20);
    }

    @Override
    public void initElements() {
        _image.keepAspectRatio(true);
        _image.isHover = false;

        _toolbar.setHeightPolicy(SizePolicy.FIXED);
        _toolbar.setHeight(30);
        _toolbar.setBackground(32, 32, 32);
        _toolbar.setSpacing(10, 0);
        _toolbar.setPadding(30, 0, 10, 0);

        _pictureSize.setWidthPolicy(SizePolicy.FIXED);
        _pictureSize.setWidth(100);
        _pictureSize.setTextAlignment(ItemAlignment.VCENTER, ItemAlignment.RIGHT);

        area.setMargin(0, 30, 0, 0);

        ButtonCore _expand = new ButtonCore();
        _expand.setSize(30, 30);
        _expand.setAlignment(ItemAlignment.RIGHT, ItemAlignment.BOTTOM);
        _expand.setMargin(0, 0, 10, 10);
        _expand.setBackground(new Color(0, 0, 0, 0));
        _expand.setBorderRadius(15);
        _expand.setPadding(5, 5, 5, 5);
        _expand.addItemState(ItemStateType.HOVERED, new ItemState(new Color(255, 255, 255, 20)));
        _expand.addItemState(ItemStateType.PRESSED, new ItemState(new Color(0, 0, 0, 0)));

        ButtonCore _menu = new ButtonCore();
        _menu.setSize(30, 30);
        _menu.setAlignment(ItemAlignment.RIGHT, ItemAlignment.TOP);
        _menu.setMargin(0, 40, 10, 0);
        _menu.setBackground(new Color(0, 0, 0, 0));
        _menu.setBorderRadius(15);
        _menu.setPadding(5, 5, 5, 5);
        _menu.addItemState(ItemStateType.HOVERED, new ItemState(new Color(255, 255, 255, 20)));
        _menu.addItemState(ItemStateType.PRESSED, new ItemState(new Color(0, 0, 0, 0)));

        addItems(_toolbar, area, _expand, _menu);
        area.addItem(_image);

        ImageItem eye = new ImageItem(DefaultsService.getDefaultImage(EmbeddedImage.EYE, EmbeddedImageSize.SIZE_32X32),
                false);
        eye.keepAspectRatio(true);
        ImageItem gear = new ImageItem(
                DefaultsService.getDefaultImage(EmbeddedImage.GEAR, EmbeddedImageSize.SIZE_32X32), false);
        gear.keepAspectRatio(true);

        _expand.addItem(eye);
        _menu.addItem(gear);

        ContextMenu _rotationMenu = new ContextMenu(getHandler());
        _rotationMenu.activeButton = MouseButton.BUTTON_LEFT;
        MenuItem _rot0 = new MenuItem("Rotate 0\u00b0");
        _rot0.eventMouseClick.add((sender, args) -> {
            _image.setRotationAngle(0);
        });
        MenuItem _rot90 = new MenuItem("Rotate 90\u00b0");
        _rot90.eventMouseClick.add((sender, args) -> {
            _image.setRotationAngle(90);
        });
        MenuItem _rot180 = new MenuItem("Rotate 180\u00b0");
        _rot180.eventMouseClick.add((sender, args) -> {
            _image.setRotationAngle(180);
        });
        MenuItem _rot270 = new MenuItem("Rotate -90\u00b0");
        _rot270.eventMouseClick.add((sender, args) -> {
            _image.setRotationAngle(270);
        });
        _rotationMenu.addItems(_rot0, _rot90, _rot180, _rot270);
        _menu.eventMouseClick.add((sender, args) -> {
            _rotationMenu.show(sender, args);
        });

        _toolbar.addItems(_pictureName, _pictureSize);

        _expand.eventMouseClick.add((sender, args) -> {
            String picture = Model.getPicturePath(_wrapLink, _pictureName.getText());
            if (!picture.equals("")) {
                BufferedImage img = null;
                try {
                    img = ImageIO.read(new File(picture));
                } catch (IOException e) {
                    System.out.println("Load image fail");
                }
                ImageItem image = new ImageItem(Model.downScaleBitmap(img, 1920, 1080), false);
                FullImageViewer viewer = new FullImageViewer(image);
                viewer.show(getHandler());
            }
        });
        
        eye.setColorOverlay(new Color(91, 225, 152), false);
        gear.setColorOverlay(new Color(110, 170, 255), false);

        _expand.eventMouseHover.add((sender, args) -> {
            eye.setColorOverlay(true);
        });
        _expand.eventMouseLeave.add((sender, args) -> {
            eye.setColorOverlay(false);
        });

        _menu.eventMouseHover.add((sender, args) -> {
            gear.setColorOverlay(true);
        });
        _menu.eventMouseLeave.add((sender, args) -> {
            gear.setColorOverlay(false);
        });
    }

    public void setPictureInfo(String name, int w, int h) {
        _pictureName.setText(name);
        _pictureSize.setText(w + " x " + h);
    }

    public void setImage(BufferedImage bitmap) {
        _image.setImage(Model.downScaleBitmap(bitmap, 1280, 720));
    }
}