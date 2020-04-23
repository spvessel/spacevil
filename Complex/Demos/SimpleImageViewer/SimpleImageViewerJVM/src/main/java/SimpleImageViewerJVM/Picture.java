package SimpleImageViewerJVM;

import java.awt.Color;
import java.awt.image.BufferedImage;

import com.spvessel.spacevil.*;
import com.spvessel.spacevil.Flags.*;

public class Picture extends Prototype implements Comparable<Picture> {

    public ImageItem image = new ImageItem();
    public Label name = new Label();
    public String path = "";

    public Picture(BufferedImage image, String name, String path) {
        setSizePolicy(SizePolicy.EXPAND, SizePolicy.EXPAND);
        setSpacing(0, 5);
        setMargin(2, 2, 2, 2);
        setAlignment(ItemAlignment.LEFT, ItemAlignment.TOP);
        setBackground(new Color(80, 80, 80));
        setShadow(10, 0, 0, new Color(0, 0, 0, 200));

        this.image.setImage(image);
        this.name.setText(name);
        this.path = path;
    }

    @Override
    public int compareTo(Picture item) {
        return this.path.compareTo(item.path);
    }

    @Override
    public void initElements() {
        name.setHeightPolicy(SizePolicy.FIXED);
        name.setHeight(30);
        name.setTextAlignment(ItemAlignment.HCENTER, ItemAlignment.VCENTER);
        name.setMargin(5, 0, 5, 0);
        name.setAlignment(ItemAlignment.LEFT, ItemAlignment.BOTTOM);

        image.isHover = false;
        image.keepAspectRatio(true);
        image.setBackground(32, 32, 32);
        image.setMargin(0, 0, 0, 35);

        addItems(image, name);
    }
}