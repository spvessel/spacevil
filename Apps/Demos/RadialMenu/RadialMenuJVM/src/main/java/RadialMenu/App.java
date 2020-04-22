package RadialMenu;

import com.spvessel.spacevil.Common.CommonService;

public class App {
    public static void main(String[] args) {
        System.out.println(CommonService.getSpaceVILInfo());
        if (!CommonService.initSpaceVILComponents())
            return;

        Model model = new Model();
        Controller controller = new Controller(model);
        controller.start();
    }
}
