package InitTemplate;

import com.spvessel.spacevil.Common.CommonService;

public class App {
    public static void main(string[] args) {
        // show SpaceVIL info: version, platform, os
        System.out.println(CommonService.getSpaceVILInfo());
        // initialize SpaceVIL components
        CommonService.initSpaceVILComponents();

        // optional: create your window and show it
        MyWindow mw = new MyWindow();
        mw.Show();
    }
}
