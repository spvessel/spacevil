package com.customchance;

import com.customchance.View.*;

public class App {
    public static void main(String[] args) {
        com.spvessel.spacevil.Common.CommonService.initSpaceVILComponents();
        MainWindow app = new MainWindow();
        com.spvessel.WindowLayoutBox.tryShow(MainWindow.class.getSimpleName());
    }
}
