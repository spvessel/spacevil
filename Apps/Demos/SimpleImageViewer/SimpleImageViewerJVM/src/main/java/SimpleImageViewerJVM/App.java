package SimpleImageViewerJVM;

import com.spvessel.spacevil.Common.CommonService;

public class App {

    public static void main(String[] args) {
        CommonService.initSpaceVILComponents();
        CustomStyle.initWparGridStyle();
        MainWindow mw = new MainWindow();
        mw.show();
    }
}
