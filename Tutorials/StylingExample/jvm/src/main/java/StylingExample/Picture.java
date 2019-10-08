package StylingExample;

import java.awt.image.BufferedImage;

import com.spvessel.spacevil.ImageItem;
import com.spvessel.spacevil.Label;
import com.spvessel.spacevil.VerticalStack;
import com.spvessel.spacevil.Common.DefaultsService;
import com.spvessel.spacevil.Decorations.Style;

public class Picture extends VerticalStack {
    private ImageItem _imagePicture = null;
    private Label _imageName = null;

    public Picture(BufferedImage image, String name) {
        _imagePicture = new ImageItem(image);
        _imageName = new Label(name);

        // get style from current theme and apply it
        setStyle(DefaultsService.getDefaultStyle(Picture.class));

        // as you can see, no properties are set here, such as size, color, alignment and etc, 
        // you have to provide a style, for example, adding a style to the current theme
    }

    @Override
    public void initElements() {
        _imagePicture.keepAspectRatio(true);
        addItems(_imagePicture, _imageName);
    }

    // define style application rules for Picture class
    @Override
    public void setStyle(Style style) {
        if (style == null)
            return;
        // set style for current item: Picture class
        super.setStyle(style);

        // set style for ImageItem
        Style innerStyle = style.getInnerStyle("image");
        if (innerStyle != null)
            _imagePicture.setStyle(innerStyle);

        // set style for Label
        innerStyle = style.getInnerStyle("text");
        if (innerStyle != null)
            _imageName.setStyle(innerStyle);
    }
}
