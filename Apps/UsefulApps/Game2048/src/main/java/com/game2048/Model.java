package com.game2048;

import java.util.ArrayList;
import java.util.List;

public class Model {
    final private static int FIELD_WIDTH = 4;
    private Tile[][] gameTiles;
    int score;
    int maxTile;

    public Model() {
        resetGameTiles();
    }

    public Tile[][] getGameTiles() {
        return gameTiles;
    }

    private void addTile() {
        List<Tile> list = getEmptyTiles();
        if (list != null && list.size() != 0) {
            int n = (int) (Math.random() * list.size());
            int v = (Math.random() < 0.9 ? 2 : 4);
            if (v > maxTile)
                maxTile = v;
            list.get(n).setValue(v);
        }
    }

    private List<Tile> getEmptyTiles() {
        List<Tile> list = new ArrayList<>();
        for (int i = 0; i < FIELD_WIDTH; i++) {
            for (int j = 0; j < FIELD_WIDTH; j++) {
                if (gameTiles[i][j].getValue() == 0)
                    list.add(gameTiles[i][j]);
            }
        }
        return list;
    }

    public void resetGameTiles() {
        gameTiles = new Tile[FIELD_WIDTH][FIELD_WIDTH];
        score = 0;
        maxTile = 0;
        for (int i = 0; i < FIELD_WIDTH; i++) {
            for (int j = 0; j < FIELD_WIDTH; j++) {
                gameTiles[i][j] = new Tile();
            }
        }
        addTile();
        addTile();
    }

    private boolean compressTiles(Tile[] tiles) {
        int i0 = 0;
        boolean b = false, out = false;
        for (int i = 0; i < tiles.length; i++) {
            if ((tiles[i].getValue() == 0) && (!b)) {
                b = true;
                i0 = i;
            }
            if ((tiles[i].getValue() != 0) && (b)) {
                tiles[i0].setValue(tiles[i].getValue());
                tiles[i].setValue(0);
                out = true;
                i0 += 1;
            }
        }
        return out;
    }

    private boolean mergeTiles(Tile[] tiles) {

        boolean out = false;
        for (int i = 0; i < tiles.length - 1; i++) {
            if ((tiles[i].getValue() != 0) && (tiles[i].getValue() == tiles[i + 1].getValue())) {
                tiles[i].setValue(tiles[i].getValue() + tiles[i + 1].getValue());
                tiles[i + 1].setValue(0);
                score += tiles[i].getValue();
                if (tiles[i].getValue() > maxTile)
                    maxTile = tiles[i].getValue();
                out = true;
            }
        }
        compressTiles(tiles);
        return out;
    }

    public void left() {
        boolean b1 = false;
        boolean b2 = false;
        for (int i = 0; i < FIELD_WIDTH; i++) {
            b1 = (b1 | compressTiles(gameTiles[i]));
            b2 = (b2 | mergeTiles(gameTiles[i]));
        }
        if (b1 | b2)
            addTile();
    }

    public void right() {
        rotateArr();
        rotateArr();
        left();
        rotateArr();
        rotateArr();
    }

    public void up() {
        rotateArr();
        rotateArr();
        rotateArr();
        left();
        rotateArr();
    }

    public void down() {
        rotateArr();
        left();
        rotateArr();
        rotateArr();
        rotateArr();

    }

    private void rotateArr() {
        Tile[][] arr = new Tile[FIELD_WIDTH][FIELD_WIDTH];
        for (int i = 0; i < FIELD_WIDTH; i++) {
            for (int j = 0; j < FIELD_WIDTH; j++) {
                arr[i][j] = gameTiles[FIELD_WIDTH - 1 - j][i];
            }
        }
        gameTiles = arr;
    }

    public boolean canMove() {
        if ((getEmptyTiles() != null) && (getEmptyTiles().size() > 0))
            return true;
        for (int i = 0; i < FIELD_WIDTH; i++) {
            for (int j = 0; j < FIELD_WIDTH - 1; j++) {
                if (gameTiles[i][j].getValue() == gameTiles[i][j + 1].getValue())
                    return true;
                if (gameTiles[j][i].getValue() == gameTiles[j + 1][i].getValue())
                    return true;
            }
        }
        return false;
    }
}
