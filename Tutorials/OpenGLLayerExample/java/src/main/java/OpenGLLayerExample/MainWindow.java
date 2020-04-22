package OpenGLLayerExample;

import java.util.*;
import java.awt.Color;

import com.spvessel.spacevil.*;
import com.spvessel.spacevil.Core.*;
import com.spvessel.spacevil.Flags.*;
import com.spvessel.spacevil.Decorations.*;

public class MainWindow extends ActiveWindow {

    @Override
    public void initWindow() {
        setParameters(this.getClass().getName(), this.getClass().getName(), 800, 800, false);
        isCentered = true;

        // oneCubeExample();
        multipleCubes();
    }

    private void oneCubeExample() {
        TitleBar titleBar = new TitleBar(this.getClass().getName());

        OpenGLLayer ogl = new OpenGLLayer();
        ogl.setMargin(0, titleBar.getHeight(), 0, 0);

        HorizontalStack toolbar = Items.getToolbarLayout();

        ImagedButton btnRotateLeft = Items.getImagedButton(EmbeddedImage.ARROW_UP, -90);
        ImagedButton btnRotateRight = Items.getImagedButton(EmbeddedImage.ARROW_UP, 90);

        HorizontalSlider zoom = Items.getSlider();

        ImagedButton btnRestoreView = Items.getImagedButton(EmbeddedImage.REFRESH, 0);

        // adding
        addItems(titleBar, ogl);
        ogl.addItems(toolbar);
        toolbar.addItems(btnRotateLeft, btnRotateRight, zoom, btnRestoreView);

        // assign events
        btnRestoreView.eventMousePress.add((sender, args) -> {
            ogl.restoreView();
        });

        btnRotateLeft.eventMousePress.add((sender, args) -> {
            ogl.rotate(KeyCode.LEFT);
        });

        btnRotateRight.eventMousePress.add((sender, args) -> {
            ogl.rotate(KeyCode.RIGHT);
        });

        zoom.eventValueChanged.add((sender) -> {
            ogl.setZoom(zoom.getCurrentValue());
        });

        // set focus
        ogl.setFocus();
        zoom.setCurrentValue(3);
    }

    private void multipleCubes() {
        TitleBar titleBar = new TitleBar(this.getClass().getName());

        FreeArea area = new FreeArea();
        area.setMargin(0, titleBar.getHeight(), 0, 0);

        addItems(titleBar, area);

        List<InterfaceBaseItem> content = new ArrayList<>();

        for (int row = 0; row < 3; row++) {
            for (int column = 0; column < 3; column++) {
                ResizableItem frame = new ResizableItem();
                frame.setBorder(new Border(Color.gray, new CornerRadius(), 2));
                frame.setPadding(5, 5, 5, 5);
                frame.setBackground(100, 100, 100);
                frame.setSize(200, 200);
                frame.setPosition(90 + row * 210, 60 + column * 210);
                area.addItem(frame);
                content.add(frame);

                frame.eventMousePress.add((sender, args) -> {
                    content.remove(frame);
                    content.add(frame);
                    area.setContent(content);
                });

                OpenGLLayer ogl = new OpenGLLayer();
                ogl.setMargin(0, 30, 0, 0);
                frame.addItem(ogl);
            }
        }
    }
}
