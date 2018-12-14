package com.customchance;

import com.customchance.View.*;

public class App {
    public static void main(String[] args) {
        MainWindow app = new MainWindow();
        com.spvessel.WindowLayoutBox.tryShow(MainWindow.class.getSimpleName());
    }
}
