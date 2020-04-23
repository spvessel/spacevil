package com.customchance;

import com.customchance.View.*;
import com.spvessel.spacevil.Common.CommonService;

public class App {
    public static void main(String[] args) {
        com.spvessel.spacevil.Common.CommonService.initSpaceVILComponents();
        System.out.println(CommonService.getSpaceVILInfo());
        MainWindow app = new MainWindow();
        app.show();
    }
}
