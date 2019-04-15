package com.game2048;

import com.spvessel.spacevil.*;
import com.spvessel.spacevil.Flags.ItemAlignment;
import com.spvessel.spacevil.Flags.SizePolicy;
import com.spvessel.spacevil.Label;
import com.spvessel.spacevil.Common.DefaultsService;

import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.IOException;

import javax.imageio.ImageIO;

public class GameWindow extends ActiveWindow {
    private Grid grid;
    private Label label;
    private static int gridWidth = 4;

    Controller controller;

    public GameWindow(Controller controller) {
        this.controller = controller;
    }

    @Override
    public void initWindow() {
        setParameters("GameWindow", "GameWindow", 400, 460, false);
        isResizable = false;

        setMinSize(400, 460);
        setMaxSize(400, 460);
        setBackground(new Color(0xbbada0));
        setPadding(2, 2, 2, 2);

        BufferedImage icon = null;
        BufferedImage iBig = null;
        BufferedImage iSmall = null;
        try {
            iBig = ImageIO.read(GameWindow.class.getResourceAsStream("/ibig.png"));
            iSmall = ImageIO.read(GameWindow.class.getResourceAsStream("/ismall.png"));
            icon = ImageIO.read(GameWindow.class.getResourceAsStream("/icon.png"));
        } catch (IOException e) {
        }
        if (iBig != null && iSmall != null)
            setIcon(iBig, iSmall);

        TitleBar title = new TitleBar("GAME2048");
        if (icon != null)
            title.setIcon(icon, 26, 26);
        title.getMaximizeButton().setVisible(false);
        title.setBackground(new Color(0xA29487));
        title.setFont(DefaultsService.getDefaultFont(Font.BOLD, 18));
        title.setForeground(new Color(0x6E655C));
        title.setShadow(5, 0, 2, new Color(0, 0, 0, 180));

        VerticalStack vstack = new VerticalStack();

        grid = new Grid(gridWidth, gridWidth);
        grid.setMargin(0, 35, 0, 0);
        grid.setPadding(6, 6, 6, 0);
        grid.setBackground(new Color(0xbbada0));
        grid.setSpacing(12, 12);
        eventKeyPress.add((intIte, keyArgs) -> controller.keyPressed(keyArgs));

        label = new Label("Score: 0");
        label.setHeight(42);
        label.setSizePolicy(SizePolicy.EXPAND, SizePolicy.FIXED);
        label.setTextAlignment(ItemAlignment.BOTTOM, ItemAlignment.HCENTER);
        label.setPadding(0, 0, 0, 2);
        label.setBackground(new Color(0xbbada0));
        label.setFont(DefaultsService.getDefaultFont(Font.BOLD, 36));
        label.setForeground(new Color(0x776e65));

        addItems(title, vstack);
        vstack.addItems(grid, label);
    }

    boolean isGameWon = false;
    boolean isGameLost = false;

    public void checkTheGame() {
        if (isGameWon) {
            MessageItem ms = new MessageItem("You won!", "Message");
            ms.onCloseDialog.add(() -> {
                if (ms.getResult())
                    controller.restartGame();
            });
            ms.show(this);
        }
        if (isGameLost) {
            MessageItem ms = new MessageItem("You lose!", "Message");
            ms.onCloseDialog.add(() -> {
                if (ms.getResult())
                    controller.restartGame();
            });
            ms.show(this);
        }

        label.setText("Score: " + controller.getScore());
    }

    public void setTiles(Tile[][] tiles) {
        int wdt = tiles.length;

        if (wdt != gridWidth && wdt != 0) {
            gridWidth = wdt;
            grid.setRowCount(gridWidth);
            grid.setColumnCount(gridWidth);
        }

        for (int i = 0; i < gridWidth; i++) {
            for (int j = 0; j < gridWidth; j++) {
                if (grid.getCell(i, j).getItem() != null)
                    grid.removeItem(grid.getCell(i, j).getItem());
                grid.insertItem(tiles[i][j], i, j);
//                grid.addItem(tiles[i][j]);
            }
        }
    }

}
