package SimpleImageViewerJVM;

import java.awt.Color;

import com.spvessel.spacevil.*;
import com.spvessel.spacevil.Decorations.CustomFigure;
import com.spvessel.spacevil.Flags.*;

public class FullImageViewer extends DialogItem {
    private ButtonCore _close = new ButtonCore();
    private ImageItem _image;

    public FullImageViewer(ImageItem image) {
        _image = image;
    }
    
    @Override
    public void initElements() {
        super.initElements();
        setBackground(0, 0, 0, 200);

        _image.keepAspectRatio(true);

        _close.setSize(30, 30);
        _close.setCustomFigure(new CustomFigure(false, GraphicsMathService.getCross(30, 30, 3, 45)));
        _close.setBackground(100, 100, 100);
        _close.setAlignment(ItemAlignment.TOP, ItemAlignment.RIGHT);
        _close.setMargin(0, 10, 10, 0);

        window.setSizePolicy(SizePolicy.EXPAND, SizePolicy.EXPAND);
        window.setMargin(20, 20, 20, 20);
        window.setBackground(new Color(0, 0, 0, 0));
        addItems(_close);

        window.addItem(_image);

        _close.eventMouseClick.add((sender, args) -> {
            close();
        });
    }

    @Override
    public void show(CoreWindow handler) {
        super.show(handler);
    }

    @Override
    public void close() {
        if (onCloseDialog != null)
            onCloseDialog.execute();

        super.close();
    }
}