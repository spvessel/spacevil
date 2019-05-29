package SimpleImageViewerJVM;

import java.awt.AlphaComposite;
import java.awt.Graphics2D;
import java.awt.RenderingHints;
import java.awt.RenderingHints.Key;
import java.awt.image.BufferedImage;
import java.awt.image.BufferedImageOp;
import java.awt.image.ConvolveOp;
import java.awt.image.Kernel;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Random;
import java.util.concurrent.atomic.AtomicInteger;

import javax.imageio.ImageIO;

import com.spvessel.spacevil.*;
import com.spvessel.spacevil.Core.*;
import com.spvessel.spacevil.Flags.*;

public class Model {

    public static void fillImageArea(CoreWindow handler, WrapGrid area, PreviewArea preview, String directory) {
        if (directory == null || directory.equals(""))
            return;

        LoadingScreen screen = new LoadingScreen();
        screen.show(handler);

        Thread tr = new Thread(() -> {
            area.clear();
            File fileFolder = new File(directory);

            ArrayList<File> files = new ArrayList<File>(Arrays.asList(fileFolder.listFiles()));
            ArrayList<Picture> list = new ArrayList<Picture>();

            int count = files.size();
            final AtomicInteger index = new AtomicInteger(0);
            files.stream().parallel().forEach((f) -> {
                index.getAndIncrement();
                if (checkExtensionFilter(f)) {
                    BufferedImage img = null;
                    try {
                        img = ImageIO.read(new File(f.getAbsolutePath()));
                    } catch (IOException e) {
                        System.out.println("Load image fail");
                    }

                    BufferedImage d_image = downScaleBitmap(img, 170, 100);
                    Picture pic = new Picture(d_image, f.getName(), f.getAbsolutePath());

                    pic.eventMouseClick.add((sender, args) -> {
                        replacePreviewImage(area, preview, pic);
                    });
                    pic.eventKeyPress.add((sender, args) -> {
                        if (args.key == KeyCode.ENTER)
                            replacePreviewImage(area, preview, pic);
                    });
                    list.add(pic);
                    d_image.flush();
                    img.flush();
                }
                float persent = (index.floatValue() / (float) count) * 100.0f;
                screen.setValue((int) persent);
            });

            if (list.size() == 0) {
                screen.setToClose();
                return;
            }

            Collections.sort(list);

            for (Picture item : list) {
                area.addItem(item);
            }

            Random rnd = new Random();
            int fileIndex = rnd.nextInt(list.size());

            BufferedImage img = null;
            try {
                img = ImageIO.read(new File(list.get(fileIndex).path));
            } catch (IOException e) {
                System.out.println("Load image fail");
            }
            preview.setImage(img);
            preview.setPictureInfo(new File(list.get(fileIndex).path).getName(), img.getWidth(), img.getHeight());
            img.flush();

            screen.setToClose();
            System.gc();
        });
        tr.start();
    }

    public static void replacePreviewImage(WrapGrid area, PreviewArea preview, Picture pic) {
        BufferedImage bitmap = null;
        try {
            bitmap = ImageIO.read(new File(pic.path));
        } catch (IOException e) {
            System.out.println("Load image fail");
        }

        preview.setImage(bitmap);
        preview.setPictureInfo(pic.name.getText(), bitmap.getWidth(), bitmap.getHeight());
        bitmap.flush();

        area.getArea().setFocus();
    }

    public static BufferedImage downScaleBitmap(BufferedImage img, int w, int h) {
        float boundW = w;
        float boundH = h;

        float ratioX = (boundW / img.getWidth());
        float ratioY = (boundH / img.getHeight());
        float ratio = ratioX < ratioY ? ratioX : ratioY;

        int resH = (int) (img.getHeight() * ratio);
        int resW = (int) (img.getWidth() * ratio);

        BufferedImage bmp = new BufferedImage(resW, resH, BufferedImage.TYPE_INT_ARGB);
        Graphics2D graphic = bmp.createGraphics();

        // graphic.setComposite(AlphaComposite.Src);
        // Map<Key, Object> map = new HashMap<>();
        // map.put(RenderingHints.KEY_INTERPOLATION,
        // RenderingHints.VALUE_INTERPOLATION_BILINEAR);
        // map.put(RenderingHints.KEY_RENDERING, RenderingHints.VALUE_RENDER_QUALITY);
        // map.put(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
        // RenderingHints hints = new RenderingHints(map);
        // float ninth = 1.0f / 2.0f;
        // float[] blurKernel = { ninth, ninth, ninth, ninth, ninth, ninth, ninth,
        // ninth, ninth };
        // BufferedImageOp op = new ConvolveOp(new Kernel(3, 3, blurKernel),
        // ConvolveOp.EDGE_NO_OP, hints);
        // graphic.drawImage(op.filter(img, null), 0, 0, resW, resH, null);
        graphic.drawImage(img, 0, 0, resW, resH, null);
        graphic.dispose();

        return bmp;
    }

    public static boolean checkExtensionFilter(File f) {
        String name = f.getName().toLowerCase();
        String[] filter = new String[] { ".png", ".bmp", ".jpg", ".jpeg" };

        for (String item : filter)
            if (name.endsWith(item))
                return true;

        return false;
    }

    public static String getPicturePath(WrapGrid area, String name) {
        List<InterfaceBaseItem> list = area.getListContent();
        for (InterfaceBaseItem item : list) {
            if (item instanceof Picture) {
                Picture tmp = (Picture) item;
                if (tmp.name.getText().equals(name))
                    return tmp.path;
            }
        }
        return "";
    }
}