package SimpleImageViewerJVM;

import java.awt.Color;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;

import com.spvessel.spacevil.ActiveWindow;
import com.spvessel.spacevil.ButtonCore;
import com.spvessel.spacevil.Frame;
import com.spvessel.spacevil.HorizontalSplitArea;
import com.spvessel.spacevil.ImageItem;
import com.spvessel.spacevil.OpenEntryDialog;
import com.spvessel.spacevil.TitleBar;
import com.spvessel.spacevil.VerticalStack;
import com.spvessel.spacevil.WrapGrid;
import com.spvessel.spacevil.Common.DefaultsService;
import com.spvessel.spacevil.Decorations.Style;
import com.spvessel.spacevil.Flags.EmbeddedImage;
import com.spvessel.spacevil.Flags.EmbeddedImageSize;
import com.spvessel.spacevil.Flags.FileSystemEntryType;
import com.spvessel.spacevil.Flags.KeyMods;
import com.spvessel.spacevil.Flags.Orientation;
import com.spvessel.spacevil.Flags.Side;
import com.spvessel.spacevil.Flags.SizePolicy;
import com.spvessel.spacevil.OpenEntryDialog.OpenDialogType;

public class MainWindow extends ActiveWindow {
    WrapGrid imageArea;
    PreviewArea previewArea;
    AlbumSideList side;

    @Override
    public void initWindow() {
        setParameters("MainWindow", "MainWindow", 1240, 750, false);
        isCentered = true;

        BufferedImage icon = null;
        try {
            icon = ImageIO.read(MainWindow.class.getResourceAsStream("/icon.png"));
        } catch (IOException e) {
            e.printStackTrace();
        }

        TitleBar title = new TitleBar("Simple Image Viewer - JAVA");
        title.setIcon(icon, 24, 24);
        title.setPadding(4, 0, 5, 0);

        Frame layout = new Frame();
        layout.setMargin(0, title.getHeight(), 0, 0);
        layout.setPadding(0, 0, 0, 0);
        layout.setSpacing(6, 0);
        layout.setBackground(60, 60, 60);

        VerticalStack vToolbar = new VerticalStack();
        vToolbar.setWidthPolicy(SizePolicy.FIXED);
        vToolbar.setWidth(30);
        vToolbar.setBackground(32, 32, 32);
        vToolbar.setPadding(0, 30, 0, 0);

        HorizontalSplitArea splitArea = new HorizontalSplitArea();
        splitArea.setMargin(vToolbar.getWidth(), 0, 0, 0);
        splitArea.setSplitThickness(5);

        imageArea = new WrapGrid(160, 120, Orientation.HORIZONTAL);
        imageArea.setBackground(new Color(0, 0, 0, 0));
        imageArea.setPadding(15, 6, 6, 6);
        imageArea.getArea().setSpacing(6, 6);
        imageArea.vScrollBar.setStyle(Style.getSimpleVerticalScrollBarStyle());

        previewArea = new PreviewArea(imageArea);

        side = new AlbumSideList(this, Side.LEFT, imageArea, previewArea);
        side.setAreaSize(400);
        Album defaultAlbum = new Album("MyPictures", System.getProperty("user.home") + File.separator + "Pictures");
        defaultAlbum.onDoubleClick.add((sender) -> {
            side.hide();
            Model.fillImageArea(this, imageArea, previewArea, ((Album) sender).getPath());
        });
        side.addAlbum(defaultAlbum);

        ButtonCore btnSideAreaShow = new ButtonCore();
        btnSideAreaShow.setStyle(CustomStyle.getButtonStyle());

        ButtonCore btnOpenFolder = new ButtonCore();
        btnOpenFolder.setStyle(CustomStyle.getButtonStyle());

        ButtonCore btnAddAlbum = new ButtonCore();
        btnAddAlbum.setStyle(CustomStyle.getButtonStyle());

        // adding
        addItems(title, layout);
        layout.addItems(vToolbar, splitArea);

        splitArea.assignTopItem(previewArea);
        splitArea.assignBottomItem(imageArea);
        splitArea.setSplitPosition(300);

        vToolbar.addItems(btnSideAreaShow, btnOpenFolder, btnAddAlbum);

        btnSideAreaShow.addItem(new ImageItem(
                DefaultsService.getDefaultImage(EmbeddedImage.LINES, EmbeddedImageSize.SIZE_32X32), false));
        btnOpenFolder.addItem(new ImageItem(
                DefaultsService.getDefaultImage(EmbeddedImage.FOLDER, EmbeddedImageSize.SIZE_32X32), false));
        btnAddAlbum.addItem(new ImageItem(
                DefaultsService.getDefaultImage(EmbeddedImage.IMPORT, EmbeddedImageSize.SIZE_32X32), false));

        // events
        eventDrop.add((sender, args) -> {

            if (side.isHide) {
                if (new File(args.paths.get(0)).isDirectory())
                    Model.fillImageArea(this, imageArea, previewArea, args.paths.get(0));
            } else {
                for (String path : args.paths) {
                    if (new File(path).isDirectory()) {
                        Album album = new Album(new File(path).getName(), path);
                        side.addAlbum(album);
                        album.onDoubleClick.add((s) -> {
                            side.hide();
                            Model.fillImageArea(this, imageArea, previewArea, ((Album) s).getPath());
                        });
                    }
                }
            }
        });

        btnSideAreaShow.eventMouseClick.add((sender, args) -> {
            side.show();
        });

        btnOpenFolder.eventMouseClick.add((sender, args) -> {
            OpenEntryDialog browse = new OpenEntryDialog("Open Folder:", FileSystemEntryType.DIRECTORY,
                    OpenDialogType.OPEN);
            browse.onCloseDialog.add(() -> {
                Model.fillImageArea(this, imageArea, previewArea, browse.getResult());
            });
            browse.show(this);
        });

        imageArea.eventScrollUp.add((sender, args) -> {
            if (args.mods.contains(KeyMods.CONTROL) && args.mods.size() == 1) {
                int w = imageArea.getCellWidth() + 8;
                int h = imageArea.getCellHeight() + 6;
                if (w > 400)
                    return;
                imageArea.setCellSize(w, h);
            }
        });
        imageArea.eventScrollDown.add((sender, args) -> {
            if (args.mods.contains(KeyMods.CONTROL) && args.mods.size() == 1) {
                int w = imageArea.getCellWidth() - 8;
                int h = imageArea.getCellHeight() - 6;
                if (w < 160)
                    return;
                imageArea.setCellSize(w, h);
            }
        });

        btnAddAlbum.eventMouseClick.add((sender, args) -> {
            OpenEntryDialog browse = new OpenEntryDialog("Open Folder:", FileSystemEntryType.DIRECTORY,
                    OpenDialogType.OPEN);
            browse.onCloseDialog.add(() -> {
                if (browse.getResult() == null || browse.getResult().equals(""))
                    return;

                Album album = new Album(new File(browse.getResult()).getName(), browse.getResult());
                side.addAlbum(album);
                album.onDoubleClick.add((s) -> {
                    side.hide();
                    Model.fillImageArea(this, imageArea, previewArea, ((Album) s).getPath());
                });
            });
            browse.show(this);
        });
    }

    @Override
    public void show() {
        Model.fillImageArea(this, imageArea, previewArea,
                System.getProperty("user.home") + File.separator + "Pictures");
        super.show();
    }
}