package com.game2048;

public class Game {
    public static void main(String... args) {
        com.spvessel.spacevil.Common.CommonService.initSpaceVILComponents();
        Model model = new Model();
        Controller controller = new Controller(model);
        controller.startGame();
    }
}
