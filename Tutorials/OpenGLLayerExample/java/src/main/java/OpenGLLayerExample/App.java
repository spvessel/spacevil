package OpenGLLayerExample;

import com.spvessel.spacevil.Common.*;

public class App {
    public static void main(String[] args) {

        System.out.println(CommonService.getSpaceVILInfo());
        CommonService.initSpaceVILComponents();

        MainWindow mw = new MainWindow();
        mw.show();
    }
}
