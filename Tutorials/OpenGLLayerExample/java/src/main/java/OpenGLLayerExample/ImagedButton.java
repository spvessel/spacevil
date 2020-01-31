package OpenGLLayerExample;

import java.awt.image.BufferedImage;
import java.awt.Color;

import com.spvessel.spacevil.ButtonCore;
import com.spvessel.spacevil.ImageItem;
import com.spvessel.spacevil.Flags.ItemAlignment;
import com.spvessel.spacevil.Flags.SizePolicy;

public class ImagedButton extends ButtonCore {
    ImageItem _image = null;

    public ImagedButton(BufferedImage image, float imageRotationAngle) {
        setBackground(0xFF, 0xB5, 0x6F);
        setSizePolicy(SizePolicy.FIXED, SizePolicy.FIXED);
        setSize(30, 30);
        setAlignment(ItemAlignment.RIGHT, ItemAlignment.BOTTOM);
        setPadding(5, 5, 5, 5);
        setShadow(5, 0, 2, Color.BLACK);
        isFocusable = false;

        _image = new ImageItem(image, false);
        _image.setRotationAngle(imageRotationAngle);
        _image.setColorOverlay(Color.WHITE);
    }

    @Override
    public void initElements() {
        super.initElements();
        addItem(_image);
    }
}
