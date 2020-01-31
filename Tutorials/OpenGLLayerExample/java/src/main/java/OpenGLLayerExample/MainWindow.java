package OpenGLLayerExample;

import com.spvessel.spacevil.ActiveWindow;
import com.spvessel.spacevil.HorizontalSlider;
import com.spvessel.spacevil.HorizontalStack;
import com.spvessel.spacevil.TitleBar;
import com.spvessel.spacevil.Flags.EmbeddedImage;
import com.spvessel.spacevil.Flags.KeyCode;

public class MainWindow extends ActiveWindow {

    @Override
    public void initWindow() {
        setParameters(this.getClass().getName(), this.getClass().getName(), 800, 800, false);

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
}
