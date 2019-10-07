package ToolTipExample;

import java.awt.Color;
import com.spvessel.spacevil.ActiveWindow;
import com.spvessel.spacevil.HorizontalStack;
import com.spvessel.spacevil.ToolTip;
import com.spvessel.spacevil.VerticalStack;
import com.spvessel.spacevil.Common.DefaultsService;
import com.spvessel.spacevil.Decorations.Border;
import com.spvessel.spacevil.Decorations.CornerRadius;
import com.spvessel.spacevil.Decorations.Shadow;
import com.spvessel.spacevil.Decorations.Style;
import com.spvessel.spacevil.Flags.EmbeddedImage;
import com.spvessel.spacevil.Flags.EmbeddedImageSize;

public class MainWindow extends ActiveWindow {

    @Override
    public void initWindow() {
        // apply new style for ToolTipItem
        initToolTipStyle();
        // window attr
        setParameters("MainWindow", "ToolTipExample", 800, 450);
        isCentered = true;

        // create toolbars
        HorizontalStack toolBar = ItemsFactory.getToolBarLayout();
        VerticalStack sideBar = ItemsFactory.getSideBarLayout();

        // add items to window
        addItems(toolBar, sideBar, ItemsFactory.getAreaForPermanentToolTip());

        // add items with standard tooltip to ToolBar
        toolBar.addItems(
                ItemsFactory.getTool(DefaultsService.getDefaultImage(EmbeddedImage.FILE, EmbeddedImageSize.SIZE_32X32),
                        "Create a new file"),
                ItemsFactory.getTool(
                        DefaultsService.getDefaultImage(EmbeddedImage.FOLDER, EmbeddedImageSize.SIZE_32X32),
                        "Create a new folder"),
                ItemsFactory.getTool(
                        DefaultsService.getDefaultImage(EmbeddedImage.RECYCLE_BIN, EmbeddedImageSize.SIZE_32X32),
                        "Delete"),
                ItemsFactory.getTool(
                        DefaultsService.getDefaultImage(EmbeddedImage.REFRESH, EmbeddedImageSize.SIZE_32X32),
                        "Refresh"));

        // add items with MyToolTip to sideBar
        sideBar.addItems(
                ItemsFactory.getSideTool(
                        DefaultsService.getDefaultImage(EmbeddedImage.FILE, EmbeddedImageSize.SIZE_32X32),
                        new MyToolTip(this, "Create new file:\nCreates a new file in the root directory.")),
                ItemsFactory.getHorizontalDivider(), // divider
                ItemsFactory.getSideTool(
                        DefaultsService.getDefaultImage(EmbeddedImage.FOLDER, EmbeddedImageSize.SIZE_32X32),
                        new MyToolTip(this, "Create new folder:\nCreates a new folder in the root directory.")),
                ItemsFactory.getHorizontalDivider(), // divider
                ItemsFactory.getSideTool(
                        DefaultsService.getDefaultImage(EmbeddedImage.RECYCLE_BIN, EmbeddedImageSize.SIZE_32X32),
                        new MyToolTip(this, "Delete:\nDelete selected file entry in root directory.")),
                ItemsFactory.getHorizontalDivider(), // divider
                ItemsFactory.getSideTool(
                        DefaultsService.getDefaultImage(EmbeddedImage.REFRESH, EmbeddedImageSize.SIZE_32X32),
                        new MyToolTip(this, "Refresh:\nUpdates the root directory.")));
    }

    private void initToolTipStyle() {
        // create style for ToolTipItem
        Style style = Style.getToolTipStyle();
        style.setBorder(new Border(Color.white, new CornerRadius(0, 5, 0, 5), 1));
        style.background = new Color(180, 180, 180);
        style.foreground = Color.black;
        style.font = DefaultsService.getDefaultFont(13);
        style.setShadow(new Shadow(5, 0, 3, new Color(0, 0, 0, 150)));
        style.isShadowDrop = true;
        Style textStyle = style.getInnerStyle("text");
        if (textStyle != null)
            textStyle.setMargin(25, 5, 10, 5);

        // manage tooltip via static ToolTip class
        ToolTip.setStyle(this, style);
        ToolTip.addItems(this, ItemsFactory.getDecor());
    }
}
