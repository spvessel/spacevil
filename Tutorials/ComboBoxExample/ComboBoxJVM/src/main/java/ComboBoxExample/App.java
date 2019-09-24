package ComboBoxExample;

import com.spvessel.spacevil.Common.CommonService;

public class App {
    public static void main(String[] args) {
        CommonService.initSpaceVILComponents();
        MainWindow mw = new MainWindow();
        mw.show();
    }
}
