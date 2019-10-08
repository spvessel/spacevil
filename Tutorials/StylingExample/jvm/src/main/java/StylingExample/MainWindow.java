package StylingExample;

import com.spvessel.spacevil.ActiveWindow;
import com.spvessel.spacevil.WrapGrid;
import com.spvessel.spacevil.Common.DefaultsService;
import com.spvessel.spacevil.Flags.EmbeddedImage;
import com.spvessel.spacevil.Flags.EmbeddedImageSize;
import com.spvessel.spacevil.Flags.MSAA;
import com.spvessel.spacevil.Flags.Orientation;

public class MainWindow extends ActiveWindow {
    // implement ActiveWindow class
    @Override
    public void initWindow() {
        // add style for our Picture class to the current theme
        DefaultsService.getDefaultTheme().addDefaultCustomItemStyle(Picture.class, StyleFactory.getPictureStyle());
        // slightly change style for WrapGrid
        StyleFactory.updateWrapGridStyle();

        // window attr
        setParameters("MainWindow", "StylingExample", 800, 600);
        setAntiAliasingQuality(MSAA.MSAA_8X);

        // create WrapGrid as main layout
        WrapGrid layout = new WrapGrid(160, 120, Orientation.HORIZONTAL);
        layout.setStretch(true);

        // add WrapGrid to window
        addItem(layout);

        // create and add 8 Picture items, the style for Picture class is automatically applied from the current theme
        layout.addItems(
                new Picture(DefaultsService.getDefaultImage(EmbeddedImage.USER, EmbeddedImageSize.SIZE_64X64),
                        "User Image"),
                new Picture(DefaultsService.getDefaultImage(EmbeddedImage.ADD, EmbeddedImageSize.SIZE_64X64),
                        "Add Image"),
                new Picture(DefaultsService.getDefaultImage(EmbeddedImage.ERASER, EmbeddedImageSize.SIZE_64X64),
                        "Eraser Image"),
                new Picture(DefaultsService.getDefaultImage(EmbeddedImage.FILTER, EmbeddedImageSize.SIZE_64X64),
                        "Filter Image"),
                new Picture(DefaultsService.getDefaultImage(EmbeddedImage.GEAR, EmbeddedImageSize.SIZE_64X64),
                        "Gear Image"),
                new Picture(DefaultsService.getDefaultImage(EmbeddedImage.HOME, EmbeddedImageSize.SIZE_64X64),
                        "Home Image"),
                new Picture(DefaultsService.getDefaultImage(EmbeddedImage.REFRESH, EmbeddedImageSize.SIZE_64X64),
                        "Refresh Image"),
                new Picture(DefaultsService.getDefaultImage(EmbeddedImage.PENCIL, EmbeddedImageSize.SIZE_64X64),
                        "Pencil Image")
                        );
    }
}