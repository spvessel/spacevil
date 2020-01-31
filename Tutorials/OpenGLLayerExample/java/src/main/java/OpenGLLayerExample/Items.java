package OpenGLLayerExample;

import java.awt.Color;

import com.spvessel.spacevil.HorizontalSlider;
import com.spvessel.spacevil.HorizontalStack;
import com.spvessel.spacevil.Common.DefaultsService;
import com.spvessel.spacevil.Flags.EmbeddedImage;
import com.spvessel.spacevil.Flags.EmbeddedImageSize;
import com.spvessel.spacevil.Flags.ItemAlignment;
import com.spvessel.spacevil.Flags.SizePolicy;

public final class Items {

    private Items() {
    }

    public static ImagedButton getImagedButton(EmbeddedImage image, float imageRotationAngle) {
        ImagedButton btn = new ImagedButton(DefaultsService.getDefaultImage(image, EmbeddedImageSize.SIZE_32X32),
                imageRotationAngle);
        return btn;
    }

    public static HorizontalSlider getSlider() {
        HorizontalSlider slider = new HorizontalSlider();
        slider.setStep(0.2f);
        slider.setMinValue(2f);
        slider.setMaxValue(10f);
        slider.setSizePolicy(SizePolicy.FIXED, SizePolicy.FIXED);
        slider.setSize(150, 30);
        slider.setAlignment(ItemAlignment.HCENTER, ItemAlignment.BOTTOM);
        slider.setMargin(40, 0, 40, 0);
        slider.handler.setShadow(5, 0, 2, Color.BLACK);
        return slider;
    }

    public static HorizontalStack getToolbarLayout() {
        HorizontalStack layout = new HorizontalStack();
        layout.setContentAlignment(ItemAlignment.HCENTER);
        layout.setAlignment(ItemAlignment.BOTTOM, ItemAlignment.LEFT);
        layout.setSizePolicy(SizePolicy.EXPAND, SizePolicy.FIXED);
        layout.setHeight(30);
        layout.setSpacing(5, 0);
        layout.setMargin(20, 0, 20, 20);
        return layout;
    }
}
