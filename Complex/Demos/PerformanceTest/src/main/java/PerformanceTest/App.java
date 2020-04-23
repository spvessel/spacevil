package PerformanceTest;

import com.spvessel.spacevil.WindowManager;
import com.spvessel.spacevil.Common.CommonService;
import com.spvessel.spacevil.Flags.RenderType;

public class App {

    public static void main(String[] args) {
        System.out.println(CommonService.getSpaceVILInfo());
        System.out.println(CommonService.initSpaceVILComponents());

        setDebugPerformanceMode(); //unlock fps

        ButtonsTest bTest = new ButtonsTest();
        GraphTest gTest = new GraphTest();
        TextTest tTest = new TextTest();

        WindowManager.startWith(
            bTest
            // gTest
            // tTest
        );
    }
    
    private static void setDebugPerformanceMode() {
        WindowManager.setRenderType(RenderType.ALWAYS);
        WindowManager.enableVSync(0);
    }
}
