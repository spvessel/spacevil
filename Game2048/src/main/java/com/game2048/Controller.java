package com.game2048;

import com.spvessel.spacevil.Core.KeyArgs;
import com.spvessel.spacevil.Flags.KeyCode;

public class Controller {
    private Model model;
    private GameWindow gw;
    final private static int WINNING_TILE = 2048;

    public Controller(Model model) {
        this.model = model;
        gw = new GameWindow(this);
        gw.setTiles(model.getGameTiles());

    }

    public void startGame() {
        gw.show();
    }

    public int getScore() {
        return model.score;
    }

    public void restartGame() {
        model.score = 0;
        model.resetGameTiles();
        gw.isGameLost = false;
        gw.isGameWon = false;
    }

    public void keyPressed(KeyArgs keyArgs) {
        if (keyArgs.key == KeyCode.ESCAPE) {
            restartGame();
            return;
        }
        if (!model.canMove()) gw.isGameLost = true;

        if ((!gw.isGameLost) && (!gw.isGameLost)) {
            switch (keyArgs.key) {
                case LEFT:
                    model.left();
                    break;
                case RIGHT:
                    model.right();
                    break;
                case UP:
                    model.up();
                    break;
                case DOWN:
                    model.down();
                    break;
            }
        }

        if (WINNING_TILE == model.maxTile) gw.isGameWon = true;
        gw.checkTheGame();
    }
}
